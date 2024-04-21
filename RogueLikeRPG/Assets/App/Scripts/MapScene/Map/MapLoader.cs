using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Scripts;
using App.Scripts.DungeonScene.GenerationsScripts;
using App.Scripts.MapScene.Camera;
using App.Scripts.MapScene.Cells;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using Object = System.Object;

public class MapLoader : MonoBehaviour
{
    [HideInInspector] public List<GameObject> ActiveCells = new List<GameObject>();

    [SerializeField] private List<SerializedDictionaryItem<CellSO, int>> CellTypesFrequencySerializedDictionary =
        new List<SerializedDictionaryItem<CellSO, int>>();

    private Dictionary<CellSO, int> _cellTypesFrequencies = new Dictionary<CellSO, int>();

    private static Dictionary<string, CellSO> _cellTypes = new Dictionary<string, CellSO>();

    private static List<int> _tilesIndexes;
    //private Dictionary<CellSO, int> CellTypesProbabilities = new Dictionary<CellSO, int>();

    public static string CurrentCellId;

    public static bool WasSpawned = false;

    public static List<string> PassedCellsIds = new List<string>(); // список с пройденными клетками (по Id)

    [SerializeField] private Transform _spawnCell; // клетка для первого спавна

    public Transform Player;

    [SerializeField] private RandomTilemapGenerator _randomTilemapGenerator;

    public static MapLoader Instance = null;

    [SerializeField] private GameVictoryEnder _gameVictoryEnded;

    //public LoadTransition StartPanelLoadTransition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is can't be more than one Map Loader Instance");
            Destroy(gameObject);
        }

        //StartPanelLoadTransition.gameObject.SetActive(true);
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(
                CurrentCellId)) // если клетка для спавна не задана, то берётся та, которая указывается в _spawnCell
        {
            CurrentCellId = _spawnCell.GetComponent<BaseCell>().CellId;
        }
        else
        {
            var bossCell = UnityEngine.Object.FindObjectOfType<BossCell>();

            if (CurrentCellId == bossCell.CellId)
            {
                _gameVictoryEnded.EndGame();
            }
            
        }
        if (!WasSpawned)
        {
            GenerateCellsType();
            _tilesIndexes =
                _randomTilemapGenerator.Generate(); // запоминаю индексы (которые были рандомно сгенерированы) тайлов
        }
        else
        {
            ApplyGeneratedTypes();
            if (_tilesIndexes != null)
            {
                _randomTilemapGenerator.ApplyGeneration(_tilesIndexes);
            }
            else
            {
                Debug.LogError("Tilemap was not generated before.");
            }
        }

        UpdateInfo();
    }

    private void GenerateCellsType()
    {
        // _cellTypesFrequencies = ConvertToDictinary<CellSO, int>(CellTypesFrequencySerializedDictionary);

        // int currentFrequency = 0;

        /* int sumOfFrequency = CellTypesFrequencySerializedDictionary.Sum(c => c.Value);

        NormalCell[] cells = UnityEngine.Object.FindObjectsOfType<NormalCell>(); // все клетки без стартовой

        foreach (var item in cells)
        {
            int currentFrequency = 0;

            System.Random random = new System.Random();

            int frequencyNumber = random.Next(0, sumOfFrequency + 1);

            foreach (var cellTypeFrequency in CellTypesFrequencySerializedDictionary)
            {
                currentFrequency += cellTypeFrequency.Value;

                if (frequencyNumber <= currentFrequency)
                {
                    _cellTypes[item.CellId] = cellTypeFrequency.Key;
                    item.SetCellData(cellTypeFrequency.Key);
                    break;
                }

            }
        }*/

        //_cellTypesProbabilities

        /*int sumOfFrequency = cellTypesFrequencies.Sum(c => c.Value);

        foreach (var cellTypeFrequency in cellTypesFrequencies)
        {
            _cellTypesProbabilities.Add(cellTypeFrequency.Key, (float)cellTypeFrequency.Value / (float)sumOfFrequency);
        }*/

        NormalCell[] cells = UnityEngine.Object.FindObjectsOfType<NormalCell>();

        foreach (var item in cells)
        {
            System.Random random = new System.Random();

            int randomNumber = random.Next(0, item.PossibleCellData.Count);

            _cellTypes[item.CellId] = item.PossibleCellData[randomNumber];

            item.SetCellData(item.PossibleCellData[randomNumber]);
        }


        WasSpawned = true;
    }

    private void ApplyGeneratedTypes()
    {
        NormalCell[] cells = UnityEngine.Object.FindObjectsOfType<NormalCell>(); // все клетки без стартовой

        foreach (var cell in cells)
        {
            cell.SetCellData(_cellTypes[cell.CellId]);
        }

        /*foreach (var item in cells)
        {

            int currentFrequency = 0;


            System.Random random = new System.Random();

            int frequencyNumber = random.Next(0, sumOfFrequency + 1);

            foreach (var cellTypeFrequency in _cellTypesFrequencies)
            {
                currentFrequency += cellTypeFrequency.Value;

                if (frequencyNumber <= currentFrequency)
                {
                    item.CellData = cellTypeFrequency.Key;
                    break;
                }

            }
        }*/
    }

    public void UpdateInfo()
    {
        BaseCell[] cells = FindObjectsOfType<BaseCell>();

        GameObject currentCell = null;

        /*foreach (var item in cells)
        {
            Debug.LogError("CellID = " + item.CellId);
        }

        Debug.LogError("Current Cell Id = " + CurrentCellId);*/


        currentCell = cells.Where(c => c.CellId == CurrentCellId).FirstOrDefault().gameObject;

        /* foreach (var spawn in cells)
        {
            if (spawn.CellId == CurrentCellId)
            {
                currentCell = spawn.gameObject;
                break;
            }
        }*/

        if (currentCell != null)
        {
            BaseCell cell = currentCell.GetComponent<BaseCell>();

            PassedCellsIds.Add(cell.CellId);

            foreach (var passedCell in PassedCellsIds)
            {
                cells.First(c => c.CellId == passedCell).CellType = CellType.Passed;
            }

            // cell.CellType = CellType.Passed; раскомментить, если будут проблемы с отображением пройденных уровней
            // (если их спрайт меняется после того, как игрок "отбежал" от него)

            Player.position = currentCell.transform.position;

            CameraMover.Instance.ChangePositionToPlayer(); // мб событием

            foreach (var neighborCell in cell.NeighborsCells)
            {
                ActiveCells.Add(neighborCell);
                neighborCell.GetComponent<BaseCell>().CellType = CellType.Active;
            }

            cell.CellType = CellType.Current;
        }
    }

    private Dictionary<T, K> ConvertToDictinary<T, K>(List<SerializedDictionaryItem<T, K>> serializedDictionary)
    {
        Dictionary<T, K> dictionary = new Dictionary<T, K>();
        foreach (var item in serializedDictionary)
        {
            dictionary.Add(item.Key, item.Value);
        }

        return dictionary;
    }


    [Serializable]
    public class
        SerializedDictionaryItem<T,
            K> // отдельный класс для того, чтобы сделать dictionary (словарь), который отображается в Inspector'е
    {
        public T Key;
        public K Value;
    }
}
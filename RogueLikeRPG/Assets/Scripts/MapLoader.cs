using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    /*[SerializeField]
    private Transform _startCell;

    public static Transform CurrentCell;*/

    private const string TAG_NAME = "Cell";

    public static string CurrentCellId;

    [SerializeField]
    private Transform _spawnCell;

    public Transform Player;

    public static MapLoader Instance = null;

    private void Awake()
    {
        
    }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        // DontDestroyOnLoad(gameObject);

        if (string.IsNullOrEmpty(CurrentCellId))
        {
            CurrentCellId = _spawnCell.GetComponent<BaseCell>().CellId;
        }

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        BaseCell[] cells = Object.FindObjectsOfType<BaseCell>(); // IBASECELL

        GameObject currentCell = new GameObject();

        foreach (var spawn in cells)
        {
            if (spawn.CellId == CurrentCellId)
            {
                currentCell = spawn.gameObject;
                break;
            }
        }

        BaseCell cell = currentCell.GetComponent<BaseCell>(); // ПОМЕНЯТЬ НА IBASECELL

        Player.position = currentCell.transform.position; // Перемещаем игрока в центр клетки

        foreach (var neighborCell in cell.NeighborsCells)
        {
            neighborCell.GetComponent<BaseCell>().IsActive = true;
        }

        cell.IsActive = false;
    }
}

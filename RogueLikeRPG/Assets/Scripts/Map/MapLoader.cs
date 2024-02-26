using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> ActiveCells = new List<GameObject>();

    public static string CurrentCellId;

    public static List<string> PassedCellsIds = new List<string>(); // ������ � ����������� �������� (�� Id)

    [SerializeField]
    private Transform _spawnCell; // ������ ��� ������� ������

    public Transform Player;

    public static MapLoader Instance = null;
    private void Start()
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

        if (string.IsNullOrEmpty(CurrentCellId)) // ���� ������ ��� ������ �� ������, �� ������ ��, ������� ����������� � _spawnCell
        {
            CurrentCellId = _spawnCell.GetComponent<BaseCell>().CellId;
        }

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        BaseCell[] cells = Object.FindObjectsOfType<BaseCell>();

        GameObject currentCell = null;

        currentCell = cells.Where(c => c.CellId == CurrentCellId).FirstOrDefault().gameObject;

        /* foreach (var spawn in cells)
        {
            if (spawn.CellId == CurrentCellId)
            {
                currentCell = spawn.gameObject;
                break;
            }
        }*/

        if(currentCell != null)
        {
            BaseCell cell = currentCell.GetComponent<BaseCell>();

            PassedCellsIds.Add(cell.CellId);

            foreach(var passedCell in PassedCellsIds)
            {
                cells.First(c => c.CellId == passedCell).CellType = CellType.Passed;
            }

            // cell.CellType = CellType.Passed; �������������, ���� ����� �������� � ������������ ���������� �������
            // (���� �� ������ �������� ����� ����, ��� ����� "�������" �� ����)

            Player.position = currentCell.transform.position;

            CameraMover.Instance.ChangePositionToPlayer(); // �� ��������

            foreach (var neighborCell in cell.NeighborsCells)
            {
                ActiveCells.Add(neighborCell);
                neighborCell.GetComponent<BaseCell>().CellType = CellType.Active;
            }

            cell.CellType = CellType.Current;
        }
    }
}

using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> ActiveCells = new List<GameObject>();

    public static string CurrentCellId;

    [SerializeField]
    private Transform _spawnCell;

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
            Destroy(gameObject);
        }

        if (string.IsNullOrEmpty(CurrentCellId))
        {
            CurrentCellId = _spawnCell.GetComponent<BaseCell>().CellId;
        }

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        BaseCell[] cells = Object.FindObjectsOfType<BaseCell>();

        GameObject currentCell = null;

        foreach (var spawn in cells)
        {
            if (spawn.CellId == CurrentCellId)
            {
                currentCell = spawn.gameObject;
                break;
            }
        }

        if(currentCell != null)
        {
            BaseCell cell = currentCell.GetComponent<BaseCell>();

            Player.position = currentCell.transform.position;

            CameraMover.Instance.ChangePosition(); // לב סמבûעטול

            foreach (var neighborCell in cell.NeighborsCells)
            {
                ActiveCells.Add(neighborCell);
                neighborCell.GetComponent<BaseCell>().CellType = CellType.Active;
            }

            cell.CellType = CellType.Current;
        }
    }
}

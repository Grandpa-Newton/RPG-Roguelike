using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

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

        GameObject currentCell = new GameObject();

        foreach (var spawn in cells)
        {
            if (spawn.CellId == CurrentCellId)
            {
                currentCell = spawn.gameObject;
                break;
            }
        }

        BaseCell cell = currentCell.GetComponent<BaseCell>();

        Player.position = currentCell.transform.position;

        foreach (var neighborCell in cell.NeighborsCells)
        {
            neighborCell.GetComponent<BaseCell>().CellType = CellType.Active;
        }

        cell.CellType = CellType.Current;
    }
}

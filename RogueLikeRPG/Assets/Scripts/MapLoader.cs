using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [HideInInspector]
    public static string CurrentCellName = "Circle";

    [SerializeField]
    private Transform _spawnCell;

    public Transform Player;

    public static MapLoader Instance = null;

    private void Awake()
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

        /*if (_spawnCell != null)
        {
            CurrentCellName = _spawnCell.name;
        }*/


        UpdateInfo();
    }
    private void Start()
    {
    }

    public void UpdateInfo()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");

        GameObject currentCell = new();

        foreach(var spawn in cells)
        {
            if(spawn.name == CurrentCellName)
            {
                currentCell = GameObject.Find(CurrentCellName);
                break;
            }
        }

        NormalCell cell = currentCell.GetComponent<NormalCell>(); // ПОМЕНЯТЬ НА IBASECELL

        Player.position = currentCell.transform.position; // Перемещаем игрока в центр клетки

        foreach (var neighborCell in cell.NeighborsCells)
        {
            neighborCell.GetComponent<NormalCell>().IsActive = true;
        }

        cell.IsActive = false;
    }
}

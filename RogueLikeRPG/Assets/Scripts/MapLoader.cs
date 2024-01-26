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
            CurrentCellId = _spawnCell.GetComponent<NormalCell>().CellId;
        }

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        NormalCell[] cells = Object.FindObjectsOfType<NormalCell>(); // IBASECELL

        GameObject currentCell = new GameObject();

        foreach (var spawn in cells)
        {
            if (spawn.CellId == CurrentCellId)
            {
                currentCell = spawn.gameObject;
                break;
            }
        }

        NormalCell cell = currentCell.GetComponent<NormalCell>(); // �������� �� IBASECELL

        Player.position = currentCell.transform.position; // ���������� ������ � ����� ������

        foreach (var neighborCell in cell.NeighborsCells)
        {
            neighborCell.GetComponent<NormalCell>().IsActive = true;
        }

        cell.IsActive = false;
    }
}

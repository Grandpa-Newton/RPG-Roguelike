using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public Transform CurentCell;
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

        DontDestroyOnLoad(gameObject);

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        NormalCell cell = CurentCell.GetComponent<NormalCell>(); // ПОМЕНЯТЬ НА IBASECELL

        Player.position = CurentCell.position; // Перемещаем игрока в центр клетки

        foreach (var neighborCell in cell.NeighborsCells)
        {
            neighborCell.GetComponent<NormalCell>().IsActive = true;
        }

        cell.IsActive = false;
    }
}

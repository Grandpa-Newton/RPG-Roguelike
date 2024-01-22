using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private Transform _currentCell;
    [SerializeField] private Transform _player;

    private void Awake()
    {
    }
    private void Start()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        NormalCell cell = _currentCell.GetComponent<NormalCell>(); // ПОМЕНЯТЬ НА IBASECELL

        _player.position = _currentCell.position; // Перемещаем игрока в центр клетки

        foreach (var neighborCell in cell.NeighborsCells)
        {
            neighborCell.GetComponent<NormalCell>().IsActive = true;
        }

        cell.IsActive = false;
    }
}

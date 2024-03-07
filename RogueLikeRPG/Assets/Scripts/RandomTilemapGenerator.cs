using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RandomTilemapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private List<TileBase> tileBases;
    [SerializeField] private int lowerBorder;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    
    private void Start()
    {
        tilemap.ClearAllTiles();
        for (int x = -gridWidth; x < gridWidth; x++)
        {
            for (int y = lowerBorder; y < gridHeight; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
               
                    tilemap.SetTile(position, tileBases[Random.Range(0, tileBases.Count)]);
                
               
            }
        }
    }
}

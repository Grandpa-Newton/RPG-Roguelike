using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace App.Scripts.DungeonScene.GenerationsScripts
{
    public class RandomTilemapGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private List<TileBase> tileBases;
        [SerializeField] private int lowerBorder;
        [SerializeField] private int gridWidth;
        [SerializeField] private int gridHeight;
    


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Список индексов во время генерации</returns>
        public List<int> Generate() 
        {
            List<int> indexes = new List<int>();
            tilemap.ClearAllTiles();
            for (int x = -gridWidth; x < gridWidth; x++)
            {
                for (int y = lowerBorder; y < gridHeight; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);

                    int nextIndex = Random.Range(0, tileBases.Count);

                    tilemap.SetTile(position, tileBases[nextIndex]);

                    indexes.Add(nextIndex);

                }
            }

            return indexes;
        }

        public void ApplyGeneration(List<int> indexes)
        {
            tilemap.ClearAllTiles();
            int listIndex = 0;
            for (int x = -gridWidth; x < gridWidth; x++)
            {
                for (int y = lowerBorder; y < gridHeight; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);

                    tilemap.SetTile(position, tileBases[indexes[listIndex]]);

                    listIndex++;

                }
            }
        }
    }
}

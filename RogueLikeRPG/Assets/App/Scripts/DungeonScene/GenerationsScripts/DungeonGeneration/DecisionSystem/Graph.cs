using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.DecisionSystem
{
    public class Graph
    {
        private static List<Vector2Int> neighbourse4directions = new List<Vector2Int>
        {
            new Vector2Int(0, 1), // UP
            new Vector2Int(1, 0), // RIGHT
            new Vector2Int(0, -1), // DOWN
            new Vector2Int(-1, 0) // LEFT
        };

        private static List<Vector2Int> neighbourse8directions = new List<Vector2Int>
        {
            new Vector2Int(0, 1), // UP
            new Vector2Int(1, 0), // RIGHT
            new Vector2Int(0, -1), // DOWN
            new Vector2Int(-1, 0), // LEFT
            new Vector2Int(1, 1), // Diagonal
            new Vector2Int(1, -1), // Diagonal
            new Vector2Int(-1, 1), // Diagonal
            new Vector2Int(-1, -1) // Diagonal
        };

        List<Vector2Int> graph;

        public Graph(IEnumerable<Vector2Int> vertices)
        {
            graph = new List<Vector2Int>(vertices);
        }

        public List<Vector2Int> GetNeighbours4Directions(Vector2Int startPosition)
        {
            return GetNeighbours(startPosition, neighbourse4directions);
        }

        public List<Vector2Int> GetNeighbours8Directions(Vector2Int startPosition)
        {
            return GetNeighbours(startPosition, neighbourse8directions);
        }

        private List<Vector2Int> GetNeighbours(Vector2Int startPosition, List<Vector2Int> neighboursOffsetList)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>();
            foreach (var neighboiurDirection in neighboursOffsetList)
            {
                Vector2Int potentialNeighbour = startPosition + neighboiurDirection;
                if (graph.Contains(potentialNeighbour))
                    neighbours.Add(potentialNeighbour);
            }
            return neighbours;
        }
    }
}
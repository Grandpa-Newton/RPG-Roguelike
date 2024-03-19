using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.DecisionSystem
{
    public class GraphTest : MonoBehaviour
    {
        Graph graph;

        bool graphReady = false;

        Dictionary<Vector2Int, int> dijkstraResult;
        int highestValue;

        public void RunDijkstraAlgorithm(Vector2Int playerPosition,IEnumerable<Vector2Int> floorPositions)
        {
            graphReady = false;
            graph = new Graph(floorPositions);
            dijkstraResult = DijkstraAlgorithm.Dijkstra(graph, playerPosition);
            /*foreach (var value in dijkstraResult.Values) 
        {
            Debug.Log(value);
        }*/
            highestValue = dijkstraResult.Values.Max();
            graphReady = true;
        }


        private void OnDrawGizmosSelected()
        {
            if (graphReady && dijkstraResult != null)
            {
                foreach (var item in dijkstraResult)
                {
                    Color color = Color.Lerp(Color.green, Color.red, (float)item.Value / highestValue);
                    color.a = 0.5f;
                    Gizmos.color = color;
                    Gizmos.DrawCube(item.Key + new Vector2(0.5f, 0.5f), Vector3.one);
                }
            }
        }
    }
}

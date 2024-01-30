using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraAlgorithm 
{
    public static Dictionary<Vector2Int, int> Dijkstra(Graph graph, Vector2Int startPosition)
    {
        Queue<Vector2Int> unfinishedVerticecs = new Queue<Vector2Int>(); // необработанные вершины

        Dictionary<Vector2Int, int> distanceDictionary = new Dictionary<Vector2Int, int>(); // словарь дистанций
        Dictionary<Vector2Int, Vector2Int> parentDictionary = new Dictionary<Vector2Int, Vector2Int>(); // словарь для хранения предыдущей вершины у каждой вершины

        distanceDictionary[startPosition] = 0; // расстояние до самой себя
        parentDictionary[startPosition] = startPosition; // Установка предыдущей вершины для начальной позиции равной самой себе

        foreach (Vector2Int vertex in graph.GetNeighbours4Directions(startPosition))
        {
            unfinishedVerticecs.Enqueue(vertex);
            parentDictionary[vertex] = startPosition;
        }

        while (unfinishedVerticecs.Count > 0) // Пока в очереди есть незавершенные вершины
        {
            Vector2Int vertex = unfinishedVerticecs.Dequeue();
            int newDistance = distanceDictionary[parentDictionary[vertex]] + 1; // увеличение веса на 1 
            if(distanceDictionary.ContainsKey(vertex) && distanceDictionary[vertex] <= newDistance)
                continue;
            distanceDictionary[vertex] = newDistance;

            foreach (Vector2Int neighbour in graph.GetNeighbours4Directions(vertex))
            {
                if(distanceDictionary.ContainsKey(neighbour))
                    continue;
                unfinishedVerticecs.Enqueue(neighbour);
                parentDictionary[neighbour] = vertex;
            }
        }

        return distanceDictionary; // словарь расстояний, который теперь содержит минимальное расстояние от начальной позиции до каждой достижимой вершины
    }
}

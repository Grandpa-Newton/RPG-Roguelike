using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    // Параметры комнаты
    [SerializeField] private int corridorLength = 14;
    [SerializeField] private int corridorCount = 5;
    
    
    [SerializeField] [Range(0.1f, 1f)] private float roomPercent = 0.8f;
    
    // Список комнат 
    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

    private HashSet<Vector2Int> floorPositions;
    HashSet<Vector2Int> corridorPositions;
   
    //Gizmos Data
    private List<Color> roomColors = new List<Color>();
    [SerializeField]
    private bool showRoomGizmo = false, showCorridorsGizmo;
    
    // Ивенты
    public UnityEvent<DungeonData> OnDungeonFloorReady;

    [SerializeField]private RoomContentGenerator roomContentGenerator;
    
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGenerator();
        DungeonData data = new DungeonData
        {
            roomsDictionary = this.roomsDictionary,
            corridorPositions = this.corridorPositions,
            floorPositions = this.floorPositions
        };
        OnDungeonFloorReady?.Invoke(data);
    }
    

    private void CorridorFirstGenerator()
    {
        roomContentGenerator.RoomContentClear();
        floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

            // перенести в generateRooms
        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        GenerateRooms(potentialRoomPositions);
        
        //StartCoroutine(GenerateRoomsCoroutine(potentialRoomPositions));
        
    }
    private void GenerateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);
        
        floorPositions.UnionWith(roomPositions); // Объединяем плитки пола с созданными плитками коридоров в один tilemap
        
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }
    private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }

        return newCorridor;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(_randomWalkParameters, position);
                SaveRoomData(position, room);
                roomFloors.UnionWith(room);
            }
        }
    }
    
    private void ClearRoomData()
    {
        roomsDictionary.Clear();
        roomColors.Clear();
    }

    private void SaveRoomData(Vector2Int roomPosition, HashSet<Vector2Int> roomFloor)
    {
        roomsDictionary[roomPosition] = roomFloor;
        roomColors.Add(UnityEngine.Random.ColorHSV());
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                /*Из-за того что мы увеличили размер коридоров и сделали его 3, то теперь в радиусе 1 клетки всё окружено floor и алгоритм работает не
                 правильно поэтому нам нужно взять через одну клетку а так как direction это единичные передвижения то просто можно взять direction * 2
                */
                if (floorPositions.Contains(position + direction * 2)) 
                    neighboursCount++;
            }

            if (neighboursCount == 1)
                deadEnds.Add(position);
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate =
            potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        ClearRoomData();
        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(_randomWalkParameters, roomPosition);
            
            SaveRoomData(roomPosition, roomFloor);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions,
        HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();


        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        corridorPositions = new HashSet<Vector2Int>(floorPositions);
        
        return corridors;
    }
    
    private void OnDrawGizmosSelected()
    {
        if (showRoomGizmo)
        {
            int i = 0;
            foreach (var roomData in roomsDictionary)
            {
                Color color = roomColors[i];
                color.a = 0.5f;
                Gizmos.color = color;
                Gizmos.DrawSphere((Vector2)roomData.Key, 0.5f);
                foreach (var position in roomData.Value)
                {
                    Gizmos.DrawCube((Vector2)position + new Vector2(0.5f,0.5f), Vector3.one);
                }
                i++;
            }
        }
        if (showCorridorsGizmo && corridorPositions != null)
        {
            Gizmos.color = Color.magenta;
            foreach (var corridorTile in corridorPositions)
            {
                Gizmos.DrawCube((Vector2)corridorTile + new Vector2(0.5f, 0.5f), Vector3.one);
            }
        }
    }
    private IEnumerator GenerateRoomsCoroutine(HashSet<Vector2Int> potentialRoomPositions)
    {
        yield return new WaitForSeconds(2);
        tilemapVisualizer.Clear();
        GenerateRooms(potentialRoomPositions);
        DungeonData data = new DungeonData
        {
            roomsDictionary = this.roomsDictionary,
            corridorPositions = this.corridorPositions,
            floorPositions = this.floorPositions
        };
        OnDungeonFloorReady?.Invoke(data);
    }
}
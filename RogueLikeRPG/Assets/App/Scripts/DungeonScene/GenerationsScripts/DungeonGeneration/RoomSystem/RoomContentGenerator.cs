using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using System.Linq;
using App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.DecisionSystem;
using App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem;


public class RoomContentGenerator : MonoBehaviour
{
    [SerializeField] private RoomGenerator[] enemyRooms;
    [SerializeField] private RoomGenerator playerRoom;
    [SerializeField] private RoomGenerator exitRoom;

    List<GameObject> spawnedObjects = new List<GameObject>();

    [SerializeField] private GraphTest graphTest;

    public Transform itemParent;

    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

    public UnityEvent RegenerateDungeon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (var item in spawnedObjects)
            {
                Destroy(item);
            }

            RegenerateDungeon?.Invoke();
        }
    }

    IEnumerator Start()
    {
        yield return StartCoroutine(RoomsContentChildsDestroy());
    }

    IEnumerator RoomsContentChildsDestroy()
    {
        if (itemParent != null)
        {
            foreach (Transform item in itemParent)
            {
                Debug.Log(item.name + " was destroyed");
                Destroy(item.gameObject);
            }
        }

        yield return null;
    }

    public void GenerateRoomContent(DungeonData dungeonData)
    {
        foreach (GameObject item in spawnedObjects)
        {
            DestroyImmediate(item);
        }

        spawnedObjects.Clear();

        SelectPlayerSpawnPoint(dungeonData);
        SelectExitSpawnPoint(dungeonData);
        SelectEnemySpawnPoints(dungeonData);
        
        foreach (GameObject item in spawnedObjects)
        {
            if (item != null)
                item.transform.SetParent(itemParent, false);
        }
    }

    private void SelectExitSpawnPoint(DungeonData dungeonData)
    {
        int randomRoomIndex = UnityEngine.Random.Range(0, dungeonData.roomsDictionary.Count);
        Vector2Int playerSpawnPoint = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);
        
        Vector2Int roomIndex = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);
        
        List<GameObject> placedPrefabs = exitRoom.ProcessRoom(
            playerSpawnPoint,
            dungeonData.roomsDictionary.Values.ElementAt(randomRoomIndex),
            dungeonData.GetRoomFloorWithoutCorridors(roomIndex)
        );
        
        spawnedObjects.AddRange(placedPrefabs);
        
        dungeonData.roomsDictionary.Remove(playerSpawnPoint);
    }
    private void SelectPlayerSpawnPoint(DungeonData dungeonData)
    {
        int randomRoomIndex = UnityEngine.Random.Range(0, dungeonData.roomsDictionary.Count);
        Vector2Int playerSpawnPoint = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);

        graphTest.RunDijkstraAlgorithm(playerSpawnPoint, dungeonData.floorPositions);

        Vector2Int roomIndex = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);

        List<GameObject> placedPrefabs = playerRoom.ProcessRoom(
            playerSpawnPoint,
            dungeonData.roomsDictionary.Values.ElementAt(randomRoomIndex),
            dungeonData.GetRoomFloorWithoutCorridors(roomIndex)
        );

        FocusCameraOnThePlayer(placedPrefabs[placedPrefabs.Count - 1].transform);
        
        spawnedObjects.AddRange(placedPrefabs);

        dungeonData.roomsDictionary.Remove(playerSpawnPoint);
    }

    private void FocusCameraOnThePlayer(Transform playerTransform)
    {
        cinemachineCamera.LookAt = playerTransform;
        cinemachineCamera.Follow = playerTransform;
    }

    private void SelectEnemySpawnPoints(DungeonData dungeonData)
    {
        foreach (KeyValuePair<Vector2Int, HashSet<Vector2Int>> roomData in dungeonData.roomsDictionary)
        {
            int randomIndex = Random.Range(0, enemyRooms.Length);
            spawnedObjects.AddRange(
                enemyRooms[randomIndex].ProcessRoom(
                    roomData.Key,
                    roomData.Value,
                    dungeonData.GetRoomFloorWithoutCorridors(roomData.Key)
                )
            );
        }
    }
}
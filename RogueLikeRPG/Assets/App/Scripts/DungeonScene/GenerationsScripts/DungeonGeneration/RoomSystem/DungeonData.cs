using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem
{
    public class DungeonData
    {
        public Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary; // словарь комнат с их позицией и набором позиций внутри комнаты(свободное место)
        public HashSet<Vector2Int> floorPositions; // позиции пола
        public HashSet<Vector2Int> corridorPositions; // позиции коридоров

        // принимает ключ по которому можно получить набор позиций пола
        public HashSet<Vector2Int> GetRoomFloorWithoutCorridors(Vector2Int dictionaryKey) 
        {
            HashSet<Vector2Int> roomFloorNoCorridors = new HashSet<Vector2Int>(roomsDictionary[dictionaryKey]);
            roomFloorNoCorridors.ExceptWith(corridorPositions);
            return roomFloorNoCorridors;
        }
    
    }
}

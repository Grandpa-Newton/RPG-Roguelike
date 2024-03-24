using System.Collections.Generic;
using App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem.Items;
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem.RoomsScript
{
    public class ExitRoom : RoomGenerator
    {
        public GameObject exit;
        
        public List<ItemPlacementData> itemData;
        [SerializeField]
        private PrefabPlacer prefabPlacer;
        
        public override List<GameObject> ProcessRoom(Vector2Int roomCenter, HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoCorridors)
        {
            ItemPlacementHelper itemPlacementHelper = 
                new ItemPlacementHelper(roomFloor, roomFloorNoCorridors);
            
            List<GameObject> placedObjects = 
                prefabPlacer.PlaceAllItems(itemData, itemPlacementHelper);
            
            Vector2Int exitSpawnPoint = roomCenter;
            
            GameObject playerObject 
                = prefabPlacer.CreateObject(exit, exitSpawnPoint + new Vector2(0.5f, 0.5f));
            
            placedObjects.Add(playerObject);

            return placedObjects;
        }
    }
}

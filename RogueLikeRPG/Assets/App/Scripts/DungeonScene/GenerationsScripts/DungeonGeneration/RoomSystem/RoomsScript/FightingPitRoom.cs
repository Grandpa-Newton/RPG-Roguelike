using System.Collections.Generic;
using App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem.Items;
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem.RoomsScript
{
    public class FightingPitRoom : RoomGenerator
    {
        [SerializeField]
        private PrefabPlacer prefabPlacer;

        public List<EnemyPlacementData> enemyPlacementData;
        public List<ItemPlacementData> itemData;

        public override List<GameObject> ProcessRoom(Vector2Int roomCenter, HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoCorridors)
        {
            ItemPlacementHelper itemPlacementHelper =
                new ItemPlacementHelper(roomFloor, roomFloorNoCorridors);

            List<GameObject> placedObjects =
                prefabPlacer.PlaceAllItems(itemData, itemPlacementHelper);

            placedObjects.AddRange(prefabPlacer.PlaceEnemies(enemyPlacementData, itemPlacementHelper));

            return placedObjects;
        }
    }
}

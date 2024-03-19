using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem
{
    public abstract class RoomGenerator : MonoBehaviour
    {
        public abstract List<GameObject> ProcessRoom(
            Vector2Int roomCenter, 
            HashSet<Vector2Int> roomFloor, 
            HashSet<Vector2Int> corridors);
    }
}

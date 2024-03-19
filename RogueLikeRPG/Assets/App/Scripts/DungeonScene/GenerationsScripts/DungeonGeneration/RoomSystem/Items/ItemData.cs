using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem.Items
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        public Sprite sprite;
        public Vector2Int size = new Vector2Int(1, 1);
        public PlacementType placementType;
        public bool addOffset;
        public int health = 1;
        public bool nonDestuctible;

        public string sortingLayerName;
        public Vector2 colliderSize;
        public Vector2 colliderOffset;
    }
}

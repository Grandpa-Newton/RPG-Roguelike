using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.Generation
{
   public abstract class AbstractDungeonGenerator : MonoBehaviour
   {
      [FormerlySerializedAs("_tilemapVisualizer")] [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;
      [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

      public void GenerateDungeon()
      {
         tilemapVisualizer.Clear();
         RunProceduralGeneration();
      }

      protected abstract void RunProceduralGeneration();
   
   }
}

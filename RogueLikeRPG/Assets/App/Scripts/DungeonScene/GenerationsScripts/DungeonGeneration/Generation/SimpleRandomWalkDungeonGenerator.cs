using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.Generation
{
    public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
    {
        [SerializeField] protected SimpleRandomWalkSO _randomWalkParameters;
    
        protected override void RunProceduralGeneration()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(_randomWalkParameters, startPosition);
            tilemapVisualizer.Clear();
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }
    
        protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO randomWalkParameters, Vector2Int position)
        {
            var currentPosition = position;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < randomWalkParameters.iterations; i++)
            {
                var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
                floorPositions.UnionWith(path);
                if (randomWalkParameters.startRandomlyEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }

            return floorPositions;
        }
    }
}
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts
{
    [CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName = "PCG/SimpleRandomWalkData")]
    public class SimpleRandomWalkSO : ScriptableObject
    {
        public int iterations = 10;
        public int walkLength = 10;
        public bool startRandomlyEachIteration = true;
    }
}

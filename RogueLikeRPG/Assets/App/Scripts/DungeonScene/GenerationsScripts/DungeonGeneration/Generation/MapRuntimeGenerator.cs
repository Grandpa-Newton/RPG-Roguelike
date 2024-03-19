using UnityEngine;
using UnityEngine.Events;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.Generation
{
    public class MapRuntimeGenerator : MonoBehaviour
    {
        public UnityEvent OnStart;
        void Start()
        {
            OnStart?.Invoke();
        }
    }
}

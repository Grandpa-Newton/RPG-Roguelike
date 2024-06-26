using Unity.AI.Navigation;
using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts
{
    public class BuildNavMeshInRunTime : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface surface;

        private void Start()
        {
            BuildNavMash();
        }

        public void BuildNavMash()
        {
            surface = GetComponent<NavMeshSurface>();
            surface.BuildNavMesh();
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

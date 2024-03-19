using UnityEngine;

namespace App.Scripts.MapScene.Settings
{
    public class NextLevelManager : MonoBehaviour
    {
        public static NextLevelManager Instance;

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("There is no more than one Player instance");
            }
            Instance = this;
        }
    }
}

using System;
using App.Scripts.GameScenes.Player;
using App.Scripts.TraderScene;
using UnityEngine;

namespace App.Scripts.AllScenes
{
    public class SpawnObjectsManager : MonoBehaviour
    {
        private static SpawnObjectsManager _instance;

        public static SpawnObjectsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SpawnObjectsManager>();
                }

                return _instance;
            }
        }

        [SerializeField] private PlayerController playerPrefab;
        [SerializeField] private PlayerUIInitializer playerUIPrefab;
        //[SerializeField] private TraderUIInitializer traderUIPrefab;
        public event Action OnPlayerComponentsSpawn;

        void Awake()
        {
            PlayerUIInitializer playerUIObject = Instantiate(playerUIPrefab, transform.position, Quaternion.identity);
            //TraderUIInitializer traderUIObject = Instantiate(traderUIPrefab, transform.position, Quaternion.identity);
            PlayerController playerObject = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        
            OnPlayerComponentsSpawn?.Invoke();
        }
    }
}
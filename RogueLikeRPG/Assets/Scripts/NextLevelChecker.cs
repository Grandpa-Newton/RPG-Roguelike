using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelChecker : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    void Start()
    {
    }

    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            // Если на сцене 0 врагов, загружаем целевую сцену
            SceneManager.LoadScene(targetSceneName);
        }

    }
}

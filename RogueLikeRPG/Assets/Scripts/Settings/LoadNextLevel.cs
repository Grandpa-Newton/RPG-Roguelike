using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;

    // Update is called once per frame
    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

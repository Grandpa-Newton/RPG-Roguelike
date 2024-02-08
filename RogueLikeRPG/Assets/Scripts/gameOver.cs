using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    void Start()
    {
    }

    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            // Если на сцене 0 врагов, загружаем целевую сцену
            textField.gameObject.SetActive(true);
            textField.text = "Нет врагов! ПОБЕДА";
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadNextLevelScreen : MonoBehaviour
{
    private AsyncOperation _asyncOperation;
    [SerializeField] private Image LoadBar;
    [SerializeField] private TMP_Text BarTxt;
    [SerializeField] private string SceneName;

    private void Start()
    {
        StartCoroutine(LoadSceneCor());
    }

    IEnumerator LoadSceneCor()
    {
        _asyncOperation = SceneManager.LoadSceneAsync(SceneName);

        while (!_asyncOperation.isDone)
        {
            float progress = _asyncOperation.progress;

            BarTxt.text = $"Loading... {string.Format("{0,0}%", progress * 100f)}";

            float fillAmount = Mathf.Clamp01(progress); // Преобразуем прогресс в диапазон [0, 1]
            LoadBar.fillAmount = fillAmount;
            
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        float progress = _asyncOperation.progress;
        BarTxt.text = $"Loading... {string.Format("{0,0}%", progress * 100f)}";
    }
}
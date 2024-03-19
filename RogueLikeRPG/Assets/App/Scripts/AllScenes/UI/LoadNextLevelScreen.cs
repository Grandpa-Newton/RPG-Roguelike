using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.AllScenes.UI
{
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
}
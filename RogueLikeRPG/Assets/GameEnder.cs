using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScenes.Player.Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _textMesh;
    private void Start()
    {
        PlayerHealth.Instance.OnPlayerDie += InstanceOnOnPlayerDie;
    }

    private void InstanceOnOnPlayerDie()
    {
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.DOFade(1.0f, 1.0f);
        _textMesh.DOScale(2.0f, 4.0f).OnComplete((LoadMainMenu));
    }

    private void LoadMainMenu()
    {
        MapLoader.WasSpawned = false;
        MapLoader.CurrentCellId = null;
        PlayerHealth.Instance.OnPlayerDie -= InstanceOnOnPlayerDie;
        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnDestroy()
    {
        PlayerHealth.Instance.OnPlayerDie -= InstanceOnOnPlayerDie;
    }
}

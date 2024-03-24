using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.MixedScenes.Player.Control;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowPortalInteractButtonUI : MonoBehaviour, IInteractAction
{
    private bool _isInteractPressed;
    private bool _isInsideTrigger;
    private Animator _animator;

    [SerializeField] private string animationName; 
    [SerializeField] private string sceneName; 
    [SerializeField] private Canvas uiCanvas;

    public static string SceneName;

    private void Start()
    {
        if (TryGetComponent(out _animator))
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !_isInteractPressed)
        {
            _isInsideTrigger = true;
            uiCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !_isInteractPressed)
        {
            _isInsideTrigger = false;
            uiCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        InteractAction();
    }

    public void InteractAction()
    {
        if (_isInsideTrigger && Input.GetKeyDown(KeyCode.E) && !_isInteractPressed)
        {
            _isInteractPressed = true;
            if (_animator && animationName.IsNullOrWhitespace())
            {
                _animator.SetBool(animationName, _isInteractPressed);
                SceneManager.LoadScene(SceneName);
            }
        }
    }
}
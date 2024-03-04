using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShowDeLoadPanel : MonoBehaviour
{
    private Animator _animator;
    private NextLevelStarter _nextLevelStarter;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        //yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ExitScene()
    {
        // Включаем объект
        gameObject.SetActive(true);
        
        // Проигрываем анимацию
        _animator.Play("SceneTransition_01");

        // Задержка перед выходом со сцены
        StartCoroutine(ExitAfterAnimation());
    }
    
    IEnumerator ExitAfterAnimation()
    {
        // Ждем окончания анимации
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        // Здесь ваш код для выхода со сцены
    }
    
    public void ShowLoadPanel()
    {
        Debug.Log("ME IS WORKING");
        gameObject.SetActive(true);
        _animator.Play("SceneTransition_01");
    }

    public void HideDeLoadPanel()
    {
        gameObject.SetActive(false);
    }
}
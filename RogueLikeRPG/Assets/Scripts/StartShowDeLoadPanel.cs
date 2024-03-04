using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartShowDeLoadPanel : MonoBehaviour
{
    private Animator _animator;
    private NextLevelStarter _nextLevelStarter;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        //yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
    }
 
    private void InstanceOnInteractPressed()
    {
        Debug.Log("BABIZYANA");
        gameObject.SetActive(true);
        _animator.SetTrigger("StartAnim");
        StartCoroutine(ExitAfterAnimation());
        SceneManager.LoadScene("EasyLevelGenerateScene");
    }

    private void Start()
    {
        InputManager.Instance.OnInteractPressed += InstanceOnInteractPressed;
        gameObject.SetActive(false);
    }
    
    IEnumerator ExitAfterAnimation()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
    }

    public void HideDeLoadPanel()
    {
        gameObject.SetActive(false);
    }
}
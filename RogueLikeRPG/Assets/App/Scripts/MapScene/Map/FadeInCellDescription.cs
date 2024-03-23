using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeInCellDescription : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            FadeInM();
        }*/
    }

    public void FadeInM()
    {
        canvasGroup.DOFade(1, 1);
    }

    public void FadeOutM()
    {
        canvasGroup.DOFade(0, 1);
    }
}

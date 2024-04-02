using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class ShowMoneyInfo : MonoBehaviour
{
    [FormerlySerializedAs("intValueSO")] [SerializeField] private ChangeableValueSO changeableValueSO;
    [SerializeField] private float timeToHide;

    private CanvasGroup _canvasGroup;

    // Start is called before the first frame update
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        changeableValueSO.OnValueChange += ShowMoneyInformation;
        _canvasGroup.alpha = 0;
    }

    private void ShowMoneyInformation(int value)
    {
        StartCoroutine(ShowAndHideInfo());
    }

    private bool boolik;

    private IEnumerator ShowAndHideInfo()
    {
        boolik = true;
        _canvasGroup.DOFade(1, 1);
        yield return StartCoroutine(WaitToHideMoneyInfo());

        if (!boolik)
            _canvasGroup.DOFade(0, 1);
    }

    private IEnumerator WaitToHideMoneyInfo()
    {
        yield return new WaitForSeconds(timeToHide);
        boolik = false;
    }

    private void OnDestroy()
    {
        changeableValueSO.OnValueChange -= ShowMoneyInformation;
    }
}
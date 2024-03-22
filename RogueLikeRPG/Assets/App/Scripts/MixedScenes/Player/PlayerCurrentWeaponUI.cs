using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurrentWeaponUI : MonoBehaviour
{
    public static PlayerCurrentWeaponUI Instance { get; set; }
    [SerializeField] private Image meleeWeaponUI;
    [SerializeField] private Image meleeBackgroundImage;
    [SerializeField] private Image meleeWeaponIcon;
    [SerializeField] private Image rangeWeaponUI;
    [SerializeField] private Image rangeBackgroundImage;
    [SerializeField] private Image rangeWeaponIcon;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseMeleeWeaponScale()
    {
        meleeWeaponUI.transform.DOScale(1.2f, 0.5f);
        meleeWeaponUI.DOFade(1f, 0.5f);
        meleeBackgroundImage.DOFade(1f, 0.5f);
        meleeWeaponIcon.DOFade(1f, 0.5f);
            
        rangeWeaponUI.transform.DOScale(0.8f, 0.5f);
        rangeWeaponUI.DOFade(0.5f, 0.5f);
        rangeBackgroundImage.DOFade(0.5f, 0.5f);
        rangeWeaponIcon.DOFade(0.5f, 0.5f);
    }

    public void IncreaseRangeWeaponScale()
    {
        meleeWeaponUI.transform.DOScale(0.8f, 0.5f);
        meleeWeaponUI.DOFade(0.5f, 0.5f);
        meleeBackgroundImage.DOFade(0.5f, 0.5f);
        meleeWeaponIcon.DOFade(0.5f, 0.5f);
            
        rangeWeaponUI.transform.DOScale(1.2f, 0.5f);
        rangeWeaponUI.DOFade(1f, 0.5f);
        rangeBackgroundImage.DOFade(1f, 0.5f);
        rangeWeaponIcon.DOFade(1f, 0.5f);
    }

    public void SetMeleeWeaponIcon(Sprite meleeSprite)
    {
        meleeWeaponIcon.sprite = meleeSprite;
    }

    public void SetRangeWeaponIcon(Sprite rangeSprite)
    {
        rangeWeaponIcon.sprite = rangeSprite;
    }
}
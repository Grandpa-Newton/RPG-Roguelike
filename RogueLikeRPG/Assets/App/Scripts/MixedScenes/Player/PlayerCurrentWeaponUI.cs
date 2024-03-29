using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerCurrentWeaponUI
{
    private static PlayerCurrentWeaponUI _instance;

    public static PlayerCurrentWeaponUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerCurrentWeaponUI();
            }

            return _instance;
        }
    }
    
   [SerializeField] private Image _meleeWeaponUI;
    [SerializeField] private Image _meleeBackgroundImage;
    [SerializeField] private Image _meleeWeaponIcon;
    [SerializeField] private Image _rangeWeaponUI;
    [SerializeField] private Image _rangeBackgroundImage;
    [SerializeField] private Image _rangeWeaponIcon;

    public void Initialize(Image meleeWeaponUI, Image meleeBackgroundImage, Image meleeWeaponIcon,
        Image rangeWeaponUI, Image rangeBackgroundImage, Image rangeWeaponIcon )
    {
        _meleeWeaponUI = meleeWeaponUI;
        _meleeBackgroundImage = meleeBackgroundImage;
        _meleeWeaponIcon = meleeWeaponIcon;
        _rangeWeaponUI = rangeWeaponUI;
        _rangeBackgroundImage = rangeBackgroundImage;
        _rangeWeaponIcon = rangeWeaponIcon;
    }
    
    /*private void Awake()
    {
        Instance = this;
    }*/
    
    public void IncreaseMeleeWeaponScale()
    {
        _meleeWeaponUI.transform.DOScale(1.2f, 0.5f);
        _meleeWeaponUI.DOFade(1f, 0.5f);
        _meleeBackgroundImage.DOFade(1f, 0.5f);
        _meleeWeaponIcon.DOFade(1f, 0.5f);
            
        _rangeWeaponUI.transform.DOScale(0.8f, 0.5f);
        _rangeWeaponUI.DOFade(0.5f, 0.5f);
        _rangeBackgroundImage.DOFade(0.5f, 0.5f);
        _rangeWeaponIcon.DOFade(0.5f, 0.5f);
    }

    public void IncreaseRangeWeaponScale()
    {
        _meleeWeaponUI.transform.DOScale(0.8f, 0.5f);
        _meleeWeaponUI.DOFade(0.5f, 0.5f);
        _meleeBackgroundImage.DOFade(0.5f, 0.5f);
        _meleeWeaponIcon.DOFade(0.5f, 0.5f);
            
        _rangeWeaponUI.transform.DOScale(1.2f, 0.5f);
        _rangeWeaponUI.DOFade(1f, 0.5f);
        _rangeBackgroundImage.DOFade(1f, 0.5f);
        _rangeWeaponIcon.DOFade(1f, 0.5f);
    }

    public void SetMeleeWeaponIcon(Sprite meleeSprite)
    {
        _meleeWeaponIcon.sprite = meleeSprite;
    }

    public void SetRangeWeaponIcon(Sprite rangeSprite)
    {
        _rangeWeaponIcon.sprite = rangeSprite;
    }
}
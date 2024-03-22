using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurrentWeaponUI : MonoBehaviour
{
    public static PlayerCurrentWeaponUI Instance { get; set; }
    [SerializeField] private GameObject meleeWeaponUI;
    [SerializeField] private GameObject meleeBackgroundImage;
    [SerializeField] private GameObject meleeWeaponIcon;
    [SerializeField] private GameObject rangeWeaponUI;
    [SerializeField] private GameObject rangeBackgroundImage;
    [SerializeField] private GameObject rangeWeaponIcon;

    private Image _meleeWeaponSpriteRenderer;
    private Image _rangeWeaponSpriteRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        _meleeWeaponSpriteRenderer = meleeWeaponIcon.GetComponent<Image>();
        _rangeWeaponSpriteRenderer = rangeWeaponIcon.GetComponent<Image>();
    }

    public void IncreaseMeleeWeaponScale()
    {
        meleeWeaponUI.transform.DOScale(1.2f, 0.5f);
        
        meleeWeaponUI.GetComponent<Image>().DOFade(1f, 0.5f);
        meleeBackgroundImage.GetComponent<Image>().DOFade(1f, 0.5f);
        meleeWeaponIcon.GetComponent<Image>().DOFade(1f, 0.5f);
            
        rangeWeaponUI.transform.DOScale(0.8f, 0.5f);
        
        rangeWeaponUI.GetComponent<Image>().DOFade(0.5f, 0.5f);
        rangeBackgroundImage.GetComponent<Image>().DOFade(0.5f, 0.5f);
        rangeWeaponIcon.GetComponent<Image>().DOFade(0.5f, 0.5f);
    }
    public void IncreaseRangeWeaponScale()
    {
        meleeWeaponUI.transform.DOScale(0.8f, 0.5f);
        
        meleeWeaponUI.GetComponent<Image>().DOFade(0.5f, 0.5f);
        meleeBackgroundImage.GetComponent<Image>().DOFade(0.5f, 0.5f);
        meleeWeaponIcon.GetComponent<Image>().DOFade(0.5f, 0.5f);
            
        rangeWeaponUI.transform.DOScale(1.2f, 0.5f);
        
        rangeWeaponUI.GetComponent<Image>().DOFade(1f, 0.5f);
        rangeBackgroundImage.GetComponent<Image>().DOFade(1f, 0.5f);
        rangeWeaponIcon.GetComponent<Image>().DOFade(1f, 0.5f);
    }
    public void SetMeleeWeaponIcon(Sprite meleeSprite)
    {
        _meleeWeaponSpriteRenderer.sprite = meleeSprite;
    }
    public void SetRangeWeaponIcon(Sprite rangeSprite)
    {
        _rangeWeaponSpriteRenderer.sprite = rangeSprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

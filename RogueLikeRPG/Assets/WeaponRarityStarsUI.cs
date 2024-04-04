using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Weapon;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRarityStarsUI : MonoBehaviour
{
    public static WeaponRarityStarsUI Instance { get; private set; }

    [SerializeField] private CanvasGroup weaponCanvas;
    [SerializeField] private Image[] imageStars;
    [SerializeField] private Sprite inActiveStar;
    [SerializeField] private Sprite ActiveStar;
    private static readonly int IsActive = Animator.StringToHash("IsActive");
    [SerializeField] private TMP_Text weaponName;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    void Start()
    {
    }

    public void SetActiveWeaponStarsByRarity(WeaponItemSO weapon)
    {
        weaponName.text = weapon.itemName;
        for (var i = 0; i < imageStars.Length; i++)
        {
            Animator starAnimator = imageStars[i].gameObject.GetComponent<Animator>();
            if (i < (int)weapon.itemRarity)
            {
                imageStars[i].sprite = ActiveStar;
                starAnimator.SetBool(IsActive, true);
            }
            else
            {
                imageStars[i].sprite = inActiveStar;
                starAnimator.SetBool(IsActive, false);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            weaponCanvas.DOFade(1,0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            weaponCanvas.DOFade(0,0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
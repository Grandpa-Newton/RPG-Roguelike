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
    //public static WeaponRarityStarsUI Instance { get; private set; }

    
    [SerializeField] private Image[] imageStars;
    [SerializeField] private Sprite inActiveStar;
    [SerializeField] private Sprite ActiveStar;
    private static readonly int IsActive = Animator.StringToHash("IsActive");
    [SerializeField] private TMP_Text weaponName;

    /*private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }*/

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
    

    // Update is called once per frame
    void Update()
    {
    }
}
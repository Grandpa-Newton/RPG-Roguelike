using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScenes.Player;
using DG.Tweening;
using UnityEngine;

public class ShowWeaponDescriptionUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup weaponCanvas;
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
}
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private void OnTriggerStay2D(Collider2D other)
    {
        if ((layerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.transform.DOMove(transform.position, 1f);
        }
    }
}
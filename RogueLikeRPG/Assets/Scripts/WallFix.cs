using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFix : MonoBehaviour
{
    [SerializeField] private GameObject block;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            for (int i = 0; i < transform.childCount; i++)
                Instantiate(block, transform.GetChild(i).position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
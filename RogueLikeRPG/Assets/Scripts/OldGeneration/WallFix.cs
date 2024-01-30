using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFix : MonoBehaviour
{
    [SerializeField] private GameObject block;

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.LogError("Я ТУТ!!!");

        if (other.CompareTag("Block"))
        {
            Debug.LogError("Я ТУТ");
            for (int i = 0; i < transform.childCount; i++)
                Instantiate(block, transform.GetChild(i).position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
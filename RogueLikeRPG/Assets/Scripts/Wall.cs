using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject block;

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("NACHALO");
        if (other.CompareTag("Block"))
        {
            Debug.Log("123");
            Instantiate(block, transform.GetChild(0).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(1).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(2).position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

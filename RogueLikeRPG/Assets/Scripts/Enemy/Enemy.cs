using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private RoomContent _room;
    // Start is called before the first frame update
    void Start()
    {
        _room = GetComponentInParent<RoomContent>();
    }
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            _room.Enemies.Remove(gameObject);
        }
    }
}

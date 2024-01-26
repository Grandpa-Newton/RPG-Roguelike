using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private RoomContent _room;
    // Start is called before the first frame update
    void Start()
    {
        _room = GetComponentInParent<RoomContent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Destroy(gameObject);
        _room.Enemies.Remove(gameObject);
    }
}

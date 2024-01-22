using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    private RoomVariants _variants;
    private int _random;
    private bool spawned = false;
    private float waitTime = 3f;

    private void Start()
    {
        _variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn",0.2f);
    }

    public void Spawn()
    {
        if (!spawned)
        {
            if (direction == Direction.Top)
            {
                _random = Random.Range(0, _variants.topRooms.Length);
                Instantiate(_variants.topRooms[_random], transform.position,
                    _variants.topRooms[_random].transform.rotation);
            }
            else if (direction == Direction.Bottom)
            {
                _random = Random.Range(0, _variants.bottomRooms.Length);
                Instantiate(_variants.bottomRooms[_random], transform.position,
                    _variants.bottomRooms[_random].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                _random = Random.Range(0, _variants.leftRooms.Length);
                Instantiate(_variants.leftRooms[_random], transform.position,
                    _variants.leftRooms[_random].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                _random = Random.Range(0, _variants.rightRooms.Length);
                Instantiate(_variants.rightRooms[_random], transform.position,
                    _variants.rightRooms[_random].transform.rotation);
            }

            spawned = true;
        }
       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }
}

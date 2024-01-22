using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    
    
    [SerializeField] private GameObject _exitFromLevel;
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

    private GameObject _lastRoom = null;

    private void Start()
    {
        _variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        
        Invoke("Spawn",.2f);
            //OnExitSpawn?.Invoke();
    }

    public void Spawn()
    {
        if (!spawned)
        {
            if (direction == Direction.Top)
            {
                _random = Random.Range(0, _variants.topRooms.Length);
                _lastRoom = Instantiate(_variants.topRooms[_random], transform.position,
                    _variants.topRooms[_random].transform.rotation);
                ExitSpawn.lastRoom = _lastRoom;
            }
            else if (direction == Direction.Bottom)
            {
                _random = Random.Range(0, _variants.bottomRooms.Length);
                _lastRoom = Instantiate(_variants.bottomRooms[_random], transform.position,
                    _variants.bottomRooms[_random].transform.rotation);
                ExitSpawn.lastRoom = _lastRoom;
            }
            else if (direction == Direction.Left)
            {
                _random = Random.Range(0, _variants.leftRooms.Length);
                _lastRoom = Instantiate(_variants.leftRooms[_random], transform.position,
                    _variants.leftRooms[_random].transform.rotation);
                ExitSpawn.lastRoom = _lastRoom;
                
            }
            else if (direction == Direction.Right)
            {
                _random = Random.Range(0, _variants.rightRooms.Length);
                _lastRoom = Instantiate(_variants.rightRooms[_random], transform.position,
                    _variants.rightRooms[_random].transform.rotation);
                ExitSpawn.lastRoom = _lastRoom;
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

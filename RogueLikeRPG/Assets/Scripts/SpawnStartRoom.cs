using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStartRoom : MonoBehaviour
{
    private int _randomRoom;

    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private Transform _roomPoint;

    void Start()
    {
        _randomRoom = Random.Range(0, _rooms.Length);
        Instantiate(_rooms[_randomRoom], transform.position, _rooms[_randomRoom].transform.rotation);
    }
}
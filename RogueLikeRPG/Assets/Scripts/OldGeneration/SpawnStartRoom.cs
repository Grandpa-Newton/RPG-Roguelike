using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStartRoom : MonoBehaviour
{
    private int _randomRoom;

    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private GameObject _roomPoint;

    void Start()
    {
        _randomRoom = Random.Range(0, _rooms.Length);
        GameObject room = Instantiate(_rooms[_randomRoom], transform.position, _rooms[_randomRoom].transform.rotation);
        GameObject roomPoint = Instantiate(_roomPoint, transform.position, _roomPoint.transform.rotation);
        roomPoint.transform.parent = room.transform;
    }
}
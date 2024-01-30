using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomVariants : MonoBehaviour
{
   public GameObject[] topRooms;
   public GameObject[] bottomRooms;
   public GameObject[] rightRooms;
   public GameObject[] leftRooms;

   public GameObject key;

   [HideInInspector] public List<GameObject> rooms;

   private void Start()
   {
      StartCoroutine(RandomSpawner());
   }

   IEnumerator RandomSpawner()
   {
      yield return new WaitForSeconds(5f); // сделать не через время, а через конец создания комнат
      RoomContent lastRoom = rooms[rooms.Count - 1].GetComponent<RoomContent>();
      int random = Random.Range(0, rooms.Count - 2);
      Debug.Log(rooms.Count + " - Количество комнат");

      Instantiate(key, rooms[random].transform.position, Quaternion.identity);
      
      lastRoom._door.SetActive(true); // СДЕЛАТЬ ЧЕРЕЗ СОБЫТИЕ
      lastRoom.DestroyWalls(); // тоже через событие
   }
}

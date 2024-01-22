using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawn : MonoBehaviour
{
    public static GameObject lastRoom;
    public const float TIME_TO_SPAWN_EXIT = 3f;
    [SerializeField] private GameObject _exit;

    private void Start()
    {
        StartCoroutine(CheckAndSpawnExit());
    }

    // Костыльчик
    private IEnumerator CheckAndSpawnExit()
    {
        yield return new WaitForSeconds(3f);

        while (lastRoom == null)
        {
            yield return new WaitForSeconds(1f);
        }

        Instantiate(_exit, lastRoom.transform.position, Quaternion.identity);
        
    }
}
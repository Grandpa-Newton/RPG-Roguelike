using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject playerPrefab;
    //[SerializeField] private GameObject playerUIPrefab;
    void Start()
    {
        GameObject playerObject = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        //GameObject playerUIObject = Instantiate(playerUIPrefab, transform.position, Quaternion.identity);

    }


}

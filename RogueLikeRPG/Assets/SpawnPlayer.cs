using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject playerPrefab;
    void Awake()
    {
        GameObject playerObject = Instantiate(playerPrefab, transform.position - new Vector3(0, 2, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCanvas : MonoBehaviour
{
    [SerializeField] private GameObject showLoad;
    [SerializeField] private GameObject showDeLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        showLoad.gameObject.SetActive(true);
    }

    void OnApplicationQuit()
    {
       
        showDeLoad.SetActive(true);
    }
}

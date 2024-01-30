using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerClass : MonoBehaviour
{
    public static TestPlayerClass Instance = null;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}

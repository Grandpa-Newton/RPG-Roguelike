using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTransition : MonoBehaviour
{
    public static LoadTransition Instance { get; private set; }
    public NextLevelStarter nextLevelStarter;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNextScene()
    {
        nextLevelStarter.ABC();
    }
}

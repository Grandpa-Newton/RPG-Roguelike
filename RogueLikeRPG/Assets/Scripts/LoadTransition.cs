using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class LoadTransition : MonoBehaviour
{
    [HideInInspector]
    public NextLevelStarter NextLevelStarter;

    private void Awake()
    {

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
        NextLevelStarter.StartNextLevel();
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }
}

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
        gameObject.SetActive(true);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCell : BaseCell
{


    private void Awake()
    {
        SCENE_TO_LOAD = "TestScene";
        ConfigureObject();
    }

    private void Start()
    {

    }
    /* public override void Interact()
    {
        Debug.Log("YES! VICTORY!");
    } */
}
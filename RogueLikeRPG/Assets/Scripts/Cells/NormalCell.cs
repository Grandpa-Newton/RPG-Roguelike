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
        /* Instance.OnActiveCell += NormalCell_OnActiveCell;
        // Сделать этот метод событием вызываемом при каждой загрузке карты
        IsCellActive(); */
    }
    private void NormalCell_OnActiveCell()
    {
        /*if (isActive)
        {
            isActiveCircle.SetActive(true);
        }
        else
        {
            isActiveCircle.SetActive(false);
        }*/
    }
    public override void Interact()
    {
        /*if (!IsActive)
        {
            Debug.Log("Cell is not active!");
        }
        else
        {
            Debug.Log("YES! VICTORY!");
        }*/
        Debug.Log("YES! VICTORY!");

    }
}
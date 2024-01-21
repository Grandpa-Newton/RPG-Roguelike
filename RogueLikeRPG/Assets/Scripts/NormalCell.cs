using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCell : MonoBehaviour, IBaseCell
{
    [SerializeField]
    private bool isActive = false;

    public bool IsActive { get => isActive; set => isActive = value; }

    public void Interact()
    {
        if (!IsActive)
        {
            Debug.Log("Cell is not active!");
        }
        else
        {
            Debug.Log("YES! VICTORY!");
        }
    }
}

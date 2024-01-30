using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestSelectHandler : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponent<BaseCell>().CellType = DefaultNamespace.CellType.Selecting;
    }
}

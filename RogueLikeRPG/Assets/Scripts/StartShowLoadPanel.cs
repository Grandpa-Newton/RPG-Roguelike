using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShowLoadPanel : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(true);
    }
    public void HideLoadPanel()
    {
        gameObject.SetActive(false);
    }
}

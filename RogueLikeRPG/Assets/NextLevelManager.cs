using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class NextLevelManager : MonoBehaviour
{
    public LoadTransition LoadTransition;

    public static NextLevelManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is no more than one Player instance");
        }
        Instance = this;
    }
}

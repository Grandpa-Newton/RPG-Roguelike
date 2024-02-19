using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAnimator playerAnimator;
   
    private void Start()
    {
        playerAnimator.Initialize(playerController);
    }
    
}

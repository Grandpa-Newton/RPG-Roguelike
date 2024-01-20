using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class InputSystem : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private PlayerInput playerInput;

    [HideInInspector]
    public Vector2 MoveDir;

    [HideInInspector]
    public float LastHorizontalVector;

    [HideInInspector]
    public float LastVerticalVector;

    [SerializeField]
    private float MoveSpeed;



    private PlayerInputActions playerInputActions;


    private void Awake()
    {
        /*playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();*/
    }

    /*private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        MoveDir = new Vector2(inputVector.x, inputVector.y).normalized;
        
        if(MoveDir.x != 0)
        {
            LastHorizontalVector = MoveDir.x;
        }
        if(MoveDir.y != 0)
        {
            LastVerticalVector = MoveDir.y;
        }    

        playerRigidbody.velocity = new Vector2(MoveDir.x, MoveDir.y) * MoveSpeed;
    }*/
}

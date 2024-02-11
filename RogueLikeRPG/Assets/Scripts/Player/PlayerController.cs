using System;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }


    [Header("Components")] 
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float accelerationMaxTime = 1.25f;
    [SerializeField] private AnimationCurve accelerationCurve;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] private VisualEffect vfxRenderer;
    private PlayerInputActions _playerInputActions;

    private bool isMoving;
    private float buttonHeldTime;
   
    //Events
    public event Action OnPlayerMovement;

    // Vectors
    private Vector2 _inputMouseVector;
    private Vector2 _worldMousePos;
    private Vector2 _inputVector;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is no more than one Player instance");
        }

        Instance = this;

        EnablePlayerComponents();
    }

    private void Update()
    {
        VFXRenderer();
        ProcessPlayerInput();
    }

    private void FixedUpdate()
    {
        float speed = CalculateSpeed(_inputVector, accelerationCurve);
        SetVelocity(speed, _inputVector);
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rigidbody2D.AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
    private void ProcessPlayerInput()
    {
        // Movement
        _inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        SetAccelerationParameters(_inputVector);


        // Mouse/Stick
        _inputMouseVector = _playerInputActions.Player.PointerPosition.ReadValue<Vector2>();

        _worldMousePos = (Camera.main.ScreenToViewportPoint(_inputMouseVector) - new Vector3(0.5f, 0.5f, 0f)) * 2;

        OnPlayerMovement?.Invoke();
    }

    private void SetAccelerationParameters(Vector2 input)
    {
        if (input.magnitude > 0)
        {
            isMoving = true;
            buttonHeldTime += Time.deltaTime;
        }
        else
        {
            isMoving = false;
            buttonHeldTime = 0;
        }
    }

    private float CalculateSpeed(Vector2 input, AnimationCurve animationCurve)
    {
        if (isMoving)
        {
            float acceleration = accelerationCurve.Evaluate(buttonHeldTime / accelerationMaxTime);
            return maxSpeed * acceleration;
        }

        return 0;
    }

    private void SetVelocity(float speed, Vector2 inputVector)
    {
        rigidbody2D.velocity = new Vector2(inputVector.x, inputVector.y).normalized * speed;
    }

    private void EnablePlayerComponents()
    {
        // Rigidbody
        rigidbody2D = GetComponent<Rigidbody2D>();
        // Player Input Actions
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void VFXRenderer()
    {
        vfxRenderer.SetVector3("ColliderPos", transform.position);
    }

    public Vector2 GetMoveDirection()
    {
        return _inputVector;
    }

    public Vector2 GetMouseDirection()
    {
        return _worldMousePos;
    }
}
using System;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Rigidbody2D _rigidbody2D;
    private PlayerInputActions _playerInputActions;

    [SerializeField] private float maxSpeed = 5;

    [Header("Player Acceleration Components")] [SerializeField]
    private float accelerationMaxTime = 1.25f;
    [SerializeField] private AnimationCurve accelerationCurve;
    // [SerializeField] private VisualEffect vfxRenderer;

    private bool _isMoving;
    private float _buttonHeldTime;

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
            Debug.LogError("Second Player/Instance was deleted!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            EnablePlayerComponents();
        }
    }

    private void Update()
    {
        //VFXRenderer();
        ProcessPlayerInput();
    }

    private void FixedUpdate()
    {
        SetVelocity(_inputVector);
    }

    private void ProcessPlayerInput()
    {
        ProcessMovementInput();
        ProcessPointerInput();
        OnPlayerMovement?.Invoke();
    }

    private void ProcessMovementInput()
    {
        //_inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        UpdateAccelerationParametersAndState();
    }

    private void ProcessPointerInput()
    {
        _inputMouseVector = _playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
        _worldMousePos = (Camera.main.ScreenToViewportPoint(_inputMouseVector) - new Vector3(0.5f, 0.5f, 0f)) * 2;
    }

    private void UpdateAccelerationParametersAndState()
    {
        Vector2 input = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        if (input.magnitude > 0)
        {
            _isMoving = true;
            _buttonHeldTime += Time.deltaTime;
        }
        else
        {
            _isMoving = false;
            _buttonHeldTime = 0;
        }

        _inputVector = input;
    }

    private float CalculateSpeed()
    {
        if (_isMoving)
        {
            float acceleration = accelerationCurve.Evaluate(_buttonHeldTime / accelerationMaxTime);
            return maxSpeed * acceleration;
        }

        return 0;
    }


    private void SetVelocity(Vector2 inputVector)
    {
        float speed = CalculateSpeed();
        _rigidbody2D.velocity = new Vector2(inputVector.x, inputVector.y).normalized * speed;
    }

    private void EnablePlayerComponents()
    {
        // Rigidbody
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // Player Input Actions
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    /*private void VFXRenderer()
    {
        vfxRenderer.SetVector3("ColliderPos", transform.position);
    }*/

    public Vector2 GetMoveDirection()
    {
        return _inputVector;
    }

    public Vector2 GetMouseDirection()
    {
        return _worldMousePos;
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            _rigidbody2D.AddForce(-direction * knockbackPower);
        }

        yield return null;
    }
}
using System;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private PlayerInputActions _playerInputActions;
    private Camera _camera;

    [SerializeField] private float maxSpeed = 5;

    [Header("Player Acceleration Components")] [SerializeField]
    private float accelerationMaxTime = 1.25f;

    [SerializeField] private AnimationCurve accelerationCurve;

    private bool _isMoving;
    private float _timeButtonHeld;
    private const float ScreenCenterOffset = 0.5f;

    //Events
    public event Action<Vector2, Vector2> OnPlayerMovement;

    // Vectors
    private Vector2 _inputMouseVector;
    private Vector2 _previousMousePos;
    private Vector2 _worldMousePos;
    private Vector2 _inputVector;

    void Awake() {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            EnablePlayerComponents();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateInputsInformationAndInvokeEvent();
    }

    private void UpdateInputsInformationAndInvokeEvent()
    {
        _inputVector = GetMovementInputVector();
        _worldMousePos = GetPointerInputVector();

        if (_worldMousePos != _previousMousePos)
        {
            OnPlayerMovement?.Invoke(_inputVector, _worldMousePos);
            _previousMousePos = _worldMousePos;
        }
    }

    private void FixedUpdate()
    {
        SetVelocity();
    }

    private Vector2 GetPointerInputVector()
    {
        _inputMouseVector = _playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
        _worldMousePos = (_camera.ScreenToViewportPoint(_inputMouseVector) -
                          new Vector3(ScreenCenterOffset, ScreenCenterOffset, 0f)) * 2;

        return _worldMousePos;
    }

    private Vector2 GetMovementInputVector()
    {
        Vector2 input = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        _isMoving = input.magnitude > 0;

        if (_isMoving)
            _timeButtonHeld += Time.deltaTime;
        else
            _timeButtonHeld = 0;


        return input;
    }

    private float CalculateSpeed()
    {
        if (_isMoving)
        {
            float acceleration = accelerationCurve.Evaluate(_timeButtonHeld / accelerationMaxTime);
            return maxSpeed * acceleration;
        }

        return 0;
    }


    private void SetVelocity()
    {
        _rigidbody2D.velocity = _inputVector.normalized * CalculateSpeed();
    }

    private void EnablePlayerComponents()
    {
        //Camera
        _camera = Camera.main;
        // Rigid body
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // Player Input Actions
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;
        Vector2 direction = (obj.transform.position - this.transform.position).normalized;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            _rigidbody2D.AddForce(-direction * knockbackPower);
        }

        yield return null;
    }
}

// [SerializeField] private VisualEffect vfxRenderer;
/*private void VFXRenderer()
{
    vfxRenderer.SetVector3("ColliderPos", transform.position);
}*/
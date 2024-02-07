using System;
using UnityEngine;
using UnityEngine.VFX;

    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [Header("Components")]
        [SerializeField] private float speed;
        [SerializeField] Rigidbody2D rigidbody2D;
        [SerializeField] private VisualEffect vfxRenderer;
        private PlayerInputActions _playerInputActions;
        
        
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

            // Rigidbody
            rigidbody2D = GetComponent<Rigidbody2D>();

            // Player Input Actions
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        }

        private void Update()
        {
            vfxRenderer.SetVector3("ColliderPos", transform.position);
        }

        private void FixedUpdate()
        {
            Move();
        }
        
        private void Move()
        {
            // Movement
            _inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
            // Mouse/Stick
            _inputMouseVector = _playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
        
            _worldMousePos = 
                (Camera.main.ScreenToViewportPoint(_inputMouseVector) - new Vector3(0.5f, 0.5f, 0f)) * 2;

            OnPlayerMovement?.Invoke();

            rigidbody2D.velocity = new Vector2(_inputVector.x, _inputVector.y).normalized * speed;
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

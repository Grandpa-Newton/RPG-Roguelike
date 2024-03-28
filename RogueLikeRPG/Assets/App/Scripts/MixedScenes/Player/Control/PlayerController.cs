using System;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Player;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Move,
        Roll,
    }

    private PlayerState _playerState;

    private PlayerAnimator _playerAnimator;
    private PlayerHealth _playerHealth;
    private PlayerMovement _playerMovement;
    private PlayerAimWeaponRotation _playerAimWeaponRotation;
    private SwitchWeaponBetweenRaM _switchWeaponBetweenRaM;
    private PlayerWeapon _playerWeapon;

    [SerializeField] private CharacteristicValueSO healthCharacteristicSO;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private CurrentWeaponsSO currentWeaponsSO;
    [SerializeField] private InventorySO inventorySO;
    [SerializeField] private List<ItemParameter> parametersToModify;
    [SerializeField] private List<ItemParameter> itemCurrentState;

    [SerializeField] private Transform aimTransform;

    [SerializeField] private Transform meleeWeapon;
    [SerializeField] private Transform rangeWeapon;
    [SerializeField] private GameObject[] hands;

    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    private PlayerInputActions _playerInputActions;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        InitializeComponents();

        _playerHealth = new PlayerHealth(healthCharacteristicSO);

        _playerMovement = new PlayerMovement(_rigidbody2D, _playerInputActions, _camera);

        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        _playerMovement.SetVirtualCamera(_virtualCamera, transform);

        _playerAnimator = new PlayerAnimator(this, playerAnimator, _playerMovement);

        _playerWeapon = new PlayerWeapon(currentWeaponsSO, inventorySO, parametersToModify, itemCurrentState);

        _playerAimWeaponRotation = new PlayerAimWeaponRotation(_playerInputActions, aimTransform);

        SwitchWeaponBetweenRaM.Instance.Initialize(meleeWeapon, rangeWeapon, hands, currentWeaponsSO);
    }

    private void Start()
    {
        _playerState = PlayerState.Idle;
    }

    private void InitializeComponents()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _camera = Camera.main;
    }

    private void PlayerStateChanger()
    {
        switch (_playerState)
        {
            case PlayerState.Idle:
                _playerMovement.Idle();
                break;
            case PlayerState.Move:
                _playerMovement.Move();
                break;
            case PlayerState.Roll:
                _playerMovement.Roll(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GetPlayerState()
    {
        Vector2 playerMovementVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();

        _playerMovement.GetPlayerInputs();

        _playerAnimator.CheckRollEnd();

        if (_playerInputActions.Player.Roll.triggered)
        {
            _playerState = PlayerState.Roll;
        }
        else if (playerMovementVector != Vector2.zero)
        {
            _playerState = PlayerState.Move;
        }
        else
        {
            _playerState = PlayerState.Idle;
        }
    }

    private void Update()
    {
        GetPlayerState();
        _playerAimWeaponRotation.HandsRotationAroundAim(transform);
        SwitchWeaponBetweenRaM.Instance.SwapWeapon();
    }

    private void FixedUpdate()
    {
        PlayerStateChanger();
    }

    private void OnDestroy()
    {
        _playerHealth.Dispose();
        _playerMovement.Dispose();
        _playerAnimator.Dispose();
        _switchWeaponBetweenRaM.Dispose();
    }
}
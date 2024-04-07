using System;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.GameScenes.Player.UI;
using App.Scripts.GameScenes.Weapon;
using App.Scripts.GameScenes.Weapon.Bullet;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using App.Scripts.GameScenes.Weapon.RangeWeapon;
using App.Scripts.MixedScenes.Inventory.Controller;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.UI;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.GameScenes.Player
{
    public class PlayerController : MonoBehaviour
    {
        private enum PlayerState
        {
            Idle,
            Move,
            Roll,
        }
        private PlayerState _playerState;

        private PlayerHealth _playerHealth;
        private PlayerWeapon _playerWeapon;
        private PlayerMovement _playerMovement;
        private PlayerMoney _playerMoney;
    
        private PlayerAnimator _playerAnimator;
    
        private PlayerAimWeaponRotation _playerAimWeaponRotation;
        private SwitchWeaponBetweenRangeAndMelee _switchWeaponBetweenRangeAndMelee;

        private Camera _camera;
        private Rigidbody2D _rigidbody2D;
        private PlayerInputActions _playerInputActions;
        private CinemachineVirtualCamera _virtualCamera;

    
        [Title("Player Stats")] [LabelText("Health Characteristic")]
        [SerializeField] private CharacteristicValueSO healthCharacteristicSO;

        [Title("Player Transforms")]
        [SerializeField] private Transform aimTransform;
        [SerializeField] private Transform meleeWeapon;
        [SerializeField] private Transform rangeWeapon;
        [SerializeField] private Transform[] hands;

        [Title("Animators")]
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private Animator aimAnimator;
    
        [Title("Weapon Components")]
        private WeaponItemSO currentPlayerWeaponSO;
        [SerializeField] private CurrentWeaponsSO currentWeaponsSO;
        [SerializeField] private List<ItemParameter> parametersToModify;
        [SerializeField] private List<ItemParameter> itemCurrentState;
    
        [Title("Audio Sources")]
        [SerializeField] private AudioSource meleeWeaponAudioSource;
        [SerializeField] private AudioSource rangeWeaponAudioSource;
    
        [Title("Weapons Sprite Renderer Component")]
        [SerializeField] private SpriteRenderer meleeWeaponSpriteRenderer;
        [SerializeField] private SpriteRenderer rangeWeaponSpriteRenderer;
    
        [Title("Current Weapon UI")]
        [SerializeField] private Image meleeWeaponUI;
        [SerializeField] private Image meleeBackgroundImage;
        [SerializeField] private Image meleeWeaponIcon;
        [SerializeField] private Image rangeWeaponUI;
        [SerializeField] private Image rangeBackgroundImage;
        [SerializeField] private Image rangeWeaponIcon;

        [Title("Bullet Components")]
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private BulletFactory bulletFactory;

        [Title("Inventory")] 
        [SerializeField] private UIInventoryPage uiInventoryPage;
        [SerializeField] private InventorySO inventorySO;
        [SerializeField] private List<InventoryItem> inventoryItems;
        [SerializeField] private AudioClip inventoryOpenClip;
        [SerializeField] private AudioSource inventoryAudioSource;

        
        private void Awake()
        {
            InitializeComponents();

            PlayerHealth.Instance.Initialize(healthCharacteristicSO);
        
            _playerMovement = new PlayerMovement(_rigidbody2D, _playerInputActions, _camera);

            _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            _playerMovement.SetVirtualCamera(_virtualCamera, transform);

            _playerAnimator = new PlayerAnimator(playerAnimator, _playerMovement);

            PlayerAnimator.OnPlayerRolling += GetPlayerRollState;
        
            PlayerCurrentWeaponUI.Instance.Initialize(meleeWeaponUI, meleeBackgroundImage, meleeWeaponIcon,
                rangeWeaponUI, rangeBackgroundImage, rangeWeaponIcon);

            PlayerWeapon.Instance.Initialize(inventorySO, parametersToModify, itemCurrentState);

            _playerAimWeaponRotation = new PlayerAimWeaponRotation(_playerInputActions, aimTransform);

            SwitchWeaponBetweenRangeAndMelee.Instance.Initialize(meleeWeapon, rangeWeapon, hands);
            
            PlayerCurrentWeapon.Instance.Initialize(currentPlayerWeaponSO,currentWeaponsSO);

            MeleeWeapon.Instance.Initialize(_playerInputActions, meleeWeaponSpriteRenderer,
                aimAnimator,meleeWeaponAudioSource);
            RangeWeapon.Instance.Initialize(bulletPrefab, _playerInputActions, rangeWeaponSpriteRenderer,
                aimTransform,
                rangeWeaponAudioSource);
            
            SwitchWeaponBetweenRangeAndMelee.Instance.CheckAvailableWeapons();
            
            InventoryController.Instance.Initialize(uiInventoryPage,inventorySO,inventoryItems,inventoryOpenClip,inventoryAudioSource);
        }

        private bool _isRolling;

        private void GetPlayerRollState(bool isRolling)
        {
            _isRolling = isRolling;
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

            _playerAnimator.RollEndAction();


            if (_isRolling)
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
            Debug.Log(PlayerCurrentWeapon.Instance.CurrentPlayerWeapon);
            GetPlayerState();
            _playerAimWeaponRotation.HandsRotationAroundAim(transform);
            SwitchWeaponBetweenRangeAndMelee.Instance.SwapWeapon();
            HandleCombat();
            InventoryController.Instance.ShowOrHideInventory();
        }

        private void HandleCombat()
        {
            if (_isRolling) return;
        
            if (PlayerCurrentWeapon.Instance.CurrentPlayerWeapon as MeleeWeaponSO)
            {
                Debug.Log("Meleey");
                MeleeWeapon.Instance.Attack();
            }
            if (PlayerCurrentWeapon.Instance.CurrentPlayerWeapon as RangeWeaponSO)
            {
                Debug.Log("Dalniy");
                RangeWeapon.Instance.Shoot(bulletFactory);
            }
        }
        private void FixedUpdate()
        {
            PlayerStateChanger();
        }

        private void OnDestroy()
        {
            PlayerHealth.Instance.Dispose();
            _playerMovement.Dispose();
            _playerAnimator.Dispose();
            SwitchWeaponBetweenRangeAndMelee.Instance.Dispose();
            InventoryController.Instance.Dispose();
        }
    }
}
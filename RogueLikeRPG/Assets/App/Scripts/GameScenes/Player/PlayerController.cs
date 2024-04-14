using System;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Inventory.Controller;
using App.Scripts.GameScenes.Inventory.Model;
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
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.GameScenes.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        
        private Camera _camera;
        private Rigidbody2D _rigidbody2D;
        private PlayerInputActions _playerInputActions;
        private CinemachineVirtualCamera _virtualCamera;

        [Title("Player Stats")] 
        [LabelText("Health")] [SerializeField] private CharacteristicValueSO healthCharacteristicSO;

        [Title("Player Transforms")] 
        [SerializeField] private Transform aimTransform;
        [SerializeField] private Transform meleeWeapon;
        [SerializeField] private Transform rangeWeapon;
        [SerializeField] private Transform[] hands;

        [Title("Animators")] 
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private Animator aimAnimator;

        [Title("Weapon Components")] 
        [SerializeField] private CurrentWeaponsSO currentWeaponsSO;
        [SerializeField] private List<ItemParameter> parametersToModify;
        [SerializeField] private List<ItemParameter> itemCurrentState;

        [Title("Audio Sources")] 
        [SerializeField] private AudioSource meleeWeaponAudioSource;
        [SerializeField] private AudioSource rangeWeaponAudioSource;

        [Title("Weapons Sprite Renderer Component")]
        [SerializeField] private SpriteRenderer meleeWeaponSpriteRenderer;
        [SerializeField] private SpriteRenderer rangeWeaponSpriteRenderer;

        [Title("Bullet Components")] 
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private BulletFactory bulletFactory;
        
        [Title("Inventory")] 
        [SerializeField] private InventorySO inventorySO;

        public event Action<Transform> OnPlayerHandsRotation;
        public event Action OnPlayerSwapWeapon;
        public event Action OnPlayerShowOrHideInventory;
        public event Action OnPlayerHandleCombat;
        public event Action OnPlayerUpdatePlayerState;

        private void Awake()
        {
            if (!Instance) Instance = this;
            InitializeUnityComponents();
            InitializePlayerComponents();
            InitializeWeaponComponents();
        }

        private void InitializeUnityComponents()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            _camera = Camera.main;
            
            _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            PlayerMovement.Instance.SetVirtualCamera(_virtualCamera, transform);

        }

        private void InitializePlayerComponents()
        {
            PlayerHealth.Instance.Initialize(healthCharacteristicSO);
            PlayerMovement.Instance.Initialize(_rigidbody2D, _playerInputActions, _camera);
            PlayerAnimator.Instance.Initialize(playerAnimator);
            
            PlayerUIInitializer.Instance.InitializeUIComponents();
            
            PlayerStateChanger.Instance.Initialize(_playerInputActions);
            PlayerCombat.Instance.Initialize(bulletFactory);
        }

        private void InitializeWeaponComponents()
        {
            PlayerWeapon.Instance.Initialize(inventorySO, parametersToModify, itemCurrentState);
            PlayerAimWeaponRotation.Instance.Initialize(_playerInputActions, aimTransform);
            
            PlayerWeaponSwitcher.Instance.Initialize(meleeWeapon, rangeWeapon, hands);
            PlayerCurrentWeapon.Instance.Initialize(currentWeaponsSO);
            MeleeWeapon.Instance.Initialize(_playerInputActions, meleeWeaponSpriteRenderer, aimAnimator,
                meleeWeaponAudioSource);
            RangeWeapon.Instance.Initialize(bulletPrefab, _playerInputActions, rangeWeaponSpriteRenderer, aimTransform,
                rangeWeaponAudioSource);
            PlayerWeaponSwitcher.Instance.CheckAvailableWeapons();
        }
       
        private void Update()
        {
            OnPlayerUpdatePlayerState?.Invoke();
            OnPlayerHandsRotation?.Invoke(transform);
            OnPlayerSwapWeapon?.Invoke();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OnPlayerShowOrHideInventory?.Invoke();
            }
            OnPlayerHandleCombat?.Invoke();
        }
        private void FixedUpdate()
        {
            PlayerStateChanger.Instance.ChangePlayerState();
        }
        private void OnDestroy()
        {
            PlayerHealth.Instance.Dispose();
            PlayerMovement.Instance.Dispose();
            PlayerAnimator.Instance.Dispose();
            PlayerStateChanger.Instance.Dispose();
            PlayerCombat.Instance.Dispose();
            PlayerAimWeaponRotation.Instance.Dispose();
            PlayerWeaponSwitcher.Instance.Dispose();
            PlayerInventoryUI.Instance.Dispose();
        }
    }
}
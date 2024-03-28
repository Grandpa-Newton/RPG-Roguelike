using System;
using App.Scripts.MixedScenes.Player;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon
{
    public class SwitchWeaponBetweenRangeAndMelee : MonoBehaviour
    {
        public static SwitchWeaponBetweenRangeAndMelee Instance { get; private set; }

        [SerializeField] private float swapReloadTime = 0.5f;
        [SerializeField] private Transform meleeWeapon;
        [SerializeField] private Transform rangeWeapon;

        [SerializeField] private GameObject[] hands;

        [SerializeField] private GameObject currentPickedWeapon;
        [SerializeField] private CurrentWeaponsSO currentWeaponsSO;

        private bool isMeleeWeapon = true;
        private bool isRolling;
        private float timerToSwapWeapon = 0f;

        private void Awake()
        {
            Instance = this;
            PlayerAnimatorOld.OnPlayerRolling += SetRollingState;
        }

        private void SetRollingState(bool isRolling)
        {
            this.isRolling = isRolling;
        }

        private void Start()
        {
            if (!currentWeaponsSO.EquipRangeWeapon && !currentWeaponsSO.EquipMeleeWeapon)
            {
                PlayerHandsVisible(false);
                return;
            }

            if (currentWeaponsSO.EquipMeleeWeapon)
            {
                PlayerCurrentWeaponUI.Instance.SetMeleeWeaponIcon(currentWeaponsSO.EquipMeleeWeapon.ItemImage);
                meleeWeapon.gameObject.SetActive(true);
                rangeWeapon.gameObject.SetActive(false);
                currentPickedWeapon = meleeWeapon.gameObject;
                PlayerCurrentWeaponUI.Instance.IncreaseMeleeWeaponScale();
            }

            if (currentWeaponsSO.EquipRangeWeapon)
            {
                PlayerCurrentWeaponUI.Instance.SetRangeWeaponIcon(currentWeaponsSO.EquipRangeWeapon.ItemImage);
                if (!currentWeaponsSO.EquipMeleeWeapon)
                {
                    meleeWeapon.gameObject.SetActive(false);
                    rangeWeapon.gameObject.SetActive(true);
                    currentPickedWeapon = rangeWeapon.gameObject;
                    PlayerCurrentWeaponUI.Instance.IncreaseRangeWeaponScale();
                }
            }
        }

        private void Update()
        {
            SwapWeapon();
        }

        private void SwapWeapon()
        {
            if (!currentWeaponsSO.EquipMeleeWeapon || !currentWeaponsSO.EquipRangeWeapon)
            {
                return;
            }

            timerToSwapWeapon += Time.deltaTime;
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && !isRolling && timerToSwapWeapon > swapReloadTime)
            {
                isMeleeWeapon = !isMeleeWeapon;
                if (isMeleeWeapon)
                {
                    PlayerCurrentWeaponUI.Instance.IncreaseMeleeWeaponScale();
                    SetActiveMeleeWeapon();
                }
                else
                {
                    PlayerCurrentWeaponUI.Instance.IncreaseRangeWeaponScale();
                    SetActiveRangeWeapon();
                }

                timerToSwapWeapon = 0f;
            }
        }

        public void SetActiveMeleeWeapon()
        {
            meleeWeapon.gameObject.SetActive(true);
            rangeWeapon.gameObject.SetActive(false);
            
            currentPickedWeapon = meleeWeapon.gameObject;
            
            if (!currentWeaponsSO.EquipMeleeWeapon)
            {
                PlayerHandsVisible(true);
            }
        }

        public void SetActiveRangeWeapon()
        {
            rangeWeapon.gameObject.SetActive(true);
            meleeWeapon.gameObject.SetActive(false);
            
            currentPickedWeapon = rangeWeapon.gameObject;
            
            if (!currentWeaponsSO.EquipRangeWeapon)
            {
                PlayerHandsVisible(true);
            }
        }

        public void WeaponAndHandsDisable()
        {
            if (!currentPickedWeapon) return;
            
            currentPickedWeapon.SetActive(false);
            PlayerHandsVisible(false);
        }

        public void WeaponAndHandsEnable()
        {
            if (!currentPickedWeapon) return;
            
            currentPickedWeapon.SetActive(true);
            PlayerHandsVisible(true);
        }


        public bool GetWeaponState()
        {
            return isMeleeWeapon;
        }

        public void PlayerHandsVisible(bool isActive)
        {
            foreach (var hand in hands)
            {
                hand.SetActive(isActive);
            }
        }

        private void OnDestroy()
        {
            PlayerAnimatorOld.OnPlayerRolling -= SetRollingState;
        }
    }
}
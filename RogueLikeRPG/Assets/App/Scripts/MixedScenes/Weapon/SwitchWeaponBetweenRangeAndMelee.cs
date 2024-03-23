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
            PlayerAnimator.OnPlayerRolling += SetRollingState;
        }

        private void SetRollingState(bool isRolling)
        {
            this.isRolling = isRolling;
        }

        private void Start()
        {
            if (currentWeaponsSO.EquipMeleeWeapon)
            {
                PlayerCurrentWeaponUI.Instance.SetMeleeWeaponIcon(currentWeaponsSO.EquipMeleeWeapon.ItemImage);
            }

            if (currentWeaponsSO.EquipRangeWeapon)
            {
                PlayerCurrentWeaponUI.Instance.SetRangeWeaponIcon(currentWeaponsSO.EquipRangeWeapon.ItemImage);
            }


            meleeWeapon.gameObject.SetActive(true);
            rangeWeapon.gameObject.SetActive(false);
            PlayerCurrentWeaponUI.Instance.IncreaseMeleeWeaponScale();
            PlayerHandsVisible(false);
        }

        private void Update()
        {
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
        }

        public void SetActiveRangeWeapon()
        {
            meleeWeapon.gameObject.SetActive(false);
            rangeWeapon.gameObject.SetActive(true);
            currentPickedWeapon = rangeWeapon.gameObject;
        }

        public void WeaponAndHandsDisable()
        {
            currentPickedWeapon.SetActive(false);
            PlayerHandsVisible(false);
        }

        public void WeaponAndHandsEnable()
        {
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
            PlayerAnimator.OnPlayerRolling -= SetRollingState;
        }
    }
}
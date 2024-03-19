using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon
{
    public class SwitchWeaponBetweenRangeAndMelee : MonoBehaviour
    {
        public static SwitchWeaponBetweenRangeAndMelee Instance { get; private set; }
        [SerializeField] private Transform meleeWeapon;
        [SerializeField] private Transform rangeWeapon;

        [SerializeField] private GameObject[] hands;

        private bool isMeleeWeapon = true;

        private void Awake()
        {
            Instance = this;
        }
    
        private void Start()
        {
            meleeWeapon.gameObject.SetActive(true);
            rangeWeapon.gameObject.SetActive(false);
            PlayerHandsVisible(false);
        }

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                isMeleeWeapon = !isMeleeWeapon;
                if (isMeleeWeapon)
                {
                    meleeWeapon.gameObject.SetActive(true);
                    rangeWeapon.gameObject.SetActive(false);
                }
                else
                {
                    meleeWeapon.gameObject.SetActive(false);
                    rangeWeapon.gameObject.SetActive(true);
                }
            }
        }

        public void SetActiveMeleeWeapon()
        {
            meleeWeapon.gameObject.SetActive(true);
            rangeWeapon.gameObject.SetActive(false);
        }
        public void SetActiveRangeWeapon()
        {
            meleeWeapon.gameObject.SetActive(false);
            rangeWeapon.gameObject.SetActive(true);
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
    }
}
using App.Scripts.GameScenes.Weapon;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.GameScenes.Player.UI
{
    public class PlayerCurrentWeaponUI
    {
        private static PlayerCurrentWeaponUI _instance;
        public static PlayerCurrentWeaponUI Instance => _instance ??= new PlayerCurrentWeaponUI();

        private CanvasGroup _meleeWeaponUI;
        private CanvasGroup _rangeWeaponUI;
        private Image _meleeWeaponIcon;
        private Image _rangeWeaponIcon;

        public void Initialize(CanvasGroup meleeWeaponUI, CanvasGroup rangeWeaponUI, Image meleeWeaponIcon, Image rangeWeaponIcon)
        {
            _meleeWeaponUI = meleeWeaponUI;
            _rangeWeaponUI = rangeWeaponUI;
            _meleeWeaponIcon = meleeWeaponIcon;
            _rangeWeaponIcon = rangeWeaponIcon;
        }
        public void AdjustWeaponScale(WeaponItemSO weaponItemSO)
        {
            bool isMeleeWeapon = weaponItemSO is MeleeWeaponSO;
            CanvasGroup selectedWeaponUI = isMeleeWeapon ? _meleeWeaponUI : _rangeWeaponUI;
            CanvasGroup deselectedWeaponUI = isMeleeWeapon ? _rangeWeaponUI : _meleeWeaponUI;

            selectedWeaponUI.transform.DOScale(1.2f, 0.5f);
            selectedWeaponUI.DOFade(1f, 0.5f);
            deselectedWeaponUI.transform.DOScale(0.8f, 0.5f);
            deselectedWeaponUI.DOFade(0.5f, 0.5f);
        }

        public void SetWeaponIcon(WeaponItemSO weaponItemSO)
        {
            bool isMeleeWeapon = weaponItemSO is MeleeWeaponSO;
            Image weaponIcon = isMeleeWeapon ? _meleeWeaponIcon : _rangeWeaponIcon;
            weaponIcon.sprite = weaponItemSO.ItemImage;
        }
    }
}
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.Components
{
    public class PlayerAimWeaponRotation
    {
        private static PlayerAimWeaponRotation _instance;
        public static PlayerAimWeaponRotation Instance => _instance ??= new PlayerAimWeaponRotation();
        
        private PlayerInputActions _playerInputActions;
        private Transform _aimTransform;

        private bool _canRotate;
        
        public void Initialize(PlayerInputActions playerInputActions, Transform aimTransform)
        {
            _playerInputActions = playerInputActions;
            _aimTransform = aimTransform;
            PlayerController.Instance.OnPlayerHandsRotation += HandsRotationAroundAim;
            MeleeWeapon.Instance.OnPlayerAttack += GetPlayerRotationInfo;
        }

        private void GetPlayerRotationInfo(bool canRotate)
        {
            _canRotate = canRotate;
        }

        private void HandsRotationAroundAim(Transform playerTransform)
        {
            if (!_canRotate)
                return;
            Vector3 mousePosition = GetMouseWorldPosition(_playerInputActions);

            Vector3 aimDirection = (mousePosition - playerTransform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            _aimTransform.eulerAngles = new Vector3(0, 0, angle);

            Vector3 localScale = Vector3.one;
            if (angle > 90 || angle < -90)
            {
                localScale.y = -1f;
            }
            else
            {
                localScale.y = +1f;
            }

            _aimTransform.localScale = localScale;
        }
        private static Vector3 GetMouseWorldPosition(PlayerInputActions playerInputActions)
        {
            Vector2 mousePosition = playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f;
            return worldPosition;
        }

        private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        public void Dispose()
        {
            PlayerController.Instance.OnPlayerHandsRotation -= HandsRotationAroundAim;

        }
    }
}

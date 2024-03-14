using System;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private Transform _aimTransform;
    
    //Scriptable Objects
    private BulletSO _bulletSo;
    private RangeWeaponSO _rangeWeaponSo;
    
    private void Awake()
    {
        _aimTransform = transform.Find("Aim");
    }

    private void Start()
    {
        _playerInputActions = InputManager.Instance.PlayerInputActions;
    }

    private void Update()
    {
        HandleAiming();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition(_playerInputActions);

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
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
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private MeleeWeaponSO _meleeWeaponData;
    public WeaponSO WeaponData => _meleeWeaponData;
    [SerializeField] private Animator _animator;
    private PlayerInputActions _playerInputActions;

    private float _timeToNextAttack = 0;
    private Vector2 _defaultPosition;
    private Vector2 _currentPointPosition;

    public MeleeWeapon(MeleeWeaponSO data)
    {
        _meleeWeaponData = data;
    }

    private void Awake()
    {
        //_playerInputActions = InputManager.Instance.PlayerInputActions;
        GetComponent<SpriteRenderer>().sprite = _meleeWeaponData.weaponSprite;
    }

    private void Start()
    {
        _playerInputActions = InputManager.Instance.PlayerInputActions;
    }

    private void Update()
    {
        DealDamage();
    }
    public void DealDamage()
    {
        _timeToNextAttack += Time.deltaTime;
        if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextAttack > _meleeWeaponData.attackRate)
        {
            _animator.SetTrigger("Shoot");
            _timeToNextAttack = 0;
            StartCoroutine(WaitToNextAttack());
            // _rb.AddForce(_aimTransform.position * _forceMultiplier, ForceMode2D.Impulse);
            _animator.SetTrigger("Idle");
        }
    }

    IEnumerator WaitToNextAttack()
    {
        yield return new WaitForSeconds(_meleeWeaponData.attackRate);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_meleeWeaponData.damage);
        }
    }
}
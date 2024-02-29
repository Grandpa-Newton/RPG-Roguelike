using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private MeleeWeaponSO _meleeWeaponData;
    public WeaponSO WeaponData => _meleeWeaponData;

    [SerializeField] private Transform attackPoint;
    private float _timeToNextAttack = 0;
    private PlayerInputActions _playerInputActions;
    [SerializeField] private Animator _animator;
    private bool _isMovingToAttackPoint = true;
    private bool _isMoving = false;
    private Vector2 _defaultPosition;
    private Vector2 _currentPointPosition;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _aimTransform;

    public MeleeWeapon(MeleeWeaponSO data)
    {
        _meleeWeaponData = data;
    }

    private void Awake()
    {
        _playerInputActions = InputManager.Instance.PlayerInputActions;
        GetComponent<SpriteRenderer>().sprite = _meleeWeaponData.weaponSprite;
    }

    private void Start()
    {
    }

    private void Update()
    {
        DealDamage();
        if (_isMoving)
        {
            if (_isMovingToAttackPoint)
            {
                if ((Vector2)(transform.position) != _currentPointPosition)
                {
                    Debug.Log("Transform" + (Vector2)transform.position);
                    Debug.Log("Point" + _currentPointPosition);
                    var moveDirection = Vector2.MoveTowards(transform.position, _currentPointPosition,
                        _speed * Time.deltaTime);
                    transform.position = moveDirection;
                }
                else
                {
                    _isMovingToAttackPoint = false;
                }
            }
            else
            {
                if ((Vector2)transform.position != _defaultPosition)
                {
                    var moveDirection = Vector2.MoveTowards(transform.position, _defaultPosition,
                        _speed * Time.deltaTime);
                    transform.position = moveDirection;
                }
                else
                {
                    _isMovingToAttackPoint = true;
                    _timeToNextAttack = 0;
                    _isMoving = false;
                }
            }
        }
    }

    IEnumerator MoveObject(Transform objectToMove, Vector3 end, float duration)
    {
        // Начальная позиция
        Vector3 start = objectToMove.position;

        // Время начала перемещения
        float startTime = Time.time;

        // Время окончания перемещения
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            // Вычисляем, сколько времени прошло с начала перемещения
            float t = (Time.time - startTime) / duration;

            // Плавно перемещаем объект из начальной позиции в конечную
            objectToMove.position = Vector3.Lerp(start, end, t);

            // Возвращаем null, чтобы продолжить выполнение корутины в следующем кадре
            yield return null;
        }

        // Убеждаемся, что объект точно достиг конечной позиции
        objectToMove.position = end;
    }

    [SerializeField] private float _forceMultiplier = 10f;
    private Rigidbody2D _rb;

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
    /*public void Deapublic void DealDamage()
                         {
                             _timeToNextAttack += Time.deltaTime;
                             if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextAttack > _meleeWeaponData.attackRate)
                             {
                                 _animator.SetTrigger("Shoot");
                                 _timeToNextAttack = 0;
                                 // _rb.AddForce(_aimTransform.position * _forceMultiplier, ForceMode2D.Impulse);
                                 _animator.SetTrigger("Idle");
                             }
                         }
                         /*public void DealDamage()
    {
        _timeToNextAttack += Time.deltaTime;
        if (!_isMoving && _playerInputActions.Player.Attack.IsPressed() && _timeToNextAttack > _meleeWeaponData.attackRate)
        {
            _defaultPosition = transform.position;
            _currentPointPosition = attackPoint.position;
            _isMovingToAttackPoint = true;
            _isMoving = true;
        }
        /* Vector3 attackDirection;
        _timeToNextAttack += Time.deltaTime;
        if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextAttack > _meleeWeaponData.attackRate)
        {
            //float step = Time.deltaTime * 10;
            //gameObject transformLocal = transform;
            StartCoroutine(MoveObject(transform, attackPoint.position , 0.15f)); // добовлять вектор направления а не 0.5 0 0
            //transform.position = Vector3.MoveTowards(transform.position, attackPoint.position, step);
            //transform.position = transformLocal;
            _timeToNextAttack = 0f;

        }#1#
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Enemy>().TakeDamage(_meleeWeaponData.damage);
    }
}
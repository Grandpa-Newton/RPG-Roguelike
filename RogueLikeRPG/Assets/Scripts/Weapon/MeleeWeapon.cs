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

    private void Update()
    {
        DealDamage();
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
    public void DealDamage()
    {
        Vector3 attackDirection;
        _timeToNextAttack += Time.deltaTime;
        if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextAttack > _meleeWeaponData.attackRate)
        {
            //float step = Time.deltaTime * 10;
            //gameObject transformLocal = transform;
            StartCoroutine(MoveObject(transform, attackPoint.position, 0.15f)); // добовлять вектор направления а не 0.5 0 0
            //transform.position = Vector3.MoveTowards(transform.position, attackPoint.position, step);
            //transform.position = transformLocal;
            _timeToNextAttack = 0f;
            
        }
    }
}
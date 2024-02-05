using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator _playerAttackAnimator;
    private PlayerInputActions _playerInputActions;

    [SerializeField] private float meleeSpeed;
    [SerializeField] private float damage;

    private float timeUntilMelee;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void Update()
    {
        if(timeUntilMelee <= 0f)
        {
            if (_playerInputActions.Player.Attack.IsPressed())
            {
                _playerAttackAnimator.SetTrigger("AttackTrigger");
                timeUntilMelee = meleeSpeed;
            }
            
        }
        else
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("Enemy hit");
        }
    }
}

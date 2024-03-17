using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStumpAnimation : MonoBehaviour
{
    private EnemyAI _enemy;
    private Animator _enemyAnimator;
    private bool _isWalking = false;
    
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemy = GetComponent<EnemyAI>();
        _enemy.OnEnemyMovement += OnEnemyStartMoving;
    }

    private void OnEnemyStartMoving(Vector2 direction)
    {
        if (direction.x != 0 || direction.y != 0)
        {
            if (!_isWalking)
            {
                _isWalking = true;
                _enemyAnimator.SetBool(IsMoving, _isWalking);
            }
        }
        else
        {
            if (_isWalking)
            {
                _isWalking = false;
                _enemyAnimator.SetBool(IsMoving, _isWalking);
            }
        }
    }
    private void OnDestroy()
    {
        _enemy.OnEnemyMovement -= OnEnemyStartMoving;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToMouse : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector3 _target;

    private void Start()
    {
        _target = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _target.z = transform.position.z;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target, _moveSpeed * Time.deltaTime);
    }
}
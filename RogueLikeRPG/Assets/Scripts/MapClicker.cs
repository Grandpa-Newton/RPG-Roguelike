using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClicker : MonoBehaviour
{
    
    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;

    private IBaseCell _interactingCell;

    private Vector2 clickedCellPosition;


    private bool _isMoving = false;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isMoving)
        {
            Debug.Log("In Input");
            RaycastHit2D raycastHit;
            //RaycastHit raycastHit;

            float rayDistance = 100.0f;

            raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, rayDistance);

            if (raycastHit)
            {
                if(raycastHit.transform != null)
                {
                    Debug.Log(raycastHit.transform.gameObject);
                    clickedCellPosition = (Vector2)raycastHit.transform.position;
                    _interactingCell = raycastHit.transform.gameObject.GetComponent<IBaseCell>(); // ГДЕ-ТО ТУТ ПРОВЕРКА НА ТО, ЧТО ЭТО BASECELL!
                    _isMoving = true;
                }
            }
        }
        
        // If player can move
        if (_isMoving)
        {
            Debug.Log(transform.position + "\n" + clickedCellPosition + "\n====");
            Debug.Log(_interactingCell.IsActive + "!!!");
            // If the player is not standing on the cell AND picked cell is Active
            if ((Vector2)transform.position != clickedCellPosition && _interactingCell.IsActive )
            {
                // Detecting Move Direction for Player
                _moveDirection = Vector2.MoveTowards(transform.position, clickedCellPosition, _speed * Time.deltaTime);

                transform.position = _moveDirection;
            }
            // 
            else
            {
                // РАБОТАЕТ НЕ ПРАВИЛЬНО (ВЫВОД НАДПИСЕЙ ПОЛУЧИЛОСЬ ЛИ ДОЙТИ ИЛИ НЕТ)
                _interactingCell.Interact();
                _isMoving = false;
            }
        }
    }
}

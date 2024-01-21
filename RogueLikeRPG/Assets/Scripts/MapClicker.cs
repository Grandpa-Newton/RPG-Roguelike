using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClicker : MonoBehaviour
{
    private bool _isMoving = false;

    private Vector2 clickedCellPosition;

    private IBaseCell _interactingCell;

    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;

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
                    _interactingCell = raycastHit.transform.gameObject.GetComponent<IBaseCell>();
                    _isMoving = true;
                }
            }
        }

        if (_isMoving)
        {

            if ((Vector2)transform.position != clickedCellPosition)
            {
                Debug.Log(transform.position);
                var step = _speed * Time.deltaTime;
                _moveDirection = Vector2.MoveTowards(transform.position, clickedCellPosition, step);
                transform.position = _moveDirection;
                Debug.Log(_moveDirection);
                if (_moveDirection.x != 0 || _moveDirection.y != 0)
                {
                }
                //_rb.velocity = _moveDirection;
            }
            else
            {
                _interactingCell.Interact();
                _isMoving = false;
            }
        }
    }
}

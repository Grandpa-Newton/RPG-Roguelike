using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMover : MonoBehaviour
{
    private Transform _clickedCellTransform;
    public Transform ClickedCellTransform 
    { 
        get { return _clickedCellTransform; }
        set 
        {
            _isMoving = true;
            _clickedCellTransform = value;
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Camera Mover Instance");
        }

        Instance = this;
    }

    private void Start()
    {
        transform.position = MapPlayerController.Instance.transform.position;
    }

    public static CameraMover Instance;

    private bool _isMoving;

    private Vector2 _moveDirection;

    [SerializeField]
    private float _speed = 25f;
    public void MoveTo()
    {
        if (ClickedCellTransform != null)
        {
            if ((Vector2)transform.position != (Vector2)ClickedCellTransform.position)
            {
                _moveDirection = Vector2.MoveTowards(transform.position, (Vector2)ClickedCellTransform.position, _speed * Time.deltaTime);
                transform.position = _moveDirection;
            }
            else
            {
                _isMoving = false;
            }
        }
    }

    private void Update()
    {
        if (_isMoving)
        {
            MoveTo();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerController : MonoBehaviour
{
    public static MapPlayerController Instance;
    public event Action OnActiveCell;
    
    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;

    private IBaseCell _interactingCell;

    private Vector2 clickedCellPosition;


    private bool _isMoving = false;
    private Vector2 _moveDirection;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is no more than one MaoPlayerController instance");
        }
        Instance = this;

        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        OnActiveCell?.Invoke();
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
                    Transform interactingCellTransform = raycastHit.transform;
                    _interactingCell = interactingCellTransform.gameObject.GetComponent<IBaseCell>(); // ГДЕ-ТО ТУТ ПРОВЕРКА НА ТО, ЧТО ЭТО BASECELL!
                    if (_interactingCell.IsActive)
                    {
                        MapLoader mapLoader = GameObject.Find("MapLoader").GetComponent<MapLoader>();
                        NormalCell currentCell = mapLoader.CurentCell.GetComponent<NormalCell>(); // слишком много getcomponent, но как без них - не знаю

                        foreach (GameObject neighbor in currentCell.NeighborsCells) // ПОМЕНЯТЬ НА IBASECELL
                        {
                            neighbor.GetComponent<NormalCell>().IsActive = false; // ПОМЕНЯТЬ НА IBASECELL
                        }

                        mapLoader.CurentCell = interactingCellTransform;


                        // УБРАТЬ ЦИКЛ НИЖЕ, ЭТО ДЛЯ ТЕСТА!

                        foreach (GameObject neighbor in interactingCellTransform.GetComponent<NormalCell>().NeighborsCells) 
                        {
                            neighbor.GetComponent<NormalCell>().IsActive = true; 
                        }

                        //foreach()
                        clickedCellPosition = (Vector2)raycastHit.transform.position;

                        _isMoving = true;
                        OnActiveCell?.Invoke();
                    }
                    else
                    {
                        Debug.Log("The cell is not active");
                    }
                }
            }
        }
        
        // If player can move
        if (_isMoving)
        {
            // If the player is not standing on the cell AND picked cell is Active
            if ((Vector2)transform.position != clickedCellPosition)
            {
                // Detecting Move Direction for Player
                _moveDirection = Vector2.MoveTowards(transform.position, clickedCellPosition, _speed * Time.deltaTime);
                transform.position = _moveDirection;
            }
            // 
            else
            {
                _interactingCell.Interact();
                _isMoving = false;
            }
        }
    }
}

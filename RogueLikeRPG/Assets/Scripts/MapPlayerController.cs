using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerController : MonoBehaviour
{
    public static MapPlayerController Instance;
    public event Action OnActiveCell;
    public event Action OnCurrentCell; // когда пользователь встал на новую клетку (может быть, поменять название)

    public event Action OnInteractCell;
    
    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] LayerMask _layerMask;

    private BaseCell _interactingCell;

    private Transform _clickedCellTransform;

    private bool _isMoving = false;
    private Vector2 _moveDirection;

    private void Awake()
    {

        if (Instance != null)
        {
            Debug.LogError("There is no more than one Player instance");
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

            float rayDistance = 100.0f;

            raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, rayDistance, _layerMask);

            if (raycastHit)
            {
                if(raycastHit.transform != null)
                {
                    Debug.Log(raycastHit.transform.gameObject);
                    Transform interactingCellTransform = raycastHit.transform;
                    if (interactingCellTransform.gameObject.TryGetComponent(out _interactingCell))
                    {
                        if (_interactingCell.CellType == CellType.Active)
                        {
                            BaseCell[] cells = UnityEngine.Object.FindObjectsOfType<BaseCell>(); // мб как-то поменять / вынести в отдельный метод

                            BaseCell currentCell = FindCurrentCell(cells); // ПРОВЕРКА НА != NULL

                            if(currentCell != null)
                            {
                                foreach (GameObject neighbor in currentCell.NeighborsCells)
                                {
                                    neighbor.GetComponent<BaseCell>().CellType = CellType.Inactive;
                                }

                                currentCell.CellType = CellType.Inactive;

                                MapLoader.CurrentCellId = interactingCellTransform.GetComponent<BaseCell>().CellId;


                                // УБРАТЬ ЦИКЛ НИЖЕ, ЭТО ДЛЯ ТЕСТА!

                                /*foreach (GameObject neighbor in interactingCellTransform.GetComponent<NormalCell>().NeighborsCells)
                                {
                                    neighbor.GetComponent<NormalCell>().IsActive = true;
                                }*/

                                _clickedCellTransform = raycastHit.transform;

                                _isMoving = true;

                                _interactingCell.CellType = CellType.Active;

                            }
                            else
                            {
                                Debug.Log("BaseCell Component is not attached to object");
                            }
                        }
                        else
                        {
                            Debug.Log("The cell is not active");
                        }
                    }
                }
            }
        }
        
        if (_isMoving)
        {
            if ((Vector2)transform.position != (Vector2)_clickedCellTransform.position)
            {
                _moveDirection = Vector2.MoveTowards(transform.position, (Vector2)_clickedCellTransform.position, _speed * Time.deltaTime);
                transform.position = _moveDirection;
            }
            else
            {
                _clickedCellTransform.GetComponent<StartNextLevel>().InCurrentCell();
                _interactingCell.CellType = CellType.Current;
                _interactingCell.Interact();
                _isMoving = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteractCell?.Invoke();
            }
        }
    }

    private BaseCell FindCurrentCell(BaseCell[] cells)
    {
        foreach (var cell in cells)
        {
            if (cell.CellId == MapLoader.CurrentCellId)
            {
                return cell;
            }
        }

        return null;
    }
}

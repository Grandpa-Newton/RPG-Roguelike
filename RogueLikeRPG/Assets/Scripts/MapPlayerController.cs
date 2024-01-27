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

    private IBaseCell _interactingCell;

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
            //RaycastHit raycastHit;

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
                        if (_interactingCell.IsActive)
                        {
                            //MapLoader mapLoader = GameObject.Find("MapLoader").GetComponent<MapLoader>();

                            NormalCell[] cells = UnityEngine.Object.FindObjectsOfType<NormalCell>(); // мб как-то поменять / вынести в отдельный метод

                            /* NormalCell currentCell;

                             foreach (var cell in cells)
                             {
                                 if (cell.CellId == MapLoader.CurrentCellId)
                                 {
                                     currentCell = cell;
                                     break;
                                 }
                             }*/

                            NormalCell currentCell = FindCurrentCell(cells); // ПРОВЕРКА НА != NULL

                            // currentCell = GameObject.Find(MapLoader.CurrentCellName).GetComponent<NormalCell>(); // слишком много getcomponent, но как без них - не знаю

                            //NormalCell currentCell = MapLoader.CurrentCell.GetComponent<NormalCell>();

                            foreach (GameObject neighbor in currentCell.NeighborsCells) // ПОМЕНЯТЬ НА IBASECELL
                            {
                                neighbor.GetComponent<NormalCell>().IsActive = false; // ПОМЕНЯТЬ НА IBASECELL
                            }

                            MapLoader.CurrentCellId = interactingCellTransform.GetComponent<NormalCell>().CellId;


                            // УБРАТЬ ЦИКЛ НИЖЕ, ЭТО ДЛЯ ТЕСТА!

                            /*foreach (GameObject neighbor in interactingCellTransform.GetComponent<NormalCell>().NeighborsCells)
                            {
                                neighbor.GetComponent<NormalCell>().IsActive = true;
                            }*/

                            //foreach()
                            _clickedCellTransform = raycastHit.transform;

                            _isMoving = true;
                            OnActiveCell?.Invoke();
                        }
                        else
                        {
                            Debug.Log("The cell is not active");
                        }
                    }
                }
                    // _interactingCell = interactingCellTransform.gameObject.GetComponent<IBaseCell>(); // ГДЕ-ТО ТУТ ПРОВЕРКА НА ТО, ЧТО ЭТО BASECELL!
                   
            }
        }
        
        // If player can move
        if (_isMoving)
        {
            // If the player is not standing on the cell AND picked cell is Active
            if ((Vector2)transform.position != (Vector2)_clickedCellTransform.position)
            {
                // Detecting Move Direction for Player
                _moveDirection = Vector2.MoveTowards(transform.position, (Vector2)_clickedCellTransform.position, _speed * Time.deltaTime);
                transform.position = _moveDirection;
            }
            // 
            else
            {
                _clickedCellTransform.GetComponent<StartNextLevel>().InCurrentCell();
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

    private NormalCell FindCurrentCell(NormalCell[] cells) // поменять на IBaseCell
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

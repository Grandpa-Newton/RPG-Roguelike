using Cinemachine;
using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapPlayerController : MonoBehaviour
{
    public static MapPlayerController Instance;

    public event Action OnInteractCell;

    public event Action OnDeselectCells;

    public event Action OnStartingSelectCell; // при старте выборе клетки

    public event Action OnSelectingCell; // после выбора клетки


    [SerializeField] private float _speed;
    [SerializeField] private GameObject _camera;
    [SerializeField] private Transform _followObject;

    private BaseCell _interactingCell;

    private Transform _clickedCellTransform;

    private PlayerInputActions _playerInputActions;

    private CinemachineVirtualCamera _virtualCamera;

    private bool _isMoving = false;

    private Vector2 _moveDirection;

    private bool _isSelecting = false; // производит ли сейчас выбор клетки пользователь

    private GameObject _selectingCell;

    private BaseCell.Path _currentPath;

    private int _wayPointIndex = -1;

    private List<GameObject> _activeCells = new List<GameObject>();

    private Transform _wayPointTransform;

    [HideInInspector]
    public GameObject SelectingCell
    {
        get
        {
            return _selectingCell;
        }
        set
        {
            _selectingCell = value;
            if (value != null)
            {
                // мб сюда перенести isselecting = true??
                _virtualCamera.Follow = CameraMover.Instance.transform;
                CameraMover.Instance.ClickedCellTransform = _selectingCell.transform;
            }
            else
            {
                CameraMover.Instance.ClickedCellTransform = _followObject; // если Selecting Cell обращается в ноль, то камера фокусируется на followObject (игрок) 
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is no more than one Player instance");
        }

        Instance = this;

        _playerInputActions = new PlayerInputActions();
    }


    private void Start()
    {
        /*_playerInputActions.Map.Enable();

        _playerInputActions.Map.GetCells.performed += GetCells_performed;
        _playerInputActions.Map.ConfirmCell.performed += ConfirmCell_performed;
        _playerInputActions.Map.ConfirmCell.Disable(); // добавляется метод к подтверждению клетки, но потом выключается, так как при загрузке это нельзя использовать
        */
        _virtualCamera = _camera.GetComponent<CinemachineVirtualCamera>();

    }

    public void GetCells_performed()
    {
        if (!_isSelecting)
        {
            _isSelecting = true;
            GetCurrentCells();
        }
        else
        {
            // если уже производится выбор, то по этой же кнопке происходит выход с выбора клеток
            SelectingCell = null;
            OnDeselectCells?.Invoke();
            //_playerInputActions.Map.ConfirmCell.Disable();
            _isSelecting = false;
        }
    }

    private void GetCurrentCells()
    {
        _activeCells = MapLoader.Instance.ActiveCells;
        foreach (var cell in _activeCells)
        {
            cell.GetComponent<Selectable>().enabled = true;
        }
        if (_activeCells.Count > 0)
        {
            SelectingCell = _activeCells[0];
            SelectingCell.GetComponent<Selectable>().Select();
            //_playerInputActions.Map.ConfirmCell.Enable();
            OnStartingSelectCell?.Invoke();
        }
        else
        {
            Debug.LogError("There are no active cells");
        }
    }

    public void ConfirmCell_performed()
    {
        SelectCell();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractCell?.Invoke();
    }

    private void Update()
    {
        if (_isMoving)
        {
            // если игрок двигается, то он идёт к ближайшей точке пути.
            // если он до неё дошёл, то меняется точка пути (или заканчивается маршрут)
            if ((Vector2)transform.position != (Vector2)_wayPointTransform.position)
            {
                _moveDirection = Vector2.MoveTowards(transform.position, (Vector2)_wayPointTransform.position, _speed * Time.deltaTime);
                transform.position = _moveDirection;
            }
            else
            {
                ChangeWayPoint();
            }
        }
    }

    private void ChangeWayPoint()
    {
        _wayPointIndex++;
        if (_wayPointIndex == _currentPath.WayPoints.Count)
        {
            SelectingCell = _clickedCellTransform.gameObject;
            _wayPointTransform = _clickedCellTransform;
            _wayPointIndex++;
        }
        else if (_wayPointIndex > _currentPath.WayPoints.Count)
        {
            _clickedCellTransform.GetComponent<NextLevelStarter>().InCurrentCell();
            _interactingCell.CellType = CellType.Current;
            _isMoving = false;
            _currentPath = null;
            _wayPointIndex = -1;
        }
        else
        {
            _wayPointTransform = _currentPath.WayPoints[_wayPointIndex].transform;
        }

    }

    private void SelectCell()
    {
        if (!_isMoving && SelectingCell != null &&
            SelectingCell.gameObject.TryGetComponent(out _interactingCell) && 
            _interactingCell.CellType == CellType.Selecting)
        {
            OnDeselectCells?.Invoke();

            BaseCell[] cells = UnityEngine.Object.FindObjectsOfType<BaseCell>();

            BaseCell currentCell = cells.Where(c => c.CellId == MapLoader.CurrentCellId).FirstOrDefault(); // получение текущей клетки

            if (currentCell != null)
            {
                foreach (GameObject neighbor in currentCell.NeighborsCells)
                {
                    neighbor.GetComponent<BaseCell>().CellType = CellType.Inactive;
                }

                currentCell.CellType = CellType.Passed;

                // GetCurrentPath(currentCell);

                // получение текущего пути: по которому пойдёт игрок

                _currentPath = _interactingCell.Paths.Where(p => p.WayPoints[0].gameObject == currentCell.gameObject).FirstOrDefault();

                _wayPointIndex = 0;

                _wayPointTransform = _currentPath.WayPoints[_wayPointIndex].transform;

                MapLoader.CurrentCellId = _interactingCell.CellId;

                _clickedCellTransform = SelectingCell.transform;

                OnSelectingCell?.Invoke();
                    
                // _playerInputActions.Map.Interact.performed += Interact_performed;


                _interactingCell.CellType = CellType.Active;

                this.SelectingCell = null;

                _isMoving = true;
            }
            else
            {
                Debug.LogError("BaseCell Component is not attached to object");
            }
        }
        else
        {
            Debug.Log("The cell is not active");
        }

    }

    /* private void GetCurrentPath(BaseCell currentCell)
    {
        foreach (var path in _interactingCell.Paths)
        {
            if (path.WayPoints[0].gameObject == currentCell.gameObject)
            {
                Debug.Log("Transforms are equal");
                _currentPath = path;
                return;
            }
        }
    } */

    /* private BaseCell FindCurrentCell(BaseCell[] cells)
    {
        foreach (var cell in cells)
        {
            if (cell.CellId == MapLoader.CurrentCellId)
            {
                return cell;
            }
        }

        return null;
    } */
}

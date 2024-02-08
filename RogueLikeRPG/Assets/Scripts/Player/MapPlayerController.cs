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
    /* public event Action OnActiveCell;
    public event Action OnCurrentCell; // когда пользователь встал на новую клетку (может быть, поменять название) */

    public event Action OnInteractCell;

    public event Action OnDeselectCells;

    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] LayerMask _layerMask;
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
            if(value != null)
            {
                // мб сюда перенести isselecting = true??
                _virtualCamera.Follow = SelectingCell.transform;
            }
            else
            {
                _virtualCamera.Follow = _followObject.transform;
            }
        }
    }

    private List<GameObject> _activeCells = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is no more than one Player instance");
        }

        Debug.Log("SOZDALSYA", this.gameObject);

        Instance = this;

        _rb = GetComponent<Rigidbody2D>();
        _virtualCamera = _camera.GetComponent<CinemachineVirtualCamera>();
        _playerInputActions = new PlayerInputActions();
    }


    private void Start()
    {
        _playerInputActions.Map.Enable();

        // _playerInputActions.Map.Interact.performed += Interact_performed;

        _playerInputActions.Map.GetCells.performed += GetCells_performed;
        _playerInputActions.Map.ConfirmCell.performed += ConfirmCell_performed;
        _playerInputActions.Map.ConfirmCell.Disable();

    }

    private void GetCells_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!_isSelecting)
        {
            _isSelecting = true;
            // _playerInputActions.Map.Interact.performed -= Interact_performed;
            GetCurrentCells();
        }
        else
        {
            // либо ничего (выход с менюшки через другую кнопку), либо тут выход с  меню: акцент на игрока, возвращение interact и т.п.
            // _virtualCamera.Follow = _followObject;
            SelectingCell = null;
            OnDeselectCells?.Invoke();
            // _playerInputActions.Map.ConfirmCell.performed -= ConfirmCell_performed;
            _playerInputActions.Map.ConfirmCell.Disable();
            _isSelecting = false;
        }
    }

    private void GetCurrentCells()
    {
        _activeCells = MapLoader.Instance.ActiveCells; // гениальная разработка, но нужно менять! .OrderBy(c => c.transform.localPosition.x).ToList()
        foreach (var cell in _activeCells)
        {
            cell.GetComponent<Selectable>().enabled = true;
        }
        if (_activeCells.Count > 0)
        {
            SelectingCell = _activeCells[0];
            SelectingCell.GetComponent<Selectable>().Select();
            // _selectingCell.GetComponent<BaseCell>().CellType = CellType.Selecting;
            // _playerInputActions.Map.ConfirmCell.performed += ConfirmCell_performed;
            _playerInputActions.Map.ConfirmCell.Enable();
        }
        else
        {
            Debug.LogError("There are no active cells");
        }
    }

    private void ConfirmCell_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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
            if ((Vector2)transform.position != (Vector2)_clickedCellTransform.position)
            {
                _moveDirection = Vector2.MoveTowards(transform.position, (Vector2)_clickedCellTransform.position, _speed * Time.deltaTime);
                transform.position = _moveDirection;
            }
            else
            {
                _clickedCellTransform.GetComponent<StartNextLevel>().InCurrentCell();
                _interactingCell.CellType = CellType.Current;
                // _interactingCell.Interact();
                _isMoving = false;
            }
        }
    }

    private void SelectCell()
    {
        if (!_isMoving && SelectingCell != null)
        {
            Debug.Log("In Input");

            if (SelectingCell.gameObject.TryGetComponent(out _interactingCell) && _interactingCell.CellType == CellType.Selecting)
            {
                BaseCell[] cells = UnityEngine.Object.FindObjectsOfType<BaseCell>();

                BaseCell currentCell = FindCurrentCell(cells);

                if (currentCell != null)
                {
                    foreach (GameObject neighbor in currentCell.NeighborsCells)
                    {
                        neighbor.GetComponent<BaseCell>().CellType = CellType.Inactive;
                    }

                    currentCell.CellType = CellType.Inactive;

                    MapLoader.CurrentCellId = _interactingCell.CellId;

                    // ЦИКЛ НИЖЕ - ДЛЯ ТЕСТА!

                    /*foreach (GameObject neighbor in interactingCellTransform.GetComponent<NormalCell>().NeighborsCells)
                    {
                        neighbor.GetComponent<NormalCell>().IsActive = true;
                    }*/

                    _clickedCellTransform = SelectingCell.transform;

                    _playerInputActions.Map.Interact.performed += Interact_performed;

                    _playerInputActions.Map.ConfirmCell.Disable();

                    _playerInputActions.Map.GetCells.Disable();

                    _isMoving = true;

                    _interactingCell.CellType = CellType.Active;

                    this.SelectingCell = null;
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

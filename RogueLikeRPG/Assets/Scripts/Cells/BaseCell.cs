using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCell : MonoBehaviour
{
    [SerializeField] private GameObject isActiveCircle;
    public string SCENE_TO_LOAD { get; set; } // ������� ���, ����� �� ���� � ���� �������� (Scriptable Objects)

    /* public virtual void Interact()
    {
        Debug.Log("YES! VICTORY!");
    } */

    [HideInInspector]
    public string CellId; // ���������� ������������� �������

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color currentColor;
    [SerializeField] private Color selectingColor;

    public List<GameObject> NeighborsCells = new List<GameObject>(); // ������, �� ������� ����� ������� �� ���� ������

    protected Renderer _renderer;

    [SerializeField]
    private CellType _cellType = CellType.Inactive;
    public CellType CellType
    {
        get => _cellType;

        set
        {
            _cellType = value;
            ChangeCellType();
        }
    }

    protected void ChangeCellType()
    {
        switch (CellType)
        {
            case CellType.Inactive:
                _renderer.material.color = inactiveColor;
                break;
            case CellType.Active:
                _renderer.material.color = activeColor;
                break;
            case CellType.Current:
                _renderer.material.color = currentColor;
                break;
            case CellType.Selecting:
                _renderer.material.color = selectingColor;
                break;
        }
    }

    protected void ConfigureObject()
    {
        CellId = name + transform.position.ToString();
        _renderer = GetComponent<Renderer>();
        // ������� ���� ����� �������� ���������� ��� ������ �������� �����
        ChangeCellType();
        //IsCellActive();
    }
}

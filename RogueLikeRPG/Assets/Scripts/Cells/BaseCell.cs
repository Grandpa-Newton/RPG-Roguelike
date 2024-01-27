using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCell : MonoBehaviour
{
    [SerializeField] private GameObject isActiveCircle;
    [SerializeField] private bool isActive = false;
    public string SCENE_TO_LOAD { get; set; }

    public virtual void Interact()
    {
        Debug.Log("YES! VICTORY!");
    }

    [HideInInspector]
    public string CellId;

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    public List<GameObject> NeighborsCells = new(); // ������, �� ������� ����� ������� �� ���� ������

    protected Renderer _renderer;

    public bool IsActive // ������� enum: ����������, �������� � ������� (����� ��������� �� ��)
    {
        get => isActive;
        set
        {
            isActive = value;
            IsCellActive(); // ����� ��������
        }
    }

    protected void IsCellActive()
    {
        if (isActive)
        {
            _renderer.material.color = activeColor;
        }
        else
        {
            _renderer.material.color = inactiveColor;
        }
    }

    protected void ConfigureObject()
    {
        CellId = name + transform.position.ToString();
        _renderer = GetComponent<Renderer>();
        // MapPlayerController.Instance.OnActiveCell += NormalCell_OnActiveCell;
        // ������� ���� ����� �������� ���������� ��� ������ �������� �����
        IsCellActive();
    }
}

using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseCell : MonoBehaviour
{
    [SerializeField] protected GameObject isActiveCircle;

    [SerializeField] public CellSO CellData;
    // public string SCENE_TO_LOAD { get; set; } // сделать так, чтобы не было у всех объектов (Scriptable Objects)

    /* public virtual void Interact()
    {
        Debug.Log("YES! VICTORY!");
    } */

    [HideInInspector]
    public string CellId; // уникальный идентификатор объекта

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color currentColor;
    [SerializeField] private Color selectingColor;

    public List<GameObject> NeighborsCells = new List<GameObject>(); // клетки, на которые можно попасть из этой клетки

    public List<Path> Paths = new List<Path>();

    // public List<List<GameObject>> Paths = new List<List<GameObject>>();

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
                isActiveCircle.SetActive(false);
                break;
            case CellType.Active:
                _renderer.material.color = activeColor;
                isActiveCircle.SetActive(false);
                break;
            case CellType.Current:
                _renderer.material.color = currentColor;
                isActiveCircle.SetActive(false);
                break;
            case CellType.Selecting:
                _renderer.material.color = selectingColor;
                isActiveCircle.SetActive(true);
                break;
        }
    }

    protected void ConfigureObject()
    {
        CellId = name + transform.position.ToString();
        _renderer = GetComponent<Renderer>();
        isActiveCircle.SetActive(false);
        // Сделать этот метод событием вызываемом при каждой загрузке карты
        ChangeCellType();

        //IsCellActive();
    }


    [Serializable]
    public class Path
    {
        public List<GameObject> WayPoints = new List<GameObject>();
        /* protected void OnDrawGizmos()
        {
            for (int j = 0; j < WayPoints.Count; j++)
            {
                GameObject currentWayPoint = WayPoints[j];
                Gizmos.color = Color.yellow;

                if (j + 1 < WayPoints.Count)
                {
                    Gizmos.DrawLine(currentWayPoint.transform.position, WayPoints[j + 1].transform.position);
                }
            }
        } */
    }

    /* protected void OnDrawGizmos()
    {
        for (int i = 0; i < Paths.Count; i++)
        {
            for (int j = 0; j < Paths[i].WayPoints.Count; j++)
            {
                GameObject currentWayPoint = Paths[i].WayPoints[j];
                Gizmos.color = Color.yellow;

                if (j + 1 < Paths[i].WayPoints.Count)
                {
                    Gizmos.DrawLine(currentWayPoint.transform.position, Paths[i].WayPoints[j + 1].transform.position);
                }
            }
        }
    }*/

}

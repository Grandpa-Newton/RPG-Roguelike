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

    [HideInInspector]
    public string CellId; // уникальный идентификатор объекта

    [SerializeField] private GameObject levelIconGameObject;

    public List<GameObject> NeighborsCells = new List<GameObject>(); // клетки, на которые можно попасть из этой клетки

    public List<Path> Paths = new List<Path>(); // пути, по которым можно попасть к данной клетке
                                                // (первый элемент - клетка, из которой идёт путь, потом идут точки "поворота" пути)

    protected SpriteRenderer _spriteRenderer;

    protected Renderer _renderer;

    protected LevelIcon _levelIcon;

    protected bool _isPassed = false;

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
                SpriteBrightnessChanger.DecreaseSpriteBrightness(_spriteRenderer);
                _levelIcon.DecreaseSpriteBrightness();
                isActiveCircle.SetActive(false);
                break;
            case CellType.Active:
                SpriteBrightnessChanger.IncreaseSpriteBrightness(_spriteRenderer);
                _levelIcon.IncreaseSpriteBrightness();
                isActiveCircle.SetActive(false);
                break;
            case CellType.Current:
                SpriteBrightnessChanger.IncreaseSpriteBrightness(_spriteRenderer);
                _levelIcon.IncreaseSpriteBrightness();
                isActiveCircle.SetActive(false);
                break;
            case CellType.Selecting:
                SpriteBrightnessChanger.IncreaseSpriteBrightness(_spriteRenderer);
                _levelIcon.IncreaseSpriteBrightness();
                isActiveCircle.SetActive(true);
                break;
            case CellType.Passed:
                _isPassed = true;
                SpriteBrightnessChanger.DecreaseSpriteBrightness(_spriteRenderer);
                _levelIcon.DecreaseSpriteBrightness();
                _levelIcon.ChangeSprite(CellData.NextSprite);
                isActiveCircle.SetActive(false);
                break;
        }
    }

    protected void ConfigureObject()
    {
        CellId = name + transform.position.ToString();
        _renderer = GetComponent<Renderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _levelIcon = levelIconGameObject.GetComponent<LevelIcon>();
    }

    protected void ConfigureObjectStart()
    {
        _renderer.material.color = CellData.CellColor;
        ChangeCellType();
        if (!_isPassed) 
        {
            _levelIcon.ChangeSprite(CellData.OriginalSprite);
        }
    }

    protected void Awake()
    {
        ConfigureObject();
    }

    protected void Start()
    {
        ConfigureObjectStart();
    }


    [Serializable] 
    public class Path // отдельный класс для того, чтобы сделать список списков
    {
        public List<GameObject> WayPoints = new List<GameObject>();
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

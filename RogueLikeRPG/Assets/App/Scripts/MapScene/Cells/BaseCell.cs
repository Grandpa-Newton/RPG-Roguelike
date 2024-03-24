using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.MapScene.Cells
{
    public abstract class BaseCell : MonoBehaviour
    {
        [SerializeField] protected GameObject isActiveCircle;

        [SerializeField] public CellSO CellData;

        [HideInInspector]
        public string CellId; // ���������� ������������� �������

        [SerializeField] private GameObject levelIconGameObject;

        public List<GameObject> NeighborsCells = new List<GameObject>(); // ������, �� ������� ����� ������� �� ���� ������

        public List<CellSO> PossibleCellData = new List<CellSO>();


        protected SpriteRenderer _spriteRenderer;

        protected Renderer _renderer;

        protected CellIcon _cellIcon;

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
                    // SpriteBrightnessChanger.DecreaseSpriteBrightness(_spriteRenderer);
                    _cellIcon.DecreaseSpriteBrightness();
                    isActiveCircle.SetActive(false);
                    break;
                case CellType.Active:
                    //SpriteBrightnessChanger.IncreaseSpriteBrightness(_spriteRenderer);
                    _cellIcon.IncreaseSpriteBrightness();
                    isActiveCircle.SetActive(false);
                    break;
                case CellType.Current:
                    //SpriteBrightnessChanger.IncreaseSpriteBrightness(_spriteRenderer);
                    _cellIcon.IncreaseSpriteBrightness();
                    isActiveCircle.SetActive(false);
                    break;
                case CellType.Selecting:
                    //SpriteBrightnessChanger.IncreaseSpriteBrightness(_spriteRenderer);
                    _cellIcon.IncreaseSpriteBrightness();
                    isActiveCircle.SetActive(true);
                    break;
                case CellType.Passed:
                    _isPassed = true;
                    //SpriteBrightnessChanger.DecreaseSpriteBrightness(_spriteRenderer);
                    _cellIcon.DecreaseSpriteBrightness();
                    _cellIcon.ChangeSprite(CellData.NextSprite);
                    isActiveCircle.SetActive(false);
                    break;
            }
        }

        protected void ConfigureObject()
        {
            CellId = name + transform.position.ToString();
            Debug.Log(CellId);
            _renderer = GetComponent<Renderer>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _cellIcon = levelIconGameObject.GetComponent<CellIcon>();
        }

        protected void ConfigureObjectStart()
        {
            ChangeCellType();
            /*if (!_isPassed) 
            {
                _cellIcon.ChangeSprite(CellData.OriginalSprite);
            }*/
        }

        public void SetCellData(CellSO cellData)
        {
            CellData = cellData;
            _cellIcon.ChangeSprite(CellData.OriginalSprite);
            // _renderer.material.color = CellData.CellColor;

        }

        protected void Awake()
        {
            ConfigureObject();
        }

        protected void Start()
        {
            ConfigureObjectStart();
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
}

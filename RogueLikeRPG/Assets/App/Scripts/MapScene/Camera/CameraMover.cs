using App.Scripts.MixedScenes.Player.Control;
using UnityEngine;

namespace App.Scripts.MapScene.Camera
{
    public class CameraMover : MonoBehaviour
    {
        private Transform _clickedCellTransform;
        public Transform ClickedCellTransform 
        { 
            get 
            { 
                return _clickedCellTransform; 
            }
            set 
            {
                _isMoving = true;
                _clickedCellTransform = value;
            }
        }

        private void Awake()
        {
            if(Instance != null)
            {
                Debug.LogError("There is more than one Camera Mover Instance");
            }

            Instance = this;
        }

        public static CameraMover Instance;

        private bool _isMoving;

        private Vector2 _moveDirection;

        [SerializeField]
        private float _speed = 25f;
        public void MoveTo()
        {
            if (ClickedCellTransform != null)
            {
                if ((Vector2)transform.position != (Vector2)ClickedCellTransform.position)
                {
                    _moveDirection = Vector2.MoveTowards(transform.position, (Vector2)ClickedCellTransform.position, _speed * Time.deltaTime);
                    transform.position = _moveDirection;
                }
                else
                {
                    _isMoving = false;
                }
            }
        }

        public void ChangePositionToPlayer()
        {
            transform.position = MapPlayerController.Instance.transform.position;
        }

        private void Update()
        {
            if (_isMoving)
            {
                MoveTo();
            }
        }
    }
}

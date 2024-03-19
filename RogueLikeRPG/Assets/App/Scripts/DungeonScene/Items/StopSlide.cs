using UnityEngine;

namespace App.Scripts.DungeonScene.Items
{
    public class StopSlide : MonoBehaviour
    {
        private Rigidbody2D _rb2D;

        void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_rb2D.velocity.magnitude > 0)
            {
                _rb2D.velocity = new Vector2(0f, 0f);
            }
        }
    }
}
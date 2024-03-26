using UnityEngine;

namespace App.Scripts.MixedScenes.Player.Interface
{
    public interface IMove
    {
        public float moveSpeed { get; }
        public float rollSpeed { get; }
        public bool isMoving { get; }
        public bool isRolling { get; }
        
        public void Move(Vector2 moveDirection);
        public void Roll(Vector2 rollDirection, bool isRoll);
    }
}

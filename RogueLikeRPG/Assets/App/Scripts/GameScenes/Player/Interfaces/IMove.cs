namespace App.Scripts.GameScenes.Player.Interfaces
{
    public interface IMove
    {
        public float moveSpeed { get; }
        public float rollSpeed { get; }
        public bool isMoving { get; }
        public bool isRolling { get; }
        
        public void Move();
        public void MakeRoll(bool isRoll);
        
    }
}

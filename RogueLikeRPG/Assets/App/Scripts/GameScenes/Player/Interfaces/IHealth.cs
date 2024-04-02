using App.Scripts.GameScenes.Player.EditableValues;

namespace App.Scripts.GameScenes.Player.Interfaces
{
    public interface IHealth {
        public int maxHealth { get; }
        public CharacteristicValueSO playerHealth { get; }


        public void IncreaseHealth(int healthToIncrease);
        public void IncreaseMaxHealth(int maxHealthToIncrease);
        public void ReduceHealth(int healthToReduce);
        public void Die();
        public void InitializeHealth();
        
    }
}

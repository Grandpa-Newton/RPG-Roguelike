namespace App.Scripts.MixedScenes.Player.Interface
{
    public interface IHealth {
        public float maxHealth { get; }
        public FloatValueSO currentHealth { get; }


        public void IncreaseHealth(int healthToIncrease);
        public void IncreaseMaxHealth(int maxHealthToIncrease);
        public void ReduceHealth(int healthToReduce);
        public void Die();
        public void InitializeHealth();
    
    }
}

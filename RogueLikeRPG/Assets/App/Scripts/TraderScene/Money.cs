using App.Scripts.MixedScenes;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public abstract class Money
    {
        protected IntValueSO _currentMoney;
        public abstract void Initialize(IntValueSO currentMoney);
        public abstract void AddMoney(int moneyBoost);
        public abstract bool CanAffordReduceMoney(int reducingMoney);
        public abstract bool TryReduceMoney(int reducingMoney);
    }
}

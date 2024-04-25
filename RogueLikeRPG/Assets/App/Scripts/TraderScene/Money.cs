using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.MixedScenes;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public abstract class Money
    {
        public ChangeableValueSO CurrentMoney;
        public abstract void Initialize(ChangeableValueSO currentMoney);
        public abstract void AddMoney(int moneyBoost);
        public abstract bool CanAffordReduceMoney(int reducingMoney);
        public abstract bool TryReduceMoney(int reducingMoney);
    }
}

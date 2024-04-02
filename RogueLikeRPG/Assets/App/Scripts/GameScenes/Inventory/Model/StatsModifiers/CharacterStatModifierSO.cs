using UnityEngine;

namespace App.Scripts.MixedScenes.Inventory.Model.StatsModifiers
{
    public abstract class CharacterStatModifierSO : ScriptableObject
    {
        public abstract void AffectCharacter(object character, float val);
    }
}

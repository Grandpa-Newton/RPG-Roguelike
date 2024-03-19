using UnityEngine;

namespace App.Scripts.MixedScenes.Inventory.Model.StatsModifiers
{
    public abstract class CharacterStatModifierSO : ScriptableObject
    {
        public abstract void AffectCharacter(GameObject character, float val);
    }
}

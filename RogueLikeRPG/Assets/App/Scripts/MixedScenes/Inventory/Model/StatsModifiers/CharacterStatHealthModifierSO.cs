using App.Scripts.MixedScenes.Player;
using UnityEngine;

namespace App.Scripts.MixedScenes.Inventory.Model.StatsModifiers
{
    [CreateAssetMenu]
    public class CharacterStatHealthModifierSO : CharacterStatModifierSO
    {
        public override void AffectCharacter(GameObject character, float val)
        {
            Health health = character.GetComponent<Health>();
            if (health != null)
            {
                health.AddHealth((int)val);
            }
        }
        
    }
}

using App.Scripts.MixedScenes.Player;
using UnityEngine;

namespace App.Scripts.MixedScenes.Inventory.Model.StatsModifiers
{
    [CreateAssetMenu]
    public class CharacterStatHealthModifierSO : CharacterStatModifierSO
    {
        public override void AffectCharacter(object character, float val)
        { 
            PlayerHealth playerHealth = character as PlayerHealth;
            
            playerHealth?.IncreaseHealth((int)val);
        }
        
    }
}

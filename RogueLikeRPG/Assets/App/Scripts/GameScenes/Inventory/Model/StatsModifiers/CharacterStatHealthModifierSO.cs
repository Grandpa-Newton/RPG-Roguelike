using App.Scripts.GameScenes.Player.Components;
using App.Scripts.MixedScenes.Player;
using UnityEngine;

namespace App.Scripts.MixedScenes.Inventory.Model.StatsModifiers
{
    [CreateAssetMenu]
    public class CharacterStatHealthModifierSO : CharacterStatModifierSO
    {
        public override void AffectCharacter(object character, float val)
        { 
            PlayerHealth.Instance.IncreaseHealth((int) val);
            
        }
        
    }
}

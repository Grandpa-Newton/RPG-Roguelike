using UnityEngine;

namespace App.Scripts.AllScenes.Interfaces
{
    public interface IDamageable 
    {
        void TakeDamage(float amount,GameObject sender);
    }
}

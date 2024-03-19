using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.MixedScenes.Inventory.UI.ItemParameters
{
    [CreateAssetMenu]
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField] public string ParameterName { get; private set; }
        
    }
}
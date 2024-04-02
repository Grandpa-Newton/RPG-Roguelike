using App.Scripts.GameScenes.Player.EditableValues;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.GameScenes.Player.UI
{
    public class PlayerCurrencyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentCurrency;
        [SerializeField] private GameObject moneyPanel;
        [FormerlySerializedAs("_intValueSO")] [SerializeField] private ChangeableValueSO changeableValueSO; 
    
        private void Awake()
        {
            changeableValueSO.OnValueChange += UpdatePlayerCurrentCurrency;
        }

        private void Start()
        {
            UpdatePlayerCurrentCurrency(changeableValueSO.CurrentValue);
        }

        private void UpdatePlayerCurrentCurrency(int money)
        {
            currentCurrency.text = money.ToString();
        }
    }
}

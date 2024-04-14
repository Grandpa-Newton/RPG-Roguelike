using App.Scripts.GameScenes.Player.EditableValues;
using TMPro;

namespace App.Scripts.GameScenes.Player.UI
{
    public class MoneyUI
    {
        private TMP_Text _currentMoneyTextField;
        private ChangeableValueSO _playerMoneySO; 
        
        public MoneyUI(TMP_Text currentMoney, ChangeableValueSO playerMoneySO)
        {
            _currentMoneyTextField = currentMoney;
            _playerMoneySO = playerMoneySO;
            _playerMoneySO.OnValueChange += UpdatePlayerCurrentCurrency;
            UpdatePlayerCurrentCurrency(_playerMoneySO.CurrentValue);
        }

        private void UpdatePlayerCurrentCurrency(int money)
        {
            _currentMoneyTextField.text = money.ToString();
        }
    }
}
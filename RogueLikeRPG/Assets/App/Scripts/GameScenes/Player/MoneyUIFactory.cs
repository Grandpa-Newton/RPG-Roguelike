using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.GameScenes.Player.UI;
using TMPro;

public class MoneyUIFactory
{
    public static MoneyUI Create(TMP_Text currentMoney, ChangeableValueSO playerMoneySO)
    {
        return new MoneyUI(currentMoney, playerMoneySO);
    }
}


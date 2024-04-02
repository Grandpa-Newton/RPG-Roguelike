using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.MixedScenes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ChangeableValueSO playerMoneySO;
    [SerializeField] private ChangeableValueSO traderMoneySO;
    private void Awake()
    {
        PlayerMoney.Instance.Initialize(playerMoneySO);
        TraderMoney.Instance.Initialize(traderMoneySO);
        
    }
}

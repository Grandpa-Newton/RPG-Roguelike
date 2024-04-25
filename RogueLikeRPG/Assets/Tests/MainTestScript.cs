using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.EditableValues;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MainTestScript
{
    [Test]
    [TestCase(3, 6)]
    [TestCase(9, 12)]
    [TestCase(16, 17)]
    public void AddHealthTest(int expr, int expected)
    {
        CharacteristicValueSO characteristicValueSO = ScriptableObject.CreateInstance<CharacteristicValueSO>();

        characteristicValueSO.CurrentValue = 3;

        characteristicValueSO.MaxValue = 17;
        
        PlayerHealth.Instance.Initialize(characteristicValueSO);
        
        PlayerHealth.Instance.IncreaseHealth(expr);
        
        Assert.AreEqual(expected, characteristicValueSO.CurrentValue);
        
    }
    
    [Test]
    [TestCase(3, 12)]
    [TestCase(9, 6)]
    [TestCase(12, 3)]
    public void ReduceHealthTest(int expr, int expected)
    {
        CharacteristicValueSO characteristicValueSO = ScriptableObject.CreateInstance<CharacteristicValueSO>();

        characteristicValueSO.CurrentValue = 15;

        characteristicValueSO.MaxValue = 17;
        
        PlayerHealth.Instance.Initialize(characteristicValueSO);
        
        PlayerHealth.Instance.ReduceHealth(expr);
        
        Assert.AreEqual(expected, characteristicValueSO.CurrentValue);
        
    }
    
    [Test]
    [TestCase(3, 3)]
    [TestCase(12, 12)]
    public void IncreaseMaxHealthTest(int expr, int expected)
    {
        CharacteristicValueSO characteristicValueSO = ScriptableObject.CreateInstance<CharacteristicValueSO>();
        
        PlayerHealth.Instance.Initialize(characteristicValueSO);
        
        PlayerHealth.Instance.IncreaseMaxHealth(expr);
        
        Assert.AreEqual(expected, PlayerHealth.Instance.maxHealth);
        
    }
    
    [Test]
    [TestCase(15, 40)]
    [TestCase(2, 27)]
    [TestCase(16, 41)]
    public void AddMoneyTest(int expr, int expected)
    {
        ChangeableValueSO moneyValueSO = ScriptableObject.CreateInstance<ChangeableValueSO>();

        moneyValueSO.CurrentValue = 25;
        
        TraderMoney.Instance.Initialize(moneyValueSO);
        
        TraderMoney.Instance.AddMoney(expr);
        
        Assert.AreEqual(expected, TraderMoney.Instance.CurrentMoney.CurrentValue);
    }
    
    [Test]
    [TestCase(35, false)]
    [TestCase(25, true)]
    [TestCase(12, true)]
    public void CanAffordReduceMoneyTest(int expr, bool expected)
    {
        ChangeableValueSO moneyValueSO = ScriptableObject.CreateInstance<ChangeableValueSO>();

        moneyValueSO.CurrentValue = 25;
        
        TraderMoney.Instance.Initialize(moneyValueSO);
        
        var result = TraderMoney.Instance.CanAffordReduceMoney(expr);
        
        Assert.AreEqual(result, expected);
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

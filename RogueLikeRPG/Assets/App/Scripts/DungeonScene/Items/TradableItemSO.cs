using System.Collections;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using App.Scripts.TraderScene;
using Unity.VisualScripting;
using UnityEngine;
using App.Scripts.MixedScenes.Inventory.Controller;

[CreateAssetMenu(fileName = "Tradable_", menuName = "Trade")]
public class TradableItemSO : ItemSO, IDestroyableItem, IItemAction
{
    public ItemSO ItemSO;
    public string ActionName => "Trade";

    public AudioClip itemActionSound { get; }

    public int Cost;

    public bool PerformAction(GameObject character, List<ItemParameter> itemState)
    {
        Debug.Log("Cost = " + Cost);

        

        if (PlayerMoney.Instance.CanAffordReduceMoney(Cost)) // тут тоже, наверное, нужно количество
        {
            Debug.Log("Player can afford it");
            if (character.GetComponent<InventoryController>().TryAddItem(ItemSO)) // сюда нужно будет количество передавать
            {
                PlayerMoney.Instance.TryReduceMoney(Cost);
                return true;
            }
            else
            {
                Debug.Log("Player doesn't have enough space in inventory");
                return false;
            }
        }
        else
        {
            Debug.Log("Player can't afford it.");
            return false;
        }
    }
}

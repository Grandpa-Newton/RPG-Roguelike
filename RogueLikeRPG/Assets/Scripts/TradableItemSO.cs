using Inventory;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tradable_", menuName = "Trade")]
public class TradableItemSO : ItemSO, IDestroyableItem, IItemAction
{
    public string ActionName => "Trade";

    public AudioClip actionSFX { get; }

    public int Cost;

    public bool PerformAction(GameObject character, List<ItemParameter> itemState)
    {
        Debug.Log("Cost = " + Cost);

        if (character.GetComponent<Money>().TryReduceMoney(Cost))
        {
            Debug.Log("Player can afford it");
            return true;
        }
        else
        {
            Debug.Log("Player can't afford it.");
            return false;
        }

        return false;
    }
}

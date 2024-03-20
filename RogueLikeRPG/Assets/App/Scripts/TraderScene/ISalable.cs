using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISalable
{
    public int ItemBuyCost { get; set; }
    public int ItemSellCost { get; set; }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public int hPRecovery;
    
    public override void Use()
    {
        int healthRecovered = player.IncreaseHealth(hPRecovery);
        actionString = healthRecovered + " health recovered.";
        player.RemoveItem(this);
    }
}

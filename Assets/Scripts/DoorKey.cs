using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DoorKey : Item
{
    public override void Use()
    {
        player.RemoveItem(this);
        actionString = itemName + " has unlocked the door.";
    }
}

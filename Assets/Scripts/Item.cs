using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public String itemName;
    public Player player;
    public String actionString;
    public bool battleUsable;
    public float itemCost;

    public virtual void Use()
    {

    }
}

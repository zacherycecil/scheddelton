using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public String weaponName;
    public List<Attack> attackList;
    public Sprite weaponSprite;
    public float switchCost;

    public Sprite GetSprite()
    {
        return weaponSprite;
    }
}

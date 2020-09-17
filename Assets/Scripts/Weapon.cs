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
    public LevelSystem level;

    public Sprite GetSprite()
    {
        return weaponSprite;
    }

    public float GetDamageMultiplier()
    {
        return 1 + 0.1f * level.currentLevel;
    }
}

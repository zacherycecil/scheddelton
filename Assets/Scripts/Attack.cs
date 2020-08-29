using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public String attackName;
    public float staminaNeeded;
    public float baseDamage;
    public float glancingBlowDamage;
    public float accuracy;

    public bool elemental;
    public bool physical;

    System.Random rnd = new System.Random();

    public bool AccuracyCheck()
    {
        bool directHit = (float)rnd.Next(100) < accuracy;
        return directHit;
    }
}

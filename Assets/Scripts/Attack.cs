using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public String attackName;
    public float actionsRequired;
    public float baseDamage;

    public Attack(String attackName, float actionsRequired, float baseDamage)
    {
        this.attackName = attackName;
        this.actionsRequired = actionsRequired;
        this.baseDamage = baseDamage;
    }
}

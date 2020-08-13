using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public String weaponName;

    LinkedList<Attack> attackList = new LinkedList<Attack>();

    public void AddAttack(Attack atk)
    {
        attackList.add(atk);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

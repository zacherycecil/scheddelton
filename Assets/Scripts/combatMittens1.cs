using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class combatMittens1 : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindWithTag("Scheddelton").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }


}

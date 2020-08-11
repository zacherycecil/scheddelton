using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class combatMittens1 : Combat
{
    public Transform target;
    public float combatBeginRadius;
    public float chaseRadius;
    public Transform homePosition;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Scheddelton").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= combatBeginRadius)
        {
            GameObject.Find("Canvas").transform.SetActive(true);
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
            GameObject.Find("Canvas").transform.SetActive(false);
        }
    }
}

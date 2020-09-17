using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidekick : Character
{
    public String sidekickName;
    public BattleSystem battleSystem;
    public Player player;
    public LevelSystem level;
    public float switchCost;

    // MOVEMENT VARIABLES
    float x;
    float y;
    bool moving;

    Vector2 previousPosition;
    public Transform myTransform;

    // ANIMATOR
    public Animator anim;

    public virtual void MakeMove()
    {

    }

    public bool IncreaseExperience(int exp)
    {
        return level.IncreaseExperience(exp);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInitialPreviousPosition();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValues();
    }

    public void SetAnimatorValues()
    {
        moving = true;
        if (myTransform.position.x == previousPosition.x && myTransform.position.y == previousPosition.y)
            moving = false;
        // X POSITION MOVING
        if (Math.Abs(myTransform.position.x - previousPosition.x) > Math.Abs(myTransform.position.y - previousPosition.y))
        {
            y = 0;
            if (myTransform.position.x > previousPosition.x)
                x = 1;
            else if (myTransform.position.x < previousPosition.x)
                x = -1;
            else
                x = 0;
        }
        else
        {
            x = 0;
            // Y POSITION MOVING
            if (myTransform.position.y > previousPosition.y)
                y = 1;
            else if (myTransform.position.y < previousPosition.y)
                y = -1;
            else
                y = 0;
        }

        if (x != 0 || y != 0)
        {
            anim.SetFloat("x", x);
            anim.SetFloat("y", y);
        }
        anim.SetBool("moving", moving);
        previousPosition = myTransform.position;
    }

    public void SetInitialPreviousPosition()
    {
        previousPosition = transform.position;
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidekick : MonoBehaviour
{
    public String sidekickName;
    public Player player;
    public LevelSystem level;

    // MOVEMENT VARIABLES
    float x;
    float y;
    bool moving;

    Vector2 position;
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
        position = myTransform.position;
        x = (position - previousPosition).x;
        y = (position - previousPosition).y;
        if (x != 0 || y != 0)
            moving = true;
        else
            moving = false;
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);
        anim.SetBool("moving", moving);
        previousPosition = position;
    }

    public void SetInitialPreviousPosition()
    {
        previousPosition = transform.position;
    }
}

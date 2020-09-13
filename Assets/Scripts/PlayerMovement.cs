using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public BattleSystem bs;

    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public bool lockedMovement;
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInputs();
        if (bs.inBattle)
            BattleAnimation();
        else
        {
            MoveCharacter();
            MoveAnimation();
        }
    }

    void GetPlayerInputs()
    {
        change = Vector3.zero;
        if (!lockedMovement)
        {
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + change.normalized * speed * Time.fixedDeltaTime);
    }

    void MoveAnimation()
    {
        animator.SetBool("inBattle", false);

        if (IsPlayerMoving())
        {
            MoveCharacter();
            animator.SetFloat("x", change.x);
            animator.SetFloat("y", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void BattleAnimation()
    {
        lockedMovement = true;
        animator.SetBool("inBattle", true);
        enemy = bs.GetEnemy();
        animator.SetFloat("x", (enemy.gameObject.transform.position - this.gameObject.transform.position).x);
    }

    public bool IsPlayerMoving()
    {
        return change != Vector3.zero;
    }

    public void LockMovement()
    {
        lockedMovement = true;
    }

    public void UnlockMovement()
    {
        lockedMovement = false;
    }

    public void SetIdleAnimation()
    {
        animator.SetBool("moving", false);
        animator.SetBool("inBattle", false);
    }
}

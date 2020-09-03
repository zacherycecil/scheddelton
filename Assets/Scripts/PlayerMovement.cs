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
    public bool inBattle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bs.state == BattleState.NOT_IN_BATTLE)
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            //UnityEngine.Debug.Log(change);
            UpdateAnimationAndMove();
        }
        else
            animator.SetBool("inBattle", true);
    }

    void UpdateAnimationAndMove()
    {
        animator.SetBool("inBattle", false);
        if (change != Vector3.zero)
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

    public void AttackAnimation(String triggerName)
    {
        StartCoroutine(AnimationDelay(triggerName));
    }

    IEnumerator AnimationDelay(String triggerName)
    {
        animator.SetTrigger(triggerName);
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger(triggerName);
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + change.normalized * speed * Time.fixedDeltaTime);
    }
}

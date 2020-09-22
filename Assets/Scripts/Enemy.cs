using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int goldWorth;
    public int experienceWorth;
    public List<Attack> attacks;
    public Animator anim;
    System.Random rnd = new System.Random();
    public Animator animator;
    public Item droppedItem;
    public Player player;

    // MOVEMENT VARIABLES
    public float moveSpeed;
    public Transform target;
    public float combatBeginRadius;
    public float chaseRadius;
    public Transform homePosition;
    public bool inBattle;
    public bool moving;
    public float x;

    public void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= combatBeginRadius && !inBattle)
        {
            bs.SetEnemy(this);
            bs.state = BattleState.START;
            inBattle = true;
            moving = false;
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && !inBattle)
        {
            moving = true;
            if(!bs.inBattle && !player.isInMenu && !player.isInDialog)
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            if ((transform.position - target.position).x > 0)
                x = 1;
            else if ((transform.position - target.position).x < 0)
                x = -1;
        }
        else
            moving = false;
    }

    public bool HasDroppedItem()
    {
        return droppedItem != null;
    }

    public Item GetDroppedItem()
    {
        return droppedItem;
    }

    public void ReduceHealth(int damage)
    {
        currentHealth -= damage;
    }

    public void DropItem()
    {
        droppedItem.gameObject.SetActive(true);
        droppedItem.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0f, 0f, 175.6206f);
    }

    public void AttackAnimation(String triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void HurtAnimation()
    {
        anim.SetTrigger("hurt");
    }

    public Attack GetAttack()
    {
        return attacks[rnd.Next(attacks.Count)];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        anim.SetBool("moving", moving);
        anim.SetFloat("x", x);
    }
}

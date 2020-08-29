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
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            x = (transform.position - target.position).x;
        }
        else
            moving = false;
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

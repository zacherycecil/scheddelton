using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cat : Sidekick
{   
    public DialogSystem dialog;
    public MenuSystem menuSystem;
    public List<Attack> attacks;
    System.Random rnd = new System.Random();
    public UnityEvent reduceEnemyHealth;
    int savedDamage;

    public Attack GetAttack()
    {
        return attacks[rnd.Next(attacks.Count)];
    }

    public override void MakeMove()
    {
        float damage;
        Attack attack = GetAttack();
        String attackDialog = sidekickName + " helps by using " + attack.attackName + ".";
        // DIRECT HIT
        if (attack.AccuracyCheck())
        {
            attackDialog += "\n";
            damage = attack.baseDamage;
        }
        // GLANCING BLOW
        else
        {
            attackDialog += "\nGlancing blow! ";
            damage = attack.glancingBlowDamage;
        }
        Enemy enemy = battleSystem.GetEnemy();
        if (enemy.currentHealth - damage <= 0)
        {
            dialog.SpecialDialogBuffer(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + enemy.characterName + ".", reduceEnemyHealth);
            battleSystem.state = BattleState.WON;
            menuSystem.ButtonsEnabled(false);
        }
        else
        {
            dialog.SpecialDialogBuffer(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + enemy.characterName + ".", reduceEnemyHealth);
            battleSystem.state = BattleState.RECOVER_STAMINA;
        }
        savedDamage = (int)damage;
    }

    public void DoDamage()
    {
        Enemy enemy = battleSystem.GetEnemy();
        enemy.currentHealth -= (int)savedDamage;
    }
}

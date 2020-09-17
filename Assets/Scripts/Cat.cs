using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Sidekick
{   
    public DialogSystem dialog;
    public MenuSystem menuSystem;
    public List<Attack> attacks;
    System.Random rnd = new System.Random();

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
        enemy.currentHealth = enemy.currentHealth - (int)damage;
        if (enemy.currentHealth <= 0)
        {
            dialog.BattleDialogBuffer(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + enemy.characterName + ".", 2);
            battleSystem.state = BattleState.WON;
            menuSystem.ButtonsEnabled(false);
        }
        else
        {
            dialog.BattleDialogBuffer(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + enemy.characterName + ".", 2);
            battleSystem.state = BattleState.RECOVER_STAMINA;
        }
    }
}

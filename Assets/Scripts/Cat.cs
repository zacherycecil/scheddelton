using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Sidekick
{
    public BattleSystem battleSystem;
    public DialogSystem dialog;
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
        dialog.DisplayDialog(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + enemy.characterName + ".");
        enemy.currentHealth = enemy.currentHealth - (int)damage;
        dialog.ResetDialogString();
        StartCoroutine(battleSystem.BattleDelay(3));
    }
}

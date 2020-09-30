using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public enum BattleState { NOT_IN_BATTLE, START, PLAYER_TURN, ENEMY_TURN, SIDEKICK_TURN, RECOVER_STAMINA, PLAYER_LEVEL_UP, SIDEKICK_LEVEL_UP, WON, TEARDOWN, LOST, WAITING }

public class BattleSystem : MonoBehaviour
{
    // UI ELEMENT
    public MenuSystem menuSystem;

    // DIALOG BOX
    public DialogSystem dialog;

    // CHARACTERS
    public Player player;
    public Enemy enemy;

    // OTHER FIGHT ELEMENTS
    public Animator fightText;
    public BattleState state;
    public bool inBattle;

    // EVENTS
    public UnityEvent showLevelUpMenu;
    public UnityEvent enemyDie;
    public UnityEvent startBattle;
    public UnityEvent notInBattle;

    // Start is called before the first frame update
    void Start()
    {
        menuSystem.CloseMenus();
        menuSystem.LoadWeaponButtons(player.weapons);
        menuSystem.LoadAttackButtons(player.currentWeapon.attackList);
        menuSystem.LoadSidekickButtons(player.sidekicks);
        menuSystem.LoadItemButtons(player.items);
        dialog.CloseDialogBox();
        notInBattle.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        // IF MENU BUTTON IS CLICKED
        if (state == BattleState.NOT_IN_BATTLE && !player.isInDialog)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuSystem.ToggleOverworldMenu(player);
                player.isInMenu = !player.isInMenu;
            }
        }
        // BATTLE LOGIC
        if(player.isInDialog)
            menuSystem.ButtonsEnabled(false);
        else
            menuSystem.ButtonsEnabled(true);

        // STATE START
        if (state == BattleState.START)
        {
            // reset player stamina
            player.ResetStamina();

            // enemy camera set
            dialog.currentDialogTarget = enemy.gameObject;

            // start (battle music)
            startBattle.Invoke();

            // enter battle dialog
            dialog.ResetDialogString();
            dialog.BattleDialogBuffer(enemy.characterName + " wants to fight!");
            dialog.ResetDialogString();

            // set up battle HUD and menus
            menuSystem.InitializeMenu();
            menuSystem.LoadBattleHUDs(player, enemy);
            menuSystem.LoadWeaponButtons(player.weapons);
            menuSystem.LoadAttackButtons(player.GetUnlockedAttacks());

            // show FIGHT text
            fightText.SetTrigger("fight text");
            //gong.Play();

            state = BattleState.WAITING;
            inBattle = true;
        }
        // STATE ENEMY TURN
        else if (state == BattleState.ENEMY_TURN)
        {
            menuSystem.ButtonsEnabled(false);
            EnemyDoAttack(enemy.GetAttack());
            state = BattleState.SIDEKICK_TURN;
        }
        // STATE SIDEKICK TURN
        else if (state == BattleState.SIDEKICK_TURN)
        {
            player.currentSidekick.MakeMove();
        }
        // STATE STAMINA RECOVERY
        else if (state == BattleState.RECOVER_STAMINA)
        {
            player.RecoverStamina();
            dialog.BattleDialogBuffer(player.characterName + " has recovered " + player.staminaRecovery + " stamina.", 0.5f);

            state = BattleState.PLAYER_TURN;
        }
        // STATE PLAYER TURN
        else if (state == BattleState.PLAYER_TURN)
        {
            menuSystem.ButtonsEnabled(true);
            state = BattleState.WAITING;
        }
        else if (state == BattleState.WON)
        {
            // VICTORY DIALOG
            dialog.SpecialDialogBuffer(player.characterName + " has defeated " + enemy.characterName + "!\n" + player.characterName + " receives " + enemy.goldWorth + " gold coins. \n" +
                player.characterName + " and " + player.currentSidekick.sidekickName + " receive " + enemy.experienceWorth + " experience.", true, enemyDie);

            if (enemy.HasDroppedItem())
            {
                dialog.SystemDialogBuffer(enemy.characterName + " dropped a " + enemy.GetDroppedItem().itemName + ".");
            }

            if (player.IncreaseExperience(enemy.experienceWorth))
            {
                dialog.SpecialDialogBuffer("Level up! Choose a trait to increase it's level.\n", 0, showLevelUpMenu);
                state = BattleState.WAITING;
            }
            else if (player.currentSidekick.IncreaseExperience(enemy.experienceWorth))
            {
                state = BattleState.SIDEKICK_LEVEL_UP;
            }
            else
            {
                state = BattleState.NOT_IN_BATTLE;
            }
        }
        else if (state == BattleState.PLAYER_LEVEL_UP)
        {
            if (player.currentSidekick.IncreaseExperience(enemy.experienceWorth))
            {
                state = BattleState.SIDEKICK_LEVEL_UP;
            }
            else
                state = BattleState.NOT_IN_BATTLE;
        }
        else if (state == BattleState.SIDEKICK_LEVEL_UP)
        {
            state = BattleState.NOT_IN_BATTLE;
        }
        else if (state == BattleState.TEARDOWN)
        {
            state = BattleState.NOT_IN_BATTLE;
        }
    }

    public void DefeatEnemy()
    {
        menuSystem.CloseMenus();
        //enemy.gameObject.SetActive(false);
        inBattle = false;
        player.isInMenu = true;
        //dialog.CloseDialogBox();
        enemy.gameObject.SetActive(false);
        menuSystem.HideBattleHUDs();
        if (enemy.HasDroppedItem()) 
            enemy.DropItem();
    }

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public Enemy GetEnemy()
    {
        return enemy;
    }

    public void EnemyDoAttack(Attack attack)
    {
        String attackDialog = enemy.characterName + " uses " + attack.attackName + ".";
        float damage;
        // CRITICAL
        if (player.DetrimentalGambleCheck())
        {
            attackDialog += "\nFate has paid you a visit! Critical hit!\n";
            damage = attack.baseDamage * (1.5f);
        }
        // DIRECT HIT
        else if (attack.AccuracyCheck())
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
        enemy.AttackAnimation(attack.attackName.ToLower());
        player.HurtAnimation();
        dialog.BattleDialogBuffer(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + player.characterName + ".", 2);
        player.currentHealth = player.currentHealth - (int)damage;
    }

    public void PlayerDoAttack(Attack attack)
    {
        String attackDialog = player.characterName + " uses " + attack.attackName + ".";
        float damage;
        // CRITICAL
        if (player.BeneficialGambleCheck())
        {
            attackDialog += "\nFate has paid you a visit! Critical hit!\n";
            damage = attack.baseDamage * (1 + player.gambleLevel * 0.2f);
        }
        // DIRECT HIT
        else if (attack.AccuracyCheck())
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
        // ELEMENTAL / PHYSICAL
        if (attack.elemental)
            damage = damage * (1 + (player.elementalControlLevel * 0.10f));
        if (attack.physical)
            damage = damage * (1 + (player.physicalStrengthLevel * 0.10f));
        // WEAPON LEVEL MULTIPLIER
        damage = damage * player.currentWeapon.GetDamageMultiplier();
        // ANIMATION
        player.AttackAnimation(attack.attackName.ToLower());
        enemy.HurtAnimation();
        enemy.currentHealth = enemy.currentHealth - (int)damage;


        if (enemy.currentHealth <= 0)
        {
            dialog.BattleDialogBuffer(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + enemy.characterName + ".", 2);
            state = BattleState.WON;
            menuSystem.ButtonsEnabled(false);
        }
        else
            dialog.BattleDialogBuffer(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + enemy.characterName + ".");
        // WEAPON EXP
        if (player.currentWeapon.level.IncreaseExperience((int)damage))
        {
            String weaponLevelUpString = player.currentWeapon.weaponName + " is now level " + player.currentWeapon.level.currentLevel + "! ";
            weaponLevelUpString += "\n" + player.currentWeapon.attackList[player.currentWeapon.level.currentLevel].attackName + " attack unlocked!";
            dialog.BattleDialogBuffer(weaponLevelUpString, 2);
            menuSystem.LoadAttackButtons(player.GetUnlockedAttacks());
        }
    }

    public Player GetPlayer()
    {
        return player;
    }
}
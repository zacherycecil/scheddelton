using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { NOT_IN_BATTLE, START, PLAYER_TURN, ENEMY_TURN, SIDEKICK_TURN, RECOVER_STAMINA, PLAYER_LEVEL_UP, SIDEKICK_LEVEL_UP, WON1, WON2, WON3, LOST, WAITING }

public class BattleSystem : MonoBehaviour
{
    // UI ELEMENT
    public MenuSystem menuSystem;

    // DIALOG BOX
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public bool delaying;
    public DialogSystem dialog;

    // CHARACTERS
    public Player player;
    Enemy enemy;

    // OTHER FIGHT ELEMENTS
    public GameObject fightText;
    public AudioSource gong;
    public AudioSource punch;
    public float weaponSwitchCost;
    public Stamina stamina;
    public BattleState state;

    // WEAPONS / ATTACKS


    // Start is called before the first frame update
    void Start()
    {
        menuSystem.CloseMenus();
        menuSystem.LoadWeaponButtons(player.weapons);
        menuSystem.LoadAttackButtons(player.currentWeapon.attackList);
    }

    // Update is called once per frame
    void Update()
    {
        // BATTLE LOGIC
        if (!delaying)
        {
            // STATE START
            if (state == BattleState.START)
            {
                // reset player stamina
                player.ResetStamina();

                // show FIGHT text
                StartCoroutine(FightTextShow(fightText, 2.0f));
                gong.Play();

                // enter battle dialog
                dialog.ResetDialogString();
                dialog.DisplayDialog(enemy.characterName + " wants to fight!");
                dialog.ResetDialogString();

                // set up battle HUD and menus
                menuSystem.InitializeMenu();
                menuSystem.LoadBattleHUDs(player, enemy);
                menuSystem.LoadWeaponButtons(player.weapons);
                menuSystem.LoadAttackButtons(player.currentWeapon.attackList);

                state = BattleState.WAITING;
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
                state = BattleState.RECOVER_STAMINA;
            }
            // STATE STAMINA RECOVERY
            else if (state == BattleState.RECOVER_STAMINA)
            {
                player.RecoverStamina();
                // PRINT INFO
                dialog.DisplayDialog(player.characterName + " has recovered " + player.staminaRecovery + " stamina.");
                dialog.ResetDialogString();

                state = BattleState.PLAYER_TURN;
            }
            // STATE PLAYER TURN
            else if (state == BattleState.PLAYER_TURN)
            {
                menuSystem.ButtonsEnabled(true);
                state = BattleState.WAITING;
            }
            else if (state == BattleState.WON1)
            {
                menuSystem.ButtonsEnabled(false);
                enemy.gameObject.SetActive(false);
                // VICTORY DIALOG
                dialog.DisplayDialog(player.characterName + " has defeated " + enemy.characterName + "!\n" + player.characterName + " receives " + enemy.goldWorth + " gold coins.");
                dialog.DisplayDialog(player.characterName + " and " + player.currentSidekick.sidekickName + " receive " + enemy.experienceWorth + " experience.");
                menuSystem.HideBattleHUDs();
                StartCoroutine(BattleDelay(3));
                dialog.ResetDialogString();
                state = BattleState.WON2;
            }
            else if (state == BattleState.WON2)
            { 
                if (player.IncreaseExperience(enemy.experienceWorth))
                {
                    dialog.DisplayDialog("Level up! Choose a trait to increase it's level.");
                    menuSystem.GoToLevelUpMenu();
                    menuSystem.HideBattleHUDs();
                    state = BattleState.WAITING;
                }
                else if (player.currentSidekick.IncreaseExperience(enemy.experienceWorth))
                {
                    state = BattleState.SIDEKICK_LEVEL_UP;
                }
                else
                {
                    state = BattleState.WON3;
                }
            }
            else if (state == BattleState.PLAYER_LEVEL_UP)
            {
                if (player.currentSidekick.IncreaseExperience(enemy.experienceWorth))
                {
                    state = BattleState.SIDEKICK_LEVEL_UP;
                }
            }
            else if (state == BattleState.SIDEKICK_LEVEL_UP)
            {
                state = BattleState.WON3;
            }
            else if (state == BattleState.WON3)
            {
                menuSystem.CloseMenus();
                state = BattleState.NOT_IN_BATTLE;
            }
        }
    }

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void EnemyDoAttack(Attack attack)
    {
        String attackDialog = enemy.characterName + " uses " + attack.attackName + ".";
        float damage;
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
        dialog.DisplayDialog(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + player.characterName + ".");
        player.currentHealth = player.currentHealth - (int)damage;
        dialog.ResetDialogString();
        StartCoroutine(BattleDelay(3));
    }

    public void PlayerDoAttack(Attack attack)
    {
        String attackDialog = player.characterName + " uses " + attack.attackName + ".";
        float damage;
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
        // ELEMENTAL / PHYSICAL
        if (attack.elemental)
            damage = damage * (1 + (player.elementalControlLevel * 0.50f));
        if (attack.physical)
            damage = damage * (1 + (player.physicalStrengthLevel * 0.50f));

        dialog.DisplayDialog(attackDialog + attack.attackName + " deals " + (int)damage + " damage to " + player.characterName + ".");
        enemy.currentHealth = enemy.currentHealth - (int)damage;
        dialog.ResetDialogString();
        if (enemy.currentHealth <= 0)
            state = BattleState.WON1;
    }

    public bool BattleSetCurrentWeapon(Weapon weapon)
    {
        if (!player.BattleSetCurrentWeapon(weapon))
        {
            dialog.DisplayDialog("This weapon is already in use.");
            dialog.ResetDialogString();
            return false;
        }
        else
            return true;
    }

    public void SetCurrentWeapon(Weapon weapon)
    {
        player.SetWeapon(weapon);
    }

    public Player GetPlayer()
    {
        return player;
    }

    IEnumerator BattleDelay(float time)
    {
        delaying = true;
        yield return new WaitForSeconds(time);
        delaying = false;
    }

    IEnumerator FightTextShow(GameObject go, float delay)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
}
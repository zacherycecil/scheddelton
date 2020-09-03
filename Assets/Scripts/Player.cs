using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // WEAPON
    public List<Weapon> weapons;
    public Weapon currentWeapon;

    public Stamina stamina;
    public Animator anim;

    public float staminaRecovery;

    public LevelSystem level;
    public Sidekick currentSidekick;
    public int gold = 0;

    // TRAITS
    public int physicalStrengthLevel;
    public int cunningLevel;
    public int elementalControlLevel;
    public int gambleLevel;

    public void AttackAnimation(String triggerName)
    {
        this.gameObject.GetComponent<PlayerMovement>().AttackAnimation(triggerName);
    }

    public void ResetStamina()
    {
        stamina.ResetStamina();
    }

    public void RecoverStamina()
    {
        stamina.RecoverStamina(staminaRecovery);
    }

    public void IncreaseStamina(float increase)
    {
        stamina.IncreaseStamina(increase);
    }

    public bool UseStamina(float staminaUsed)
    {
        return stamina.UseStamina(staminaUsed);
    }

    public bool IncreaseExperience(int exp)
    {
        return level.IncreaseExperience(exp);
    }

    public bool BattleSetCurrentWeapon(Weapon weapon)
    {
        if (weapon != currentWeapon)
        {
            if (UseStamina(weapon.switchCost))
            {
                SetWeapon(weapon);
            }
            return true;
        }
        else
            return false;           
    }

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}

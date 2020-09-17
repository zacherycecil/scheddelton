using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // WEAPON
    public List<Weapon> weapons;
    public List<Item> items;
    public Weapon currentWeapon;

    public Stamina stamina;
    public Animator anim;

    public float staminaRecovery;
    System.Random rnd = new System.Random();

    public LevelSystem level;
    public List<Sidekick> sidekicks;
    public Sidekick currentSidekick;
    public int gold = 0;

    // TRAITS
    public int physicalStrengthLevel;
    public int cunningLevel;
    public int elementalControlLevel;
    public int gambleLevel; 

    private PlayerMovement playerMovement;

    public bool isInDialog;
    public bool isInMenu;

    void Update()
    {
        if(isInDialog || isInMenu)
            SetMovementLocked(true);
        else
            SetMovementLocked(false);
    }

    public void IsInDialog(bool isInDialog)
    {
        this.isInDialog = isInDialog;
    }

    // ATTACK
    public void AttackAnimation(String triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public bool BeneficialGambleCheck()
    {
        return rnd.Next(100) <= 2 * gambleLevel;
    }

    public bool DetrimentalGambleCheck()
    {
        return rnd.Next(100) <= gambleLevel;
    }

    // ITEM
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    // STAMINA
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

    // EXP
    public bool IncreaseExperience(int exp)
    {
        bool leveledUp = level.IncreaseExperience(exp);
        if(leveledUp)
        {
            currentHealth += level.currentLevel * 3;
            maxHealth += level.currentLevel * 3;
        }
        return leveledUp;
    }

    public int GetLevel()
    {
        return level.currentLevel;
    }

    public int PercentToNextLevel()
    {
        return level.PercentToNextLevel();
    }

    // HEALTH
    public int IncreaseHealth(int hPIncrease)
    {
        int healthRecovered;
        if (currentHealth + hPIncrease > maxHealth)
        {
            healthRecovered = maxHealth - currentHealth;
            currentHealth = maxHealth;
        }
        else
        {
            healthRecovered = hPIncrease;
            currentHealth += hPIncrease;
        }
        return healthRecovered;
    }

    //WEAPON

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    public List<Attack> GetUnlockedAttacks()
    {
        return currentWeapon.attackList.GetRange(0, currentWeapon.level.currentLevel + 1);
    }

    //SIDEKICK

    public void SetCurrentSidekick(Sidekick sidekick)
    {
        sidekick.gameObject.transform.position = currentSidekick.gameObject.transform.position;
        currentSidekick = sidekick;
        foreach(Sidekick sk in sidekicks)
        {
            if (sk == currentSidekick)
                sk.gameObject.SetActive(true);
            else
                sk.gameObject.SetActive(false);
        }
    }

    // MOVEMENT
    public bool GetMovementLocked()
    {
        return playerMovement.lockedMovement;
    }

    public void SetMovementLocked(bool locked)
    {
        playerMovement.lockedMovement = locked;
    }

    public bool GetMoving()
    {
        return playerMovement.IsPlayerMoving();
    }

    void Start()
    {
        SetCurrentSidekick(currentSidekick);
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
    }

    public void HurtAnimation()
    {
        anim.SetTrigger("hurt");
    }

    // KEY/DOOR
    public bool HasKey(DoorKey key)
    {
        bool found = false;
        foreach (Item item in items)
        {
            if(item == key)
            {
                found = true;
                break;
            }
        }
        return found;
    }
}

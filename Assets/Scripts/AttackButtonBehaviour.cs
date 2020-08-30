﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButtonBehaviour : ButtonBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Attack attack;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        stamina.HoverAction(attack.staminaNeeded);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stamina.EndHoverAction();
    }

    public void DoAttack()
    {
        if (stamina.UseStamina(attack.staminaNeeded))
        {
            battleSystem.PlayerDoAttack(attack);
        }
        menuSystem.GoToMainBattleMenu();
    }
}

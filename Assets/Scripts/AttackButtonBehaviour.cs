using System;
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
            menuSystem.ButtonsEnabled(false);
            battleSystem.PlayerDoAttack(attack);
            menuSystem.GoToMainBattleMenu();
        }
        else
        {
            dialog.DisplaySystemDialog("Not enough stamina for this action!");
            dialog.ResetDialogString();
        }
    }
}

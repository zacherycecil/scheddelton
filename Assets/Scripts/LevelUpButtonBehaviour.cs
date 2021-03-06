using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpButtonBehaviour : ButtonBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public String traitDescription;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        dialog.BattleDialogBuffer("Level up! Choose a trait to increase it's level.\n" + traitDescription, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dialog.BattleDialogBuffer("Level up! Choose a trait to increase it's level.\n", 0);
    }

    public void IncreasePhysicalStrength()
    {
        player.physicalStrengthLevel++;
        battleSystem.state = BattleState.PLAYER_LEVEL_UP;
        LevelUpTearDown();
    }

    public void IncreaseCunning()
    {
        player.cunningLevel++;
        battleSystem.state = BattleState.PLAYER_LEVEL_UP;
        LevelUpTearDown();
    }

    public void IncreaseElementalControl()
    {
        player.elementalControlLevel++;
        battleSystem.state = BattleState.PLAYER_LEVEL_UP;
        LevelUpTearDown();
    }

    public void IncreaseGamble()
    {
        player.gambleLevel++;
        battleSystem.state = BattleState.PLAYER_LEVEL_UP;
        LevelUpTearDown();
    }

    void LevelUpTearDown()
    {
        menuSystem.CloseMenus();
        dialog.CloseDialogBox();
    }
}

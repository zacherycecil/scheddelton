﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpButtonBehaviour : ButtonBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Player player;
    public String traitDescription;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        dialog.ResetDialogString();
        dialog.DisplayDialog("Level up! Choose a trait to increase it's level.\n" + traitDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dialog.ResetDialogString();
        dialog.DisplayDialog("Level up! Choose a trait to increase it's level.\n");
    }

    public void IncreasePhysicalStrength()
    {
        player.physicalStrengthLevel++;
        battleSystem.state = BattleState.PLAYER_LEVEL_UP;
    }

    public void IncreaseCunning()
    {
        player.cunningLevel++;
        battleSystem.state = BattleState.PLAYER_LEVEL_UP;
    }

    public void IncreaseElementalControl()
    {
        player.elementalControlLevel++;
        battleSystem.state = BattleState.PLAYER_LEVEL_UP;
    }

    public void IncreaseGamble()
    {
        player.gambleLevel++;
        battleSystem.state = BattleState.PLAYER_LEVEL_UP;
    }
}

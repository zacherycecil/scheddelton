using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Sidekick
{
    public float staminaRecovery;
    public DialogSystem dialog;

    public override void MakeMove()
    {
        player.IncreaseStamina(staminaRecovery + player.staminaRecovery);
        dialog.BattleDialogBuffer(sidekickName + " has helped " + player.characterName + " recover stamina.\n" 
            + (staminaRecovery + player.staminaRecovery) + " stamina recovered.", 2);
        bs.state = BattleState.PLAYER_TURN;
    }
}

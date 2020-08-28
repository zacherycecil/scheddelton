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
        player.IncreaseStamina(staminaRecovery);
        dialog.DisplayDialog(sidekickName + " has helped " + player.characterName + " recover stamina.\n" 
            + staminaRecovery + " stamina recovered.");
    }
}

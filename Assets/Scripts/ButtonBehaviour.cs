using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour
{
    public BattleSystem battleSystem;
    public MenuSystem menuSystem;
    public Stamina stamina;
    public DialogSystem dialog;
    public Player player;

    public void GoToWeaponsMenu()
    {
         menuSystem.GoToWeaponsMenu();
    }

    public void GoToAttackMenu()
    {
        menuSystem.GoToAttackMenu();
    }

    public void GoToSidekickMenu()
    {
        menuSystem.GoToSidekickMenu();
    }

    public void GoToPocketsMenu()
    {
        menuSystem.GoToPocketsMenu();
    }

    public void GetPlayerStats()
    {
        dialog.DisplaySystemDialog(player.characterName + "\nLevel: " + player.GetLevel() + ", " + player.PercentToNextLevel() + "% to next level.");
        dialog.DisplaySystemDialog("Physical Strength: " + player.physicalStrengthLevel);
        dialog.DisplaySystemDialog("Cunning: " + player.cunningLevel);
        dialog.DisplaySystemDialog("Elemental Control: " + player.elementalControlLevel);
        dialog.DisplaySystemDialog("Gamble Level: " + player.gambleLevel);
        dialog.ResetDialogString(); 
    }

    public void GoBack()
    {
        if(battleSystem.state == BattleState.NOT_IN_BATTLE)
            menuSystem.GoToOverworldMenu();
        else
            menuSystem.GoToMainBattleMenu();
    }

    public void FinishTurn()
    {
        battleSystem.state = BattleState.ENEMY_TURN;
    }
}

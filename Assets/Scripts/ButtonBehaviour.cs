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

    public void GoToWeaponsMenu()
    {
         menuSystem.GoToWeaponsMenu();
    }

    public void GoToAttackMenu()
    {
        menuSystem.GoToAttackMenu();
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

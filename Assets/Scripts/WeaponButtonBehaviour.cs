using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponButtonBehaviour : ButtonBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Weapon weapon;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        stamina.HoverAction(0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stamina.EndHoverAction();
    }

    public void SetCurrentWeapon()
    {
        if(battleSystem.state == BattleState.NOT_IN_BATTLE)
        {
            battleSystem.SetCurrentWeapon(weapon);
            menuSystem.LoadAttackButtons(weapon.attackList);
            menuSystem.SetWeaponIcon(weapon);
        }
        else
            if (battleSystem.BattleSetCurrentWeapon(weapon))
            {
                menuSystem.LoadAttackButtons(weapon.attackList);
                menuSystem.GoToMainBattleMenu();
                menuSystem.SetWeaponIcon(weapon);
            }
    }
}

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
        if (battleSystem.state == BattleState.NOT_IN_BATTLE && player.currentWeapon != weapon)
        {
            player.SetWeapon(weapon);
            menuSystem.LoadAttackButtons(player.GetUnlockedAttacks());
            menuSystem.SetWeaponIcon(weapon);
            menuSystem.ReturnToMain();
        }
        else
        {
            if (player.currentWeapon == weapon)
            {
                dialog.DisplaySystemDialog("This weapon is already being used.");
            }
            else if (stamina.UseStamina(weapon.switchCost))
            {
                dialog.BattleDialogBuffer(player.characterName + " switched to " + weapon.weaponName + ".", 0);
                player.SetWeapon(weapon);
                menuSystem.LoadAttackButtons(player.GetUnlockedAttacks());
                menuSystem.SetWeaponIcon(weapon);
                menuSystem.ReturnToMain();
                menuSystem.LoadWeaponButtons(player.weapons);
            }
            else
            {
                dialog.BattleDialogBuffer("Not enough stamina for this action!", 0);
            }
        }
    }
}

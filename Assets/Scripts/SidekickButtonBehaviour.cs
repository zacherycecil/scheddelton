using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SidekickButtonBehaviour : ButtonBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sidekick sidekick;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        stamina.HoverAction(0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stamina.EndHoverAction();
    }

    public void SetCurrentSidekick()
    {
        if (battleSystem.state == BattleState.NOT_IN_BATTLE && player.currentSidekick != sidekick)
        {
            player.SetCurrentSidekick(sidekick);
            menuSystem.ReturnToMain();
        }
        else
        {
            if (player.currentSidekick == sidekick)
            {
                dialog.DisplayDialog("This sidekick is already by your side.");
                dialog.ResetDialogString();
            }
            else if(stamina.UseStamina(sidekick.switchCost))
            {
                player.SetCurrentSidekick(sidekick);
                menuSystem.ReturnToMain();
            }
            else
            {
                dialog.DisplayDialog("Not enough stamina for this action!");
                dialog.ResetDialogString();
            }
        }
    }

    public void SetSidekick(Sidekick sidekick)
    {
        this.sidekick = sidekick;
    }

}

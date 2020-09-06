using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButtonBehaviour : ButtonBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        stamina.HoverAction(item.itemCost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stamina.EndHoverAction();
    }

    public void UseItem()
    {
        if (item.battleUsable)
        {
            menuSystem.ItemConfirmationActive(true);
            menuSystem.ItemButtonsEnabled(false);
            menuSystem.SetConfirmationItem(item);
        }
        else
        {
            dialog.DisplaySystemDialog("This item is not usable in battle.");
            dialog.ResetDialogString();
        }
    }

    public void ConfirmUseItem()
    {
        if (stamina.UseStamina(item.itemCost))
        {
            item.Use();
            menuSystem.ItemButtonsEnabled(true);
            menuSystem.ItemConfirmationActive(false);
            menuSystem.LoadItemButtons(player.items);
            dialog.DisplaySystemDialog(item.itemName + " has been used.");
            dialog.DisplaySystemDialog(item.actionString);
            dialog.ResetDialogString();
            menuSystem.GoToMainBattleMenu();
        }
        else
        {
            dialog.DisplaySystemDialog("Not enough stamina for this action!");
            dialog.ResetDialogString();
        }
    }

    public void DeclineUseItem()
    {
        menuSystem.ItemButtonsEnabled(true);
        menuSystem.ItemConfirmationActive(false);
    }
}

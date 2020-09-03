using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButtonBehaviour : ButtonBehaviour
{
    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void UseItem()
    {
        menuSystem.ItemConfirmationActive(true);
        menuSystem.ItemButtonsEnabled(false);
        menuSystem.SetConfirmationItem(item);
    }

    public void ConfirmUseItem()
    {
        item.Use();
        menuSystem.ItemButtonsEnabled(true);
        menuSystem.ItemConfirmationActive(false);
        menuSystem.LoadItemButtons(player.items);
        dialog.DisplayDialog(item.itemName + " has been used.");
        dialog.DisplayDialog(item.actionString);
        dialog.ResetDialogString();
    }

    public void DeclineUseItem()
    {
        menuSystem.ItemButtonsEnabled(true);
        menuSystem.ItemConfirmationActive(false);
    }
}

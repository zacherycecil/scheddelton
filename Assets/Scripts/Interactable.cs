using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : Character
{
    [System.Serializable]
    public struct DialogObject
    {
        public Character character;
        public String str;
        public Item item;
        public bool oneTimeDialog;
    };

    public DialogSystem dialog;
    public Player player;
    public MenuSystem menuSystem;
    public List<DialogObject> dialogList;

    public void AddDialogToBuffer()
    {
        foreach (DialogObject dialogObject in dialogList)
        {
            dialog.CharacterDialogBuffer(dialogObject.str, dialogObject.character, dialogObject.item);
        }

        // DELETE ONE-TIME DIALOG
        List<DialogObject> toDelete = new List<DialogObject>();
        foreach (DialogObject dialogObject in dialogList)
        {
            if (dialogObject.oneTimeDialog)
                toDelete.Add(dialogObject);
        }
        dialogList.RemoveAll(item => toDelete.Contains(item));
    }

    public void PickupItem(Item item)
    {
        dialog.SystemDialogBuffer(player.characterName + " has picked up the " + item.itemName + ".");
        player.AddItem(item);
        menuSystem.LoadItemButtons(player.items);
        item.gameObject.SetActive(false);
    }
}

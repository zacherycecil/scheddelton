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
    };

    public DialogSystem dialog;
    public Player player;
    public MenuSystem menuSystem;
    public List<DialogObject> dialogList;
    public Image portraitObject;
    int counter;

    void Start()
    {
        counter = 0;
    }

    public void NextDialog()
    {
        if(counter == dialogList.Count)
        {
            counter = 0;
            dialog.CloseDialogBox();
            player.SetMovementLocked(false);
            player.IsInDialog(false);
        }
        else
        {
            dialog.ResetDialogString();
            dialog.DisplayDialog(dialogList[counter].str);
            portraitObject.sprite = dialogList[counter].character.characterPortrait;
            counter++;
        }
    }

    public void PickupItem(Item item)
    {
        dialog.DisplaySystemDialog(player.characterName + " has picked up the " + item.itemName + ".");
        player.AddItem(item);
        menuSystem.LoadItemButtons(player.items);
        item.gameObject.SetActive(false);
    }
}

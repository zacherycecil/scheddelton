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
        public bool itemReceived;
    };

    public DialogSystem dialog;
    public Player player;
    public MenuSystem menuSystem;
    public List<DialogObject> dialogList;
    public Image portraitObject;
    int counter;
    public bool itemReceived;   

    void Start()
    {
        counter = 0;
    }

    public void NextDialog()
    {
        if (counter == dialogList.Count)
        {
            ResetDialog();
        }
        else
        {
            if (dialogList[counter].item == null)
            {
                dialog.ResetDialogString();
                dialog.DisplayDialog(dialogList[counter].str);
                portraitObject.sprite = dialogList[counter].character.characterPortrait;
                counter++;

            }
            else
            {
                dialog.ResetDialogString();
                if (!itemReceived)
                {
                    ReceiveItem(dialogList[counter].item);
                    itemReceived = true;
                    dialogList.RemoveAt(counter);
                }
                else
                    counter++;
            }
        }
    }

    public void ResetDialog()
    {
        counter = 0;
        dialog.CloseDialogBox();
        player.SetMovementLocked(false);
        player.IsInDialog(false);
    }

    public void PickupItem(Item item)
    {
        dialog.DisplaySystemDialog(player.characterName + " has picked up the " + item.itemName + ".");
        dialog.ResetDialogString();
        player.AddItem(item);
        menuSystem.LoadItemButtons(player.items);
        item.gameObject.SetActive(false);
    }

    public void ReceiveItem(Item item)
    {
        dialog.DisplaySystemDialog(player.characterName + " has received a " + item.itemName + ".");
        dialog.ResetDialogString();
        player.AddItem(item);
        menuSystem.LoadItemButtons(player.items);
    }
}

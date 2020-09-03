using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : Character
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
    public Image portrait;
    int counter = 0;

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
            portrait.sprite = dialogList[counter].character.characterPortrait;
            counter++;
            if (counter % 3 == 0)
            {
                dialog.ResetDialogString();
            }
        }
    }
}

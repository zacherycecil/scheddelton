using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [System.Serializable]
    public struct DialogObject
    {
        public bool system;
        public Character character;
        public String str;
        public Item item;
        public float delay;
        public bool needsCleared;
        public UnityEvent specialEvent;
    };

    public BattleSystem battleSystem;
    String dialogString = "";
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;
    public GameObject characterPortrait;
    public Player player;
    public MenuSystem menuSystem;
    public bool readyForNext;
    public KeyCode interactKey;
    public List<DialogObject> dialogStringBuffer;
    public GameObject readyIndicator;
    public bool needsCleared;
    public bool dialogBoxOpen;
    public GameObject currentDialogTarget;

    // UTILITY
    public void ResetDialogString()
    {
        dialogString = "";
    }

    public void DisplayDialog(String str)
    {
        characterPortrait.SetActive(true);
        dialogBox.SetActive(true);
        dialogString +=  str + "\n";
        dialogText.text = dialogString;
    }

    public void DisplaySystemDialog(String str)
    {
        characterPortrait.SetActive(false);
        dialogBox.SetActive(true);
        dialogString += str + "\n";
        dialogText.text = dialogString;
    }

    public void CloseDialogBox()
    {
        dialogBox.SetActive(false);
        dialogBoxOpen = false;
    }

    public bool IsBufferEmpty()
    {
        return dialogStringBuffer.Count == 0;
    }

    // DIALOG BUFFER OBJECT CREATION
    public void CreateDialogObject(bool system, String str, Character character, Item item, float delay, bool needsCleared, UnityEvent specialEvent)
    {
        DialogObject dialogObject;
        dialogObject.system = system;
        dialogObject.str = str;
        dialogObject.character = character;
        dialogObject.item = item;
        dialogObject.delay = delay;
        dialogObject.needsCleared = needsCleared;
        dialogObject.specialEvent = specialEvent;
        dialogStringBuffer.Add(dialogObject);
    }

    public void BattleDialogBuffer(String str)
    {
        CreateDialogObject(true, str, null, null, 1, false, null);
    }

    public void BattleDialogBuffer(String str, float delay)
    {
        CreateDialogObject(true, str, null, null, delay, false, null);
    }

    public void CharacterDialogBuffer(String str, Character character, Item item)
    {
        CreateDialogObject(false, str, character, item, 1, true, null);
    }

    public void SystemDialogBuffer(String str)
    {
        CreateDialogObject(true, str, null, null, 1, true, null);
    }

    public void SpecialDialogBuffer(String str, UnityEvent specialEvent)
    {
        CreateDialogObject(true, str, null, null, 2, false, specialEvent);
    }

    public void SpecialDialogBuffer(String str, int delay, UnityEvent specialEvent)
    {
        CreateDialogObject(true, str, null, null, delay, false, specialEvent);
    }

    public void SpecialDialogBuffer(String str, bool needsCleared, UnityEvent specialEvent)
    {
        CreateDialogObject(true, str, null, null, 2, needsCleared, specialEvent);
    }

    // UPDATE
    void Update()
    {
        if (dialogBoxOpen)
            player.SetMovementLocked(true);
        else
            player.SetMovementLocked(false);

        // HAPPENS ONCE IF BUFFER NOT EMPTY
        if (dialogStringBuffer.Count != 0 && !player.isInDialog && !player.isInMenu)
        {
            dialogBoxOpen = true;
            player.isInDialog = true;
            player.isInMenu = true;
            readyForNext = true;
            DisplayNextInBuffer(dialogStringBuffer[0]);
            needsCleared = false;
        }

        // IF IN DIALOG
        if (player.isInDialog || player.isInMenu)
        {
            if (readyForNext)
            {
                readyIndicator.SetActive(true);
                // READY FOR NEXT
                if (Input.GetKeyDown(interactKey) || !needsCleared)
                {
                    if (dialogStringBuffer.Count == 0)
                    {
                        player.isInDialog = false;
                        player.isInMenu = false;
                        if (needsCleared)
                            CloseDialogBox();
                    }
                    else
                    {
                        readyForNext = false;
                        readyIndicator.SetActive(false);
                        DialogObject currentDialog = dialogStringBuffer[0];
                        dialogStringBuffer.RemoveAt(0);
                        StartCoroutine(TextDelay(currentDialog.delay));
                        DisplayNextInBuffer(currentDialog);
                    }
                }
            }
            else // NOT READY FOR NEXT
                readyIndicator.SetActive(false);
        }

        if(!player.isInDialog && !player.isInMenu && !battleSystem.inBattle)
            currentDialogTarget = null;
    }

    // HANDLES THE BUFFER OBJECT
    void DisplayNextInBuffer(DialogObject currentDialog)
    {
        // NEEDS CLEARED WITH E
        needsCleared = currentDialog.needsCleared;
        // IF EVENT
        if (currentDialog.specialEvent != null)
            currentDialog.specialEvent.Invoke();
        // SYSTEM/CHARACTER TEXT
        if (currentDialog.item != null)
        {
            // RECEIVE ITEM
            DisplaySystemDialog(player.characterName + " has received a " + currentDialog.item.itemName + ".");
            ResetDialogString();
            player.AddItem(currentDialog.item);
            menuSystem.LoadItemButtons(player.items);
        }
        else if (currentDialog.system)
        {
            DisplaySystemDialog(currentDialog.str);
            ResetDialogString();
        }
        else
        {
            characterPortrait.GetComponent<Image>().sprite = currentDialog.character.characterPortrait;
            DisplayDialog(currentDialog.str);
            ResetDialogString();
        }
    }

    // DELAY UNTIL READY
    public IEnumerator TextDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        readyForNext = true;
    }
}

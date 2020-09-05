using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    String dialogString = "";
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;
    public GameObject characterPortrait;

    public void ResetDialogString()
    {
        dialogString = "";
    }

    // ! DIALOG
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
        ResetDialogString();
    }
}

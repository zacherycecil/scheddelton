using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    String dialogString = "";
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;

    public void ResetDialogString()
    {
        dialogString = "";
    }

    // ! DIALOG
    public void DisplayDialog(String str)
    {
        dialogBox.SetActive(true);
        dialogString +=  str + "\n";
        dialogText.text = dialogString;
    }
}

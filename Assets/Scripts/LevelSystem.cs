using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    // EXP AND LEVEL
    public int currentExperience;
    public int currentLevel;
    public List<int> levelUpExperience;

    // LEVEL MENU
    public GameObject menu;
    public List<Button> levelUpMenuButtons;

    public DialogSystem dialog;

    public bool IncreaseExperience(int exp)
    {
        currentExperience += exp;
        if (currentExperience >= levelUpExperience[currentLevel])
        {
            // LEVEL UP
            currentLevel++;
            return true;
        }
        else
            return false;
    }

    public void PlayerStatIncrease()
    {
        dialog.ResetDialogString();
        dialog.DisplayDialog("Choose one of your traits to level up.");
    }
}

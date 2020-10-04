using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BattleSystem battleSystem;
    public MenuSystem menuSystem;
    public Stamina stamina;
    public DialogSystem dialog;
    public Player player;

    // sprites for finish
    public Sprite greenButton;
    public Sprite greenButtonPressed;
    public Sprite blueButton;
    public Sprite blueButtonPressed;

    public void GoToWeaponsMenu()
    {
         menuSystem.GoToWeaponsMenu();
    }

    public void GoToAttackMenu()
    {
        menuSystem.GoToAttackMenu();
    }

    public void GoToSidekickMenu()
    {
        menuSystem.GoToSidekickMenu();
    }

    public void GoToPocketsMenu()
    {
        menuSystem.GoToPocketsMenu();
    }

    public void GetPlayerStats()
    {
        dialog.DisplaySystemDialog(player.characterName + "\nLevel: " + player.GetLevel() + ", " + player.PercentToNextLevel() + "% to next level.");
        dialog.DisplaySystemDialog("Physical Strength: " + player.physicalStrengthLevel);
        dialog.DisplaySystemDialog("Cunning: " + player.cunningLevel);
        dialog.DisplaySystemDialog("Elemental Control: " + player.elementalControlLevel);
        dialog.DisplaySystemDialog("Gamble Level: " + player.gambleLevel);
        dialog.ResetDialogString(); 
    }

    public void GoBack()
    {
        if(battleSystem.state == BattleState.NOT_IN_BATTLE)
            menuSystem.GoToOverworldMenu();
        else
            menuSystem.GoToMainBattleMenu();
    }

    public void FinishTurn()
    {
        ButtonColorBlue();
        battleSystem.state = BattleState.ENEMY_TURN;
    }

    public void TextToMatchPressedButton(bool buttonPressed)
    {
        if (buttonPressed || !this.GetComponentInChildren<Button>().interactable)
        {
            transform.GetChild(0).GetComponent<RectTransform>().offsetMin
                = new Vector2(0, -7);
            transform.GetChild(0).GetComponent<RectTransform>().offsetMax
                = new Vector2(0, 0);
        }
        else
        {
            transform.GetChild(0).GetComponent<RectTransform>().offsetMin
                = new Vector2(0, 0);
            transform.GetChild(0).GetComponent<RectTransform>().offsetMax
                = new Vector2(0, 7);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TextToMatchPressedButton(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        TextToMatchPressedButton(false);
    }

    public void ButtonColorGreen()
    {
        UnityEngine.Debug.Log("shit");
        // set unpressed sprite
        this.GetComponentInChildren<Image>().sprite = greenButton;

        // set pressed sprite
        SpriteState state = new SpriteState();
        state = this.GetComponentInChildren<Button>().spriteState;
        state.pressedSprite = greenButtonPressed;
        this.GetComponentInChildren<Button>().spriteState = state;

        // set text color to green
        transform.GetChild(0).GetComponent<Text>().color = new Color32(155, 199, 113, 255);
    }
    public void ButtonColorBlue()
    {
        // set unpressed sprite
        this.GetComponentInChildren<Image>().sprite = blueButton;

        // set pressed sprite
        SpriteState state = new SpriteState();
        state = this.GetComponentInChildren<Button>().spriteState;
        state.pressedSprite = blueButtonPressed;
        this.GetComponentInChildren<Button>().spriteState = state;

        // set text color to blue
        transform.GetChild(0).GetComponent<Text>().color = new Color32(135, 217, 230, 255);
    }
}

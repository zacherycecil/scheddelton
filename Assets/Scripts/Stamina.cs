using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public bool blinking;
    public float currentStamina;
    public float totalStamina;
    public float staminaAfterMove;

    public List<Animator> anim;
    public Button finishButton;
    public BattleSystem battleSystem;

    public void HoverAction(float staminaNeeded)
    {
        if (battleSystem.state != BattleState.NOT_IN_BATTLE)
        {
            staminaAfterMove = currentStamina - staminaNeeded;
            blinking = true;
        }
    }

    public void EndHoverAction()
    {
        blinking = false;
    }

    public bool UseStamina(float staminaUsed)
    {
        staminaAfterMove = currentStamina - staminaUsed;
        blinking = false;
        bool enoughStamina;
        if (staminaAfterMove >= 0)
        {
            currentStamina = currentStamina - staminaUsed;
            enoughStamina = true;
        }
        else
        {
            enoughStamina = false;
        }
        if(currentStamina == 0)
        {
            StartCoroutine(BlinkFinishButton());
        }
        return enoughStamina;
    }

    public void ResetStamina()
    {
        currentStamina = totalStamina;
    }

    public void RecoverStamina(float staminaIncrease)
    {
        currentStamina = currentStamina + staminaIncrease;
        if (currentStamina > totalStamina)
            currentStamina = totalStamina;
    }

    public void IncreaseStamina(float stamUp)
    {
        currentStamina = currentStamina + stamUp;
        if (currentStamina > totalStamina)
            currentStamina = totalStamina;
    }

    IEnumerator BlinkFinishButton()
    {
        ColorBlock colorBlock = finishButton.colors;
        colorBlock.normalColor = new Color(1, 1, 1, 1);
        finishButton.colors = colorBlock;
        yield return new WaitForSeconds(0.5f);
        colorBlock.normalColor = new Color(0.6117647f, 0.6509804f, 0.7098039f, 1);
        finishButton.colors = colorBlock;
        yield return new WaitForSeconds(0.5f);
        colorBlock.normalColor = new Color(1, 1, 1, 1);
        finishButton.colors = colorBlock;
        yield return new WaitForSeconds(0.5f);
        colorBlock.normalColor = new Color(0.6117647f, 0.6509804f, 0.7098039f, 1);
        finishButton.colors = colorBlock;
    }

    void Update()
    {
        anim[0].SetBool("blinking", blinking);
        anim[0].SetFloat("staminaBefore", currentStamina);
        anim[0].SetFloat("staminaAfter", staminaAfterMove);
        
        anim[1].SetBool("blinking", blinking);
        anim[1].SetFloat("staminaBefore", currentStamina);
        anim[1].SetFloat("staminaAfter", staminaAfterMove);

        anim[2].SetBool("blinking", blinking);
        anim[2].SetFloat("staminaBefore", currentStamina);
        anim[2].SetFloat("staminaAfter", staminaAfterMove);
    }
}

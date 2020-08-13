using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { NOT_IN_BATTLE, START, PLAYER_TURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    // UI ELEMENT
    public BattleHUD playerHud;
    public BattleHUD enemyHud;
    public GameObject attackMenu;
    public GameObject mainBattleMenu;

    // BUTTONS
    public Button attack;
    public Button back;
    public Button checkPockets;
    public Button weapons;

    public GameObject battleUI;

    // CHARACTERS
    public Character player;
    public Character enemy;
    public GameObject playerGameObject;
    public GameObject enemyGameObject;

    public GameObject fightText;
    public AudioSource gong;
    public AudioSource punch;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(state == BattleState.NOT_IN_BATTLE)
        {
            battleUI.SetActive(false);
        }
        else if (state == BattleState.START)
        {
            // play FIGHT animation
            StartCoroutine(ShowAndHide(fightText, 2.0f));

            gong.Play();

            // set up battle HUDs
            battleUI.SetActive(true);
            ResetMenu();
            playerHud.SetCharacter(player);
            playerHud.LoadCard();
            enemyHud.SetCharacter(enemy);
            enemyHud.LoadCard();

            state = BattleState.PLAYER_TURN;
        }
        
        if (enemy.currentHealth<=0)
        {
            state = BattleState.WON;
        }

        if (state == BattleState.WON)
        {
            enemyGameObject.SetActive(false);
            battleUI.SetActive(false);
            state = BattleState.NOT_IN_BATTLE;
        }
    }

    public void ResetMenu()
    {
        attackMenu.SetActive(false);
        mainBattleMenu.SetActive(true);
    } 
    
    public void OpenAttackMenu()
    {
        attackMenu.SetActive(true);
        mainBattleMenu.SetActive(false);
    }

    public void PunchAttack()
    {
        int power = 3;
        punch.Play();
        enemy.currentHealth -= power;
        
    }

    IEnumerator ShowAndHide(GameObject go, float delay)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }
}

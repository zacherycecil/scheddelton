using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSystem : MonoBehaviour
{
    // BUTTONS
    public List<Button> weaponButtons;
    public List<Button> attackButtons;
    public List<Button> mainBattleMenuButtons;
    public List<Button> levelUpButtons;

    // MENU OBJECTS
    public GameObject mainBattleMenu;
    public GameObject weaponsMenu;
    public GameObject attackMenu;
    public GameObject levelUpMenu;
    public GameObject overworldMenu;

    // UI ELEMENTS
    public GameObject battleUI;
    public BattleHUD playerHud;
    BattleHUD enemyHud;
    public BattleHUD enemyHudPrototype;
    public Image weaponIcon;
    public BattleSystem battleSystem;
    public GameObject dialoxBox;

    void Update()
    {
        if (battleSystem.state == BattleState.NOT_IN_BATTLE)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleOverworldMenu(battleSystem.GetPlayer());
            }
        }
    }

    // ALL MENUS
    public void InitializeMenu()
    {
        battleUI.SetActive(true);
        GoToMainBattleMenu();
        ButtonsEnabled(true);
    }

    public void CloseMenus()
    {
        battleUI.SetActive(false);
    }

    // OVERWORLD MENU
    public void ToggleOverworldMenu(Player player)
    {
        if (battleUI.activeInHierarchy)
        {
            battleUI.SetActive(false);
            UnityEngine.Debug.Log("1"); 
        }
        else
        {
            battleUI.SetActive(true);
            GoToOverworldMenu();
            LoadPlayerBattleHUD(player);
            dialoxBox.SetActive(false);
            UnityEngine.Debug.Log("2");
        }
    }

    public void GoToOverworldMenu()
    {
        attackMenu.SetActive(false);
        weaponsMenu.SetActive(false);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        overworldMenu.SetActive(true);
    }

    // MAIN BATTLE MENU
    public void ButtonsEnabled(bool enableButtons)
    {
        for (int i = 0; i < mainBattleMenuButtons.Count; i++)
        {
            mainBattleMenuButtons[i].interactable = enableButtons;
        }
    }

    public void GoToMainBattleMenu()
    {
        attackMenu.SetActive(false);
        weaponsMenu.SetActive(false);
        mainBattleMenu.SetActive(true);
        levelUpMenu.SetActive(false);
        overworldMenu.SetActive(false);
    }

    // ATTACK MENU
    public void LoadAttackButtons(List<Attack> attacks)
    {
        for (int i = 0; i < 6; i++) // number of weapon buttons
        {
            attackButtons[i].gameObject.SetActive(false);
        }

        // load attack names
        for (int i = 0; i < attacks.Count; i++)
        {
            attackButtons[i].gameObject.SetActive(true);

            // set button name
            TextMeshProUGUI buttonText = attackButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonText.text = " " + attacks[i].attackName;

            // set button attack
            attackButtons[i].GetComponent<AttackButtonBehaviour>().attack = attacks[i];
        }
    }

    public void GoToAttackMenu()
    {
        attackMenu.SetActive(true);
        weaponsMenu.SetActive(false);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        overworldMenu.SetActive(false);
    }

    // WEAPONS MENU
    public void LoadWeaponButtons(List<Weapon> weapons)
    {
        // load weapon names
        for (int i = 0; i < 6; i++) // number of weapon buttons
        {
            weaponButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            // activate button
            weaponButtons[i].gameObject.SetActive(true);

            // set button name
            TextMeshProUGUI buttonText = weaponButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonText.text = " " + weapons[i].weaponName;
            weaponButtons[i].GetComponent<WeaponButtonBehaviour>().weapon = weapons[i];
        }
    }

    public void GoToWeaponsMenu()
    {
        attackMenu.SetActive(false);
        weaponsMenu.SetActive(true);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        overworldMenu.SetActive(false);
    }

    // LEVELUP MENU
    public void GoToLevelUpMenu()
    {
        attackMenu.SetActive(false);
        weaponsMenu.SetActive(false);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(true);
        overworldMenu.SetActive(false);
    }

    // BATTLE HUDS
    public void SetWeaponIcon(Weapon weapon)
    {
        weaponIcon.GetComponent<Image>().sprite = weapon.GetSprite();
    }

    public void LoadBattleHUDs(Player player, Enemy enemy)
    {
        LoadEnemyBattleHUD(enemy);
        LoadPlayerBattleHUD(player);
    }

    public void LoadEnemyBattleHUD(Enemy enemy)
    {
        enemyHud = Instantiate(enemyHudPrototype, battleUI.transform);
        enemyHud.SetCharacter(enemy);
        enemyHud.LoadCard();
        enemyHud.gameObject.SetActive(true);
    }

    public void LoadPlayerBattleHUD(Player player)
    {
        playerHud.SetCharacter(player);
        playerHud.LoadCard();
        playerHud.gameObject.SetActive(true);
    }

    public void HideBattleHUDs()
    {
        playerHud.gameObject.SetActive(false);
        enemyHud.gameObject.SetActive(false);
    }
}

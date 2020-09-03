using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSystem : MonoBehaviour
{
    // BUTTONS
    public List<Button> mainBattleMenuButtons;
    public List<Button> levelUpButtons;

    // MENU OBJECTS
    public BattleSystem battleSystem;
    public GameObject mainBattleMenu;
    public GameObject weaponsMenu;
    public GameObject attackMenu;
    public GameObject levelUpMenu;
    public GameObject overworldMenu;
    public GameObject sidekicksMenu;
    public GameObject pocketsMenu;

    // UI ELEMENTS
    public GameObject battleUI;
    public BattleHUD playerHud;
    BattleHUD enemyHud;
    public BattleHUD enemyHudPrototype;
    public Image weaponIcon;
    public GameObject dialoxBox;

    public GameObject itemUseConfirmation;
    public ItemButtonBehaviour itemConfirmButton;
    public ItemButtonBehaviour itemDeclineButton;

    // BUTTON PROTOTYPES
    public Button sidekickButtonPrototype;
    public Button weaponButtonPrototype;
    public Button backButtonPrototype;
    public Button attackButtonPrototype;
    public Button itemButtonPrototype;

    // BUTTON CONTIANERS (PANELS)
    public GameObject sidekickMenuPanel;
    public GameObject weaponMenuPanel;
    public GameObject attackMenuPanel;
    public GameObject pocketsMenuPanel;

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

    public void ReturnToMain()
    {
        if (battleSystem.state == BattleState.NOT_IN_BATTLE)
            GoToOverworldMenu();
        else
            GoToMainBattleMenu();
    }

    // OVERWORLD MENU
    public void ToggleOverworldMenu(Player player)
    {
        if (battleUI.activeInHierarchy)
        {
            battleUI.SetActive(false);
            dialoxBox.SetActive(false);
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
        sidekicksMenu.SetActive(false);
        pocketsMenu.SetActive(false);
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
        sidekicksMenu.SetActive(false);
        pocketsMenu.SetActive(false);
    }

    // ATTACK MENU
    public void LoadAttackButtons(List<Attack> attacks)
    {
        foreach (Transform child in attackMenuPanel.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < attacks.Count; i++)
        {
            Button attackButton = Instantiate(attackButtonPrototype, attackMenuPanel.transform);

            // set button name
            TextMeshProUGUI buttonText = attackButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonText.text = " " + attacks[i].attackName;

            // set button attack
            attackButton.GetComponent<AttackButtonBehaviour>().attack = attacks[i];
        }
        Button backButton = Instantiate(backButtonPrototype, attackMenuPanel.transform);
    }

    public void GoToAttackMenu()
    {
        attackMenu.SetActive(true);
        weaponsMenu.SetActive(false);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        overworldMenu.SetActive(false);
        sidekicksMenu.SetActive(false);
        pocketsMenu.SetActive(false);
    }

    // WEAPONS MENU
    public void LoadWeaponButtons(List<Weapon> weapons)
    {
        foreach (Transform child in weaponMenuPanel.transform) 
            Destroy(child.gameObject);

        for (int i = 0; i < weapons.Count; i++)
        {
            Button weaponButton = Instantiate(weaponButtonPrototype, weaponMenuPanel.transform);

            // set button name
            TextMeshProUGUI buttonText = weaponButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonText.text = " " + weapons[i].weaponName;
            weaponButton.GetComponent<WeaponButtonBehaviour>().weapon = weapons[i];
        }
        Button backButton = Instantiate(backButtonPrototype, weaponMenuPanel.transform);
    }

    public void GoToWeaponsMenu()
    {
        attackMenu.SetActive(false);
        weaponsMenu.SetActive(true);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        overworldMenu.SetActive(false);
        sidekicksMenu.SetActive(false);
        pocketsMenu.SetActive(false);
    }

    // SIDEKICKS MENU
    public void LoadSidekickButtons(List<Sidekick> sidekicks)
    {
        foreach (Transform child in sidekickMenuPanel.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < sidekicks.Count; i++)
        {
            Button sidekickButton = Instantiate(sidekickButtonPrototype, sidekickMenuPanel.transform);

            // set button name
            TextMeshProUGUI buttonText = sidekickButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonText.text = " " + sidekicks[i].sidekickName;
            sidekickButton.GetComponent<SidekickButtonBehaviour>().SetSidekick(sidekicks[i]);
        }
        Button backButton = Instantiate(backButtonPrototype, sidekickMenuPanel.transform);
    }

    public void GoToSidekickMenu()
    {
        attackMenu.SetActive(false);
        weaponsMenu.SetActive(false);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        overworldMenu.SetActive(false);
        sidekicksMenu.SetActive(true);
        pocketsMenu.SetActive(false);
    }

    // POCKETS MENU
    public void LoadItemButtons(List<Item> items)
    {
        foreach (Transform child in pocketsMenuPanel.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < items.Count; i++)
        {
            Button itemButton = Instantiate(itemButtonPrototype, pocketsMenuPanel.transform);

            // set button name
            TextMeshProUGUI buttonText = itemButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonText.text = " " + items[i].itemName;
            itemButton.GetComponent<ItemButtonBehaviour>().SetItem(items[i]);
        }
        Button backButton = Instantiate(backButtonPrototype, pocketsMenuPanel.transform);
    }

    public void GoToPocketsMenu()
    {
        attackMenu.SetActive(false);
        weaponsMenu.SetActive(false);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(false);
        overworldMenu.SetActive(false);
        sidekicksMenu.SetActive(false);
        pocketsMenu.SetActive(true);
    }

    public void SetConfirmationItem(Item item)
    {
        itemDeclineButton.item = item;
        itemConfirmButton.item = item;
    }

    public void ItemConfirmationActive(bool isActive)
    {
        itemUseConfirmation.SetActive(isActive);
    }

    public void ItemButtonsEnabled(bool enableButtons)
    {
        foreach (Transform child in pocketsMenuPanel.transform)
        { 
            child.gameObject.GetComponent<Button>().interactable = enableButtons;
        }
    }

    // LEVELUP MENU
    public void GoToLevelUpMenu()
    {
        attackMenu.SetActive(false);
        weaponsMenu.SetActive(false);
        mainBattleMenu.SetActive(false);
        levelUpMenu.SetActive(true);
        overworldMenu.SetActive(false);
        sidekicksMenu.SetActive(false);
        pocketsMenu.SetActive(false);
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

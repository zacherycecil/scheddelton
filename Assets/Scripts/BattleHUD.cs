using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI characterName;
    public Character character;

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    // load stats
    public void LoadCard()
    {
        healthSlider.maxValue = character.maxHealth;
        healthSlider.value = character.currentHealth;
        characterName.text = character.characterName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = character.currentHealth;
    }
}

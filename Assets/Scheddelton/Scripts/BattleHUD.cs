using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

    public Text nameText;
    public Slider hpSlider;

    public void SetHUD(Combat combat)
    {
        nameText.text = combat.enemyName;
        hpSlider.maxValue = combat.maxHealth;
        hpSlider.value = combat.currentHealth;
    }

    public void SetHP(int health)
    {
        hpSlider.value = health;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

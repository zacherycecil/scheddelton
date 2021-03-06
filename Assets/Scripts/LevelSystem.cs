﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    // EXP AND LEVEL
    public int currentExperience;
    public int currentLevel;
    public List<int> levelUpExperience;
    public GameObject levelUpSparklePrototype;

    public bool IncreaseExperience(int exp)
    {
        currentExperience += exp;
        if (currentExperience >= levelUpExperience[currentLevel+1])
        {
            // LEVEL UP
            currentLevel++;
            return true;
        }
        else
            return false;
    }

    public int PercentToNextLevel()
    {
        // return percent til next level
        return (int)(100 * ((float)(currentExperience - levelUpExperience[currentLevel]) / (float)(levelUpExperience[currentLevel+1] - levelUpExperience[currentLevel])));
    }

    public void Sparkle(Character character)
    {
        StartCoroutine(LevelUpSparkle(character));
    }

    public IEnumerator LevelUpSparkle(Character character)
    {
        GameObject levelUpSparkle = Instantiate(levelUpSparklePrototype);
        levelUpSparkle.transform.position = character.gameObject.transform.position;
        yield return new WaitForSeconds(1);
        Destroy(levelUpSparkle);
    }
}

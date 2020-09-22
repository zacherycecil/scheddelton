using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public BattleSystem battleSystem;
    public AudioSource battleMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (battleSystem.inBattle && !battleMusic.isPlaying)
        {
            battleMusic.Play();
        }
        else if (!battleSystem.inBattle)
        {
            battleMusic.Stop();
        }
    }
}

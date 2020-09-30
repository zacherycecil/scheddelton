using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioSource audioSource;

    // MUSIC
    public AudioClip battleMusic;
    public AudioClip overworldMusic;
    public AudioClip victoryMusic;

    public void MuteMusic()
    {
        audioSource.Stop();
    }

    public void PlayMusic(AudioClip clip)
    {
        MuteMusic();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayOverworldMusic()
    {
        audioSource.loop = true;
        PlayMusic(overworldMusic);
    }

    public void PlayBattleMusic()
    {
        audioSource.loop = true;
        PlayMusic(battleMusic);
    }

    public void PlayVictoryMusic()
    {
        StartCoroutine(BufferedSound());
    }

    IEnumerator BufferedSound()
    {
        audioSource.clip = victoryMusic;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(victoryMusic.length);
        audioSource.clip = overworldMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}

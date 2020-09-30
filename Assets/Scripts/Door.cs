using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public String doorName;
    public Sprite doorClosed;
    public Sprite doorOpened;
    public DoorKey key;
    public bool unlocked;

    public void OpenDoor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = doorOpened;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        foreach (Transform doorCollider in this.gameObject.transform)
        {
            doorCollider.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public DoorKey GetKeyToOpen()
    {
        return key;
    }
}

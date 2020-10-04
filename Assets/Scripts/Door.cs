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
    public bool vertical;

    public void OpenDoor()
    {
        if (vertical)
            OpenVerticalDoor();
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = doorOpened;
            this.gameObject.GetComponent<Collider2D>().enabled = false;

            foreach (Transform doorCollider in this.gameObject.transform)
            {
                doorCollider.gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void OpenVerticalDoor()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;

        foreach (Transform door in this.gameObject.transform)
        {
            door.gameObject.SetActive(true);
        }
    }

    public DoorKey GetKeyToOpen()
    {
        return key;
    }
}

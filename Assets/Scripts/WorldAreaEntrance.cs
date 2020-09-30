using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldAreaEntrance : MonoBehaviour
{
    public enum EntryOffset
    {
        UP, DOWN, LEFT, RIGHT
    }
    public String entranceName;
    public BoxCollider2D entrancePosition;
    public WorldArea area;
    public EntryOffset offset;

    public Vector2 GetEntryPosition()
    {
        Vector2 position = entrancePosition.transform.position; 
        if (offset == EntryOffset.UP)
        {
            position.y += 0.2f;
        }
        else if (offset == EntryOffset.DOWN)
        {
            position.y -= 0.2f;
        }
        else if (offset == EntryOffset.LEFT)
        {
            position.x -= 0.2f;
        }
        else if (offset == EntryOffset.RIGHT)
        {
            position.x += 0.2f;
        }
        return position;
    }
}

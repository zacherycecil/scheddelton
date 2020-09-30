using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogEvent : MonoBehaviour
{
    public Interactable interactable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Scheddelton"))
        {
            interactable.AddDialogToBuffer();
            this.gameObject.SetActive(false);
        }
    }
}

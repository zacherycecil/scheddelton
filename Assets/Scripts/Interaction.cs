using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public GameObject interactionBox;
    public Player player;
    public KeyCode interactKey;
    public bool itemPickedUp;
    public DialogSystem dialog;
    public GameObject dialogIcon;
    public GameObject dialogIconPrototype;
    public GameObject currentInteractable;

    // bools
    public bool itemInRange;
    public bool friendlyInRange;
    public bool sidekickInRange;

    // interactables
    Interactable item;
    Interactable friendly;
    Interactable sidekick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!player.GetMovementLocked()) // check if player movement is locked
            if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)) // check if 
                interactionBox.transform.position = new Vector2((0.25f * Input.GetAxisRaw("Horizontal")) - 0.25f + player.transform.position.x, 0.25f * Input.GetAxisRaw("Vertical") + player.transform.position.y);

        // INTERACTION
        if (player.isInMenu)
        {
            if(dialogIcon!= null)
                dialogIcon.SetActive(false);
        }
        else
        {
            if (itemPickedUp && Input.GetKeyDown(interactKey))
            {
                dialog.CloseDialogBox();
                player.SetMovementLocked(false);
                player.IsInDialog(false);
                itemPickedUp = false;
            }
            else if (itemInRange && Input.GetKeyDown(interactKey))
            {
                player.SetMovementLocked(true);
                player.IsInDialog(true);
                item.PickupItem(item.gameObject.GetComponent<Item>());
                itemPickedUp = true;
            }
            else if (friendlyInRange && Input.GetKeyDown(interactKey))
            {
                player.SetMovementLocked(true);
                player.IsInDialog(true);
                friendly.NextDialog();
            }
            else if (sidekickInRange && Input.GetKeyDown(interactKey))
            {
                player.SetMovementLocked(true);
                player.IsInDialog(true);
                sidekick.NextDialog();
            }

            if(dialogIcon!=null)
                dialogIcon.SetActive(true);
        }

        // DIALOG ICON
        if (friendlyInRange)
        {
            dialogIcon.transform.position = friendly.gameObject.transform.position + new Vector3(0f, 0.2f, 175.6206f);
        }
        else if (sidekickInRange)
        {
            dialogIcon.transform.position = sidekick.gameObject.transform.position + new Vector3(0f, 0.2f, 175.6206f);
        }
        else
            Destroy(dialogIcon);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            item = collision.gameObject.GetComponent<Interactable>();
            itemInRange = true;
        }
        else if (collision.gameObject.CompareTag("Friendly"))
        {
            friendly = collision.gameObject.GetComponent<Interactable>();
            friendlyInRange = true;
            if (dialogIcon != null)
                Destroy(dialogIcon);
            dialogIcon = Instantiate(dialogIconPrototype);
        }
        else if (collision.gameObject.CompareTag("Sidekick"))
        {
            sidekick = collision.gameObject.GetComponent<Interactable>();
            sidekickInRange = true;
            if (!friendlyInRange)
            {
                if (dialogIcon != null)
                    Destroy(dialogIcon);
                dialogIcon = Instantiate(dialogIconPrototype);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            itemInRange = false;
        }
        else if (collision.gameObject.CompareTag("Friendly"))
        {
            friendlyInRange = false;
        }
        else if (collision.gameObject.CompareTag("Sidekick"))
        {
            sidekickInRange = false;
        }
    }
}
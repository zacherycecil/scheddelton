using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public GameObject interactionBox;
    public Player player;
    public BattleSystem battleSystem;
    public KeyCode interactKey;
    public DialogSystem dialog;
    public GameObject dialogIcon;
    public GameObject dialogIconPrototype;
    public GameObject currentInteractable;

    // bools
    public bool itemInRange;
    public bool friendlyInRange;
    public bool sidekickInRange;
    public bool doorInRange;

    // interactables
    Interactable item;
    Interactable friendly;
    Interactable sidekick;
    Door door;

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
        if (player.isInMenu || battleSystem.inBattle)
        {
            if(dialogIcon!= null)
                dialogIcon.SetActive(false);
        }
        else if(!player.isInDialog)
        {
            if (itemInRange && Input.GetKeyDown(interactKey))
            {
                item.PickupItem(item.gameObject.GetComponent<Item>());
            }
            else if (doorInRange && Input.GetKeyDown(interactKey))
            {
                player.isInDialog = true;
                if (player.HasKey(door.GetKeyToOpen()))
                {
                    door.GetKeyToOpen().Use();
                    door.OpenDoor();
                    dialog.SystemDialogBuffer(door.GetKeyToOpen().actionString);
                }
                else
                {
                    dialog.SystemDialogBuffer("The door is locked. " + player.characterName + " checks his pockets but he does not have the key.");
                }
            }
            else if (friendlyInRange && Input.GetKeyDown(interactKey))
            {
                friendly.AddDialogToBuffer();
            }
            else if (sidekickInRange && Input.GetKeyDown(interactKey))
            {
                sidekick.AddDialogToBuffer();
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
        else if (collision.gameObject.CompareTag("Door"))
        {
            door = collision.gameObject.GetComponent<Door>();
            doorInRange = true;
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
        else if (collision.gameObject.CompareTag("Door"))
        {
            doorInRange = false;
        }
    }
}
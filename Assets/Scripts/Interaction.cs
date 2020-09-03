using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public GameObject interactionBox;
    public Player player;
    public KeyCode interactKey;
    public bool inRange;
    public UnityEvent action;

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

        if(inRange && Input.GetKeyDown(interactKey))
        {
            player.SetMovementLocked(true);
            player.IsInDialog(true);
            // player.GetComponent<PlayerMovement>().SetIdleAnimation();
            action.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Friendly"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
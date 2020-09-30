using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Player player;
    Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public BattleSystem battleSystem;
    public Interaction interaction;
    public DialogSystem dialog;
    public WorldArea currentArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!battleSystem.inBattle && !player.isInDialog)
            dialog.currentDialogTarget = null;

        if(dialog.currentDialogTarget == null)
        {
            if (transform.position != player.gameObject.transform.position)
            {
                Vector3 targetPosition = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, transform.position.z);

                targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
        }
        else
        {
            target = dialog.currentDialogTarget.transform;
            if (transform.position != (target.position - player.gameObject.transform.position)/2)
            {
                Vector3 targetPosition = new Vector3((target.position + player.gameObject.transform.position).x/2, (target.position + player.gameObject.transform.position).y/2, transform.position.z);

                targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
        }
    }

    public void SetCameraPosition(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(pos.y, minPosition.y, maxPosition.y);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void SetCameraMinMax(Vector2 max, Vector2 min)
    {
        maxPosition = max;
        minPosition = min;
    }
}

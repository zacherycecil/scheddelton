using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeArea : MonoBehaviour
{
    public WorldAreaEntrance areaEntrance;
    public GameObject player;
    public GameObject mainCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Scheddelton"))
        {
            player.transform.position = areaEntrance.GetEntryPosition();
            player.GetComponent<Player>().currentSidekick.gameObject.transform.position = areaEntrance.GetEntryPosition();
            mainCamera.GetComponent<CameraMovement>().SetCameraMinMax(areaEntrance.area.maxCameraPosition, areaEntrance.area.minCameraPosition);
            mainCamera.GetComponent<CameraMovement>().SetCameraPosition(player.transform.position);
        }
    }
}
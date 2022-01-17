using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameObj_DroneMinigame2 : MonoBehaviour
{
    public Hide_DroneMinigame2 hideObjExact;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController_DroneMinigame2.instance.isCompleted)
        {
            hideObjExact = collision.GetComponent<Hide_DroneMinigame2>();
        }
    }
}

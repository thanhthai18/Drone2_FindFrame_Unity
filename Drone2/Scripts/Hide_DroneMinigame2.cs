using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide_DroneMinigame2 : MonoBehaviour
{
    public int indexCheck;
    public static event Action<int> Event_OnGetIndex;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (indexCheck == 0)
            {
                GameController_DroneMinigame2.instance.isCompleted = true;
                Event_OnGetIndex?.Invoke(indexCheck);
            }
            else
            {
                Debug.Log("sai");
                GameController_DroneMinigame2.instance.isCompleted = false;
            }
        }
        else if (collision.gameObject.CompareTag("People"))
        {
            if (indexCheck == 1)
            {
                GameController_DroneMinigame2.instance.isCompleted = true;
                Event_OnGetIndex?.Invoke(indexCheck);
            }
            else
            {
                Debug.Log("sai");
                GameController_DroneMinigame2.instance.isCompleted = false;
            }
        }
        else if (collision.gameObject.CompareTag("Balloon"))
        {
            if (indexCheck == 2)
            {
                GameController_DroneMinigame2.instance.isCompleted = true;
                Event_OnGetIndex?.Invoke(indexCheck);
            }
            else
            {
                Debug.Log("sai");
                GameController_DroneMinigame2.instance.isCompleted = false;
            }
        }
        else if (collision.gameObject.CompareTag("ColorHive"))
        {
            if (indexCheck == 3)
            {
                GameController_DroneMinigame2.instance.isCompleted = true;
                Event_OnGetIndex?.Invoke(indexCheck);
            }
            else
            {
                Debug.Log("sai");
                GameController_DroneMinigame2.instance.isCompleted = false;
            }
        }
        else if (collision.gameObject.CompareTag("BoundScreen"))
        {
            if (indexCheck == 4)
            {
                GameController_DroneMinigame2.instance.isCompleted = true;
                Event_OnGetIndex?.Invoke(indexCheck);
            }
            else
            {
                Debug.Log("sai");
                GameController_DroneMinigame2.instance.isCompleted = false;
            }
        }
        else if (collision.gameObject.CompareTag("TrashRecycleTruck"))
        {
            if (indexCheck == 5)
            {
                GameController_DroneMinigame2.instance.isCompleted = true;
                Event_OnGetIndex?.Invoke(indexCheck);
            }
            else
            {
                Debug.Log("sai");
                GameController_DroneMinigame2.instance.isCompleted = false;
            }
        }

    }
}

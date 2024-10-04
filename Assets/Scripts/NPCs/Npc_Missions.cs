using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Npc_Missions : MonoBehaviour
{
    public GameObject ExclamationUI;
    public Missions missionToActivate;
    private bool playerIsClose;

    void Update()
    {
        if (missionToActivate.state == Missions.MissionState.Inactive)
        {
            ExclamationUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && playerIsClose)
            {
                ActivateMission();
            }
        }
        else
        {
            ExclamationUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }

    void ActivateMission()
    {
        missionToActivate.state = Missions.MissionState.Active;
    }
}

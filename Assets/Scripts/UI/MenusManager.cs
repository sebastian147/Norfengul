using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MenusManager : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject PauseMenuUI;
    public GameObject InventoryUI;
    public GameObject JouneyUI;
    public GameObject PlayerObj;
    public Animator Anim;
    public InventoryManager inventoryManager;
    public JournalManager journalManager;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(GameIsPause)
            {
                Resume();
            }else
            {
                Pause();
                Anim.SetBool("InInventory", true);
                PauseMenuUI.SetActive(false);
                JouneyUI.SetActive(false);
                InventoryUI.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPause)
            {
               Resume();
            }else
            {
                Pause();
                Anim.SetBool("InPause", true);
                JouneyUI.SetActive(false);
                InventoryUI.SetActive(false);
                PauseMenuUI.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(GameIsPause)
            {
               Resume();
            }else
            {
                Pause();
                Anim.SetBool("InJourney", true);
                InventoryUI.SetActive(false);
                PauseMenuUI.SetActive(false);
                JouneyUI.SetActive(true);
                journalManager.CheckMissions();
                journalManager.ClassifyMissions();
                journalManager.ShowAllMissions();
            }
        }
    }

    public void Resume()
    {
        Anim.SetBool("InPause", false);
        Anim.SetBool("InInventory", false);
        Anim.SetBool("InJourney", false);
        InventoryUI.SetActive(false);
        JouneyUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        GameIsPause = false;
        Time.timeScale = 1f;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void BackMainMenu()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalManager : MonoBehaviour
{
    public List<Missions> missions;
    private List<Missions> activeMissions = new List<Missions>();
    private List<Missions> completedMissions = new List<Missions>();

    public TextMeshProUGUI missionNameText;
    public TextMeshProUGUI missionDescriptionText;
    
    public GameObject activeBox;
    public GameObject completedBox;
    public GameObject botonPrefab;

    public TrackedMissions trackedMissions;
    public InventoryManager inventoryManager;

    private void Start()
    {
        ClassifyMissions();
        ShowAllMissions();
    }

    private void UpdateMissionUI(Missions mission)
    {
        missionNameText.text = mission.missionName;
        missionDescriptionText.text = mission.description;
        trackedMissions.missions = mission;
    }

    public void ClassifyMissions()
    {
        activeMissions.Clear();
        completedMissions.Clear();

        foreach (Missions mission in missions)
        {
            if (mission.state == Missions.MissionState.Active)
            {
                activeMissions.Add(mission);
            }
            else if (mission.state == Missions.MissionState.Completed)
            {
                completedMissions.Add(mission);
            }
        }
    }

    public void ShowAllMissions()
    {
        ClearButtons(activeBox);
        ClearButtons(completedBox);

        InstantiateButtons(activeMissions, activeBox);
        InstantiateButtons(completedMissions, completedBox);
    }

    private void InstantiateButtons(List<Missions> missionsList, GameObject container)
    {
        foreach (Missions mission in missionsList)
        {
            GameObject buttonInstance = Instantiate(botonPrefab);
            Button buttonComponent = buttonInstance.GetComponent<Button>();

            if (buttonComponent != null)
            {
                TextMeshProUGUI buttonText = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = mission.missionName;
                }
                buttonComponent.onClick.AddListener(() => UpdateMissionUI(mission));
            }

            buttonInstance.transform.SetParent(container.transform);
        }
    }

    private void ClearButtons(GameObject container)
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void CheckMissions()
    {
        foreach (Missions mission in activeMissions)
        {
            if (HaveItemMissionRequirements(mission))
            {
                mission.state = Missions.MissionState.Completed;
                GiveRewards(mission);
            }
        }
    }

    private void GiveRewards(Missions mission)
    {
        inventoryManager.Coins += mission.coinsReward;

        if (mission.itemRewards != null)
        {
            foreach (Items missionItem in mission.itemRewards)
            {
                inventoryManager.AddItem(missionItem);
            }
        }
    }

    private bool HaveItemMissionRequirements (Missions missionToAnalyze)
    {
        return inventoryManager.FindItem(missionToAnalyze.requiredItems) != -1;
    }
}
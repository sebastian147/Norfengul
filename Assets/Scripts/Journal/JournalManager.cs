using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class JournalManager : MonoBehaviour
{

    public List<Missions> mission;
    List<Missions> activeMissions = new List<Missions>();
    List<Missions> completedMissions = new List<Missions>();
    public TextMeshProUGUI missionNameText;
    public TextMeshProUGUI missionDescriptionText;
    public GameObject ActiveBox;
    public GameObject CompletedBox;
    public GameObject BotonPrefab;

    public void UpdateMissionUI(Missions mission)
    {
        missionNameText.text = mission.missionName;
        missionDescriptionText.text = mission.description;
    }

    public void ClassifyMisions ()
    {
        activeMissions.Clear();
        completedMissions.Clear();

        foreach (Missions missionItem in mission)
        {
            if (missionItem.state == Missions.MissionState.Active)
            {
                activeMissions.Add(missionItem);
            }
            else if (missionItem.state == Missions.MissionState.Completed)
            {
                completedMissions.Add(missionItem);
            }
        }
    }

    public void ShowAllMissions ()
    {
        foreach (Transform child in ActiveBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Missions missionItem in activeMissions)
        {
            GameObject buttonInstance = Instantiate(BotonPrefab);
            Button buttonComponent = buttonInstance.GetComponent<Button>();
            
            if (buttonComponent != null)
            {
                TextMeshProUGUI buttonText = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = missionItem.missionName;
                }
                buttonComponent.onClick.AddListener(() => UpdateMissionUI(missionItem));
            }

            buttonInstance.transform.SetParent(ActiveBox.transform);
        }

        foreach (Transform child in CompletedBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Missions missionItem in completedMissions)
        {
            GameObject buttonInstance = Instantiate(BotonPrefab);
            Button buttonComponent = buttonInstance.GetComponent<Button>();
            
            if (buttonComponent != null)
            {
                TextMeshProUGUI buttonText = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = missionItem.missionName;
                }
                buttonComponent.onClick.AddListener(() => UpdateMissionUI(missionItem));
            }

            buttonInstance.transform.SetParent(CompletedBox.transform);
        }
        
    }

}

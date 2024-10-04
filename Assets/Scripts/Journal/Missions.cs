using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "Missions/Mission")]
public class Missions : ScriptableObject
{

    public enum MissionState
    {
        Inactive,
        Active,
        Completed
    }

    public MissionState state; 

    public string missionName;
    [TextArea] public string description;
    public List<string> requiredAmount = new List<string>();
    public Items requiredItems;
    public int experienceReward;
    public int coinsReward;
    public List<Items> itemRewards = new List<Items>();

    public Transform NPCPosition;
    
}


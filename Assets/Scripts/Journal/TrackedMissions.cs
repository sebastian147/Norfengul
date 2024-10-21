using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackedMissions : MonoBehaviour
{
    public Missions missions;
    public TextMeshProUGUI missionNameText;
    public GameObject TrackedHUD;
    public GameObject ObjetivesBox;

    public GameObject CompasHUD;

    private float moveSpeed = 1000f;
    private float distanceFromObject = 2.5f;

    public Transform target;

    void Update()
    {
        if(TrackedHUD != null && ObjetivesBox != null)
        {
            TrackedUI();
            CompassMission();
        }
    }

    private void TrackedUI()
    {
        if (missions != null && missionNameText != null && missions.state == Missions.MissionState.Active)
        {
            TrackedHUD.SetActive(true);
            CompasHUD.SetActive(true);
            missionNameText.text = missions.missionName;
            target = FindTargetObject(missions.requiredItems.itemName);
        }
        else
        {
            TrackedHUD.SetActive(false);
            CompasHUD.SetActive(false);
        }
    }

    private void CompassMission()
    {
        if (CompasHUD != null && target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;
            Vector3 directionXY = new Vector3(directionToTarget.x, directionToTarget.y, 0f).normalized;

            Vector3 desiredPosition = transform.position + directionXY * distanceFromObject;
            CompasHUD.transform.position = Vector3.Lerp(CompasHUD.transform.position, desiredPosition, moveSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, -directionXY);
            CompasHUD.transform.rotation = Quaternion.Slerp(CompasHUD.transform.rotation, targetRotation, moveSpeed * Time.deltaTime);

             if (directionToTarget.magnitude < 3f)
            {
                CompasHUD.SetActive(false);
            }
            else
            {
                CompasHUD.SetActive(true);
            }
        }
    }

    private Transform FindTargetObject(string objectName)
    {
        GameObject targetObject = GameObject.Find(objectName);

        if (targetObject != null)
        {
            return targetObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto con el nombre: " + objectName);
            return null;
        }
    }
}

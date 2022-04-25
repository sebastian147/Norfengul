using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public CinemachineVirtualCamera cam;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    void Start() 
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        var player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //PhotonNetwork.Instantiate(enemyPrefab.name, randomPosition, Quaternion.identity);
        cam.LookAt = player.transform;
        cam.Follow = player.transform;
    }
    /*public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }*/
}

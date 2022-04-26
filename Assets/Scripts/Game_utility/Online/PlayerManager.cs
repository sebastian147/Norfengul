using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public GameObject playerPrefab = null;
    private CinemachineVirtualCamera virtualCamera;
    private GameObject camObj;
    GameObject player;


    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start() 
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }
    void CreateController()
    {
        Transform spawnpoint = SpawnManager.Instance.GetSpawnPoint();
        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnpoint.position, spawnpoint.rotation, 0, new object []{PV.ViewID});
        //PhotonNetwork.Instantiate(enemyPrefab.name, randomPosition, Quaternion.identity);
        camObj = GameObject.Find("CM vcam1");
        virtualCamera = camObj.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.LookAt = player.transform;
        virtualCamera.Follow = player.transform;
    }
    public void Die()
    {
        StartCoroutine(WaitForPlay(2.0f));
    }

    IEnumerator WaitForPlay(float waitTime) 
    { 
        yield return new WaitForSeconds(waitTime);
        PhotonNetwork.Destroy(player);
        CreateController();
    }
}

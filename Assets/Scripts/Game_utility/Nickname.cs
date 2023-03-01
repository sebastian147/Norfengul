using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Nickname : MonoBehaviour
{
    public void Start() 
    {
        GetComponent<TextMesh>().text =  PhotonNetwork.NickName;
    }
}

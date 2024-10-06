using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Nickname : MonoBehaviourPunCallbacks
{
    public PhotonView Pv;

    // Initializes the player's PhotonView and sets their nickname
    public void Start() 
    {
        Pv = GetComponent<PhotonView>();
        Name(PhotonNetwork.NickName);
    }

    // Sends the player's name to all clients
    public void Name(string name)
    {
        // Sends an RPC call to synchronize the name across all clients
        Pv.RPC("RPC_Name", RpcTarget.AllBuffered, name);
    }

    [PunRPC]
    public void RPC_Name(string name)
    {
        // Displays the nickname on a TextMesh component attached to the same GameObject
        GetComponent<TextMesh>().text = name;
    }
}
/*to do
*      handle pv null and name null 
*/
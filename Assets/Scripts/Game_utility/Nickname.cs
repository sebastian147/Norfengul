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
        public void Start() 
        {
                Pv= GetComponent<PhotonView>();
                Name(PhotonNetwork.NickName);
        }
        public void Name(string name)
        {
                Pv.RPC("RPC_Name", RpcTarget.AllBuffered, name);//nombre funcion, a quien se lo paso, valor
        }
        [PunRPC]
        public void RPC_Name(string name)
        {
                GetComponent<TextMesh>().text =  name;
        }
}

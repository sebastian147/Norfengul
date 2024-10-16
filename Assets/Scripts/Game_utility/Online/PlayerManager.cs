using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using System.IO;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
        PhotonView PV;
        public GameObject playerPrefab = null;
        private CinemachineVirtualCamera virtualCamera;
        private GameObject camObj;
        GameObject player;

        public string Arma;
        public string Barbas;
        public string Cuerpo;
        public string Escudos;
        public string Pelos;
        public float R;
        public float G;
        public float B;

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
                //skin
                object[] instantiationData = this.GetComponent<PhotonView>().InstantiationData;
                Barbas = (string)instantiationData[0];
                Arma = (string)instantiationData[1];
                Cuerpo = (string)instantiationData[2];
                Escudos = (string)instantiationData[3];
                Pelos = (string)instantiationData[4];
                R = (float)instantiationData[5];
                G = (float)instantiationData[6];
                B = (float)instantiationData[7];
                


                player.GetComponent<playerMovement>().changeSkin(Arma,Barbas,Cuerpo,Escudos,Pelos, R, G, B, PhotonNetwork.NickName);//deberia llamar a esta funcion dentro de player y aca solo deberia pasarle los datos
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class RoomManager : MonoBehaviourPunCallbacks
{
        public static RoomManager Instance;    
        public GameObject playerManager;
        string folderPath = "Assets/Resources/Skins_Huscarle";

        string skinRandomCarpeta(string path,string pathNT)
        {
                System.Random random = new System.Random();
                string[] fileNames = Directory.GetFiles(path).Where(name => name.EndsWith(".spriteLib")).ToArray();
                string randomFilePath   = fileNames[random.Next(0, fileNames.Length)];
                randomFilePath = randomFilePath.Replace(".spriteLib", "");
                return randomFilePath.Replace(pathNT, "");
        }
        private void Awake() {
                if(Instance)
                {
                        Destroy(gameObject);//only one
                        return;
                }
                DontDestroyOnLoad(gameObject);
                Instance = this;
                //skins

        }
        public override void OnEnable() {
                base.OnEnable();
                SceneManager.sceneLoaded += OnSceneLoaded;
        }
        public override void OnDisable() {
                base.OnDisable();
                SceneManager.sceneLoaded -= OnSceneLoaded;

        }
        void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
                if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName((string)PhotonNetwork.CurrentRoom.CustomProperties["Scene"]))//game scene 
                {
                        string Barbas = skinRandomCarpeta(folderPath+"/Barbas", "Assets/Resources/");
                        string Arma = skinRandomCarpeta(folderPath+"/Armas", "Assets/Resources/");
                        string Cuerpo = skinRandomCarpeta(folderPath+"/Cuerpo", "Assets/Resources/");
                        string Escudos = skinRandomCarpeta(folderPath+"/Escudos", "Assets/Resources/");
                        string Pelos = skinRandomCarpeta(folderPath+"/Pelos", "Assets/Resources/");
                        object[] customData = new object[] { Barbas, Arma, Cuerpo, Escudos, Pelos };

                        PhotonNetwork.Instantiate(playerManager.name, Vector2.zero,Quaternion.identity,0,customData);

                }
        }
        void Start() 
        {
        }

}

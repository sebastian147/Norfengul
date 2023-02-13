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
        string folderPathA = "Assets/Resources/Armas_Huscarle";

        string skinRandomCarpeta(string path,string pathNT, string end)
        {
                System.Random random = new System.Random();
                string[] fileNames = Directory.GetFiles(path).Where(name => name.EndsWith(end)).ToArray();
                string randomFilePath   = fileNames[random.Next(0, fileNames.Length)];
                randomFilePath = randomFilePath.Replace(end, "");
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
                        string Barbas = skinRandomCarpeta(folderPath+"/Barbas", "Assets/Resources/",".spriteLib");
                        string Arma = skinRandomCarpeta(folderPathA, "Assets/Resources/",".asset");
                        string Cuerpo = skinRandomCarpeta(folderPath+"/Cuerpo", "Assets/Resources/",".spriteLib");
                        string Escudos = skinRandomCarpeta(folderPath+"/Escudos", "Assets/Resources/",".spriteLib");
                        string Pelos = skinRandomCarpeta(folderPath+"/Pelos", "Assets/Resources/",".spriteLib");
                        object[] customData = new object[] { Barbas, Arma, Cuerpo, Escudos, Pelos };

                        PhotonNetwork.Instantiate(playerManager.name, Vector2.zero,Quaternion.identity,0,customData);

                }
        }
        void Start() 
        {
        }

}

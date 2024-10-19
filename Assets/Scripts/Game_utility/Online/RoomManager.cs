using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using static JsonFunctions;

public class RoomManager : MonoBehaviourPunCallbacks
{
        public static RoomManager Instance;    
        public GameObject playerManager;
        string folderPath = "Assets/Resources/Skins_Huscarle";
        string folderPathA = "Assets/Resources/Armas_Huscarle";
        string archivoJson = "Assets/Scripts/Game_utility/JSON/ColoresPeloyBarba.json";
        string skinJson = "Assets/Scripts/Game_utility/JSON/characterSkin.json";

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
                        ColorList colorSelect = CargarJSON<ColorList>(archivoJson);
                        Debug.Log(colorSelect);
                        int ran = Random.Range(0, colorSelect.colors.Count);
                        float R = colorSelect.colors[ran].r;
                        float B = colorSelect.colors[ran].b;
                        float G = colorSelect.colors[ran].g;

                        CharacterSkin skins = CargarJSON<CharacterSkin>(skinJson);
                        string Barbas = skinRandomCarpeta(folderPath+"/Barbas", "Assets/Resources/",".spriteLib");
                        string Arma = skins.Arma;
                        string Cuerpo = skins.Cuerpo;
                        string Escudos = skins.Escudos;
                        string Pelos = skinRandomCarpeta(folderPath+"/Pelos", "Assets/Resources/",".spriteLib");

                        object[] customData = new object[] { Barbas, Arma, Cuerpo, Escudos, Pelos, R, G, B};

                        PhotonNetwork.Instantiate(playerManager.name, Vector2.zero,Quaternion.identity,0,customData);
                }
        }
        void Start() 
        {
        }

}

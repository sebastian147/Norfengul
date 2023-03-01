using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;


public class RoomManager : MonoBehaviourPunCallbacks
{
        [System.Serializable]
        public class ColorData
        {
                public float r;
                public float g;
                public float b;
                public float a;
                public string name;
        }

        [System.Serializable]
        private class ColorList
        {
                public List<ColorData> colors;
        }

        [System.Serializable]
        public class CharacterSkin 
        {
                public string arma;
                public string Barbas;
                public string Cuerpo;
                public string Escudos;
                public string Pelos;
        }

        public static RoomManager Instance;    
        public GameObject playerManager;
        string folderPath = "Assets/Resources/Skins_Huscarle";
        string folderPathA = "Assets/Resources/Armas_Huscarle";
        string archivoJson = "Assets/Scripts/Game_utility/JSON/ColoresPeloyBarba.json";
        string skinJson = "Assets/Scripts/Game_utility/JSON/characterSkin.json";
        
        private CharacterSkin CargarSkin() 
        {
                string contenido = File.ReadAllText(skinJson);
                Debug.Log(contenido);
                CharacterSkin skin = JsonUtility.FromJson<CharacterSkin>(contenido);
                Debug.Log(skin.Cuerpo);
                Debug.Log(skin.Escudos);
                return skin;
        }

        private ColorList CargarColores()
        {
                string contenido = File.ReadAllText(archivoJson);
                Debug.Log(contenido);
                ColorList colorArray = JsonUtility.FromJson<ColorList>(contenido);
                return colorArray;
        }

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
                        ColorList colorSelect = CargarColores();
                        int ran = Random.Range(0, colorSelect.colors.Count);
                        float R = colorSelect.colors[ran].r;
                        float B = colorSelect.colors[ran].b;
                        float G = colorSelect.colors[ran].g;

                        CharacterSkin skins = CargarSkin();
                        Debug.Log(skins.Cuerpo);
                        Debug.Log(skins.Escudos);
                        string Barbas = skinRandomCarpeta(folderPath+"/Barbas", "Assets/Resources/",".spriteLib");
                        Debug.Log(Barbas);
                        string Arma = skinRandomCarpeta(folderPathA, "Assets/Resources/",".asset");
                        string Cuerpo = skins.Cuerpo;
                        string Escudos = skins.Escudos;
                        Debug.Log(Escudos);
                        string Pelos = skinRandomCarpeta(folderPath+"/Pelos", "Assets/Resources/",".spriteLib");

                        object[] customData = new object[] { Barbas, Arma, Cuerpo, Escudos, Pelos, R, G, B};

                        PhotonNetwork.Instantiate(playerManager.name, Vector2.zero,Quaternion.identity,0,customData);

                }
        }
        void Start() 
        {
        }

}

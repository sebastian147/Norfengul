using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class characterSkinSave
{
    public GameObject Arma;
    public UnityEngine.U2D.Animation.SpriteLibraryAsset Barbas;
    public UnityEngine.U2D.Animation.SpriteLibraryAsset Cuerpo;
    public UnityEngine.U2D.Animation.SpriteLibraryAsset Escudos;
    public UnityEngine.U2D.Animation.SpriteLibraryAsset Pelos;
}

public class CambiodeSkin : MonoBehaviour
{
    [SerializeField] public GameObject ArmaSelect;
    [SerializeField] public UnityEngine.U2D.Animation.SpriteLibraryAsset BarbaSelect;
    [SerializeField] public UnityEngine.U2D.Animation.SpriteLibraryAsset CuerpoSelect;
    [SerializeField] public UnityEngine.U2D.Animation.SpriteLibraryAsset EscudoSelect;
    [SerializeField] public UnityEngine.U2D.Animation.SpriteLibraryAsset PeloSelect;

    public string archivoCharacter = "Assets/Scripts/Game_utility/JSON/characterSkin.json";
    public characterSkinSave Skin = new characterSkinSave();

   public void GuardarSkin()
    {
        characterSkinSave skin = new characterSkinSave()
        {
            Arma = ArmaSelect,
            Barbas = BarbaSelect,
            Cuerpo = CuerpoSelect,
            Escudos = EscudoSelect,
            Pelos = PeloSelect
        };

        string skinJSON = JsonUtility.ToJson(skin);
        File.WriteAllText(archivoCharacter, skinJSON);
    }
}

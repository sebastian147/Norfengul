using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CambioSkin : MonoBehaviour
{
    protected string pathArma = "Armas_Huscarle/";
    protected string pathBarbas = "Skins_Huscarle/Barbas/";
    protected string pathCuerpo = "Skins_Huscarle/Cuerpo/";
    protected string pathEscudos = "Skins_Huscarle/Escudos/";
    protected string pathPelos = "Skins_Huscarle/Pelos/";

    public string ArmaSelect = "Arma";
    public string BarbaSelect = "Huscarle_Barba1";
    public string CuerpoSelect = "Huscarle_Nude";
    public string EscudoSelect = "Huscarle_EscudoMadera";
    public string PeloSelect = "Huscarle_Pelo1";

    protected string archivoCharacter = "Assets/Scripts/Game_utility/JSON/characterSkin.json";
    protected characterSkinSave Skin = new characterSkinSave();
    
    public void GuardarSkin()
    {
        characterSkinSave skin = new characterSkinSave()
        {
            arma = pathArma + ArmaSelect,
            Barbas = pathBarbas + BarbaSelect,
            Cuerpo = pathCuerpo + CuerpoSelect,
            Escudos = pathEscudos + EscudoSelect,
            Pelos = pathPelos + PeloSelect
        };

        string skinJSON = JsonUtility.ToJson(skin);
        File.WriteAllText(archivoCharacter, skinJSON);
    }
}

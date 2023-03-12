using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static JsonFunctions;
using TMPro;

public class CambioSkin : MonoBehaviour
{
    private string pathArma = "Armas_Huscarle/";
    private string pathBarbas = "Skins_Huscarle/Barbas/";
    private string pathCuerpo = "Skins_Huscarle/Cuerpo/";
    private string pathEscudos = "Skins_Huscarle/Escudos/";
    private string pathPelos = "Skins_Huscarle/Pelos/";

    public string ArmaSelect = "Arma";
    public string BarbaSelect = "Huscarle_Barba1";
    public string CuerpoSelect = "Huscarle_Nude";
    public string EscudoSelect = "Huscarle_EscudoMadera";
    public string PeloSelect = "Huscarle_Pelo1";

    private string archivoCharacter = "Assets/Scripts/Game_utility/JSON/characterSkin.json";
    private string archivoPlayer = "Assets/Scripts/Game_utility/JSON/playerData.json";
    private string name;
    public void SaveName(TMP_InputField   textMeshPro)
    {
        if (textMeshPro != null) // Verificar que el componente TextMeshPro existe
        {
                name = textMeshPro.text; // Extraer el texto del TextMeshPro y asignarlo a la variable name
        }
    }
    public void GuardarSkin()
    {
        CharacterSkin skin = new CharacterSkin()
        {
            arma = pathArma + ArmaSelect,
            Barbas = pathBarbas + BarbaSelect,
            Cuerpo = pathCuerpo + CuerpoSelect,
            Escudos = pathEscudos + EscudoSelect,
            Pelos = pathPelos + PeloSelect
        };
        EscribirDatoJSON<PlayerData, string>(archivoPlayer, datos => datos.Name, (datos, valor) => datos.Name = valor, name);
        GuardarJSON<CharacterSkin>(archivoCharacter,skin);

    }
}

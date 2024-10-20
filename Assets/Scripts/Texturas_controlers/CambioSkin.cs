using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static JsonFunctions;
using TMPro;
using System;

/// <summary>
/// Clase encargada de cambiar y guardar las skins del personaje.
/// </summary>
public class CambioSkin : MonoBehaviour
{
    private readonly string pathArma = "Armas_Huscarle/"; // Ruta de las armas
    private readonly string pathBarbas = "Skins_Huscarle/Barbas/"; // Ruta de las barbas
    private readonly string pathCuerpo = "Skins_Huscarle/Cuerpo/"; // Ruta de los cuerpos
    private readonly string pathEscudos = "Skins_Huscarle/Escudos/"; // Ruta de los escudos
    private readonly string pathPelos = "Skins_Huscarle/Pelos/"; // Ruta de los pelos

    private string archivoCharacter = "Assets/Scripts/Game_utility/JSON/characterSkin.json"; // Archivo de configuración de la skin
    private string archivoPlayer = "Assets/Scripts/Game_utility/JSON/playerData.json"; // Archivo de configuración del jugador

    private string playerName; // Nombre del jugador
    public string Name { get => playerName; set => playerName = value; } // Propiedad que almacena el nombre del jugador

    // Propiedades que almacenan las selecciones actuales de las skins
    public string ArmaSelect { get; set; } = "Espada_de_Hierro";
    public string BarbaSelect { get; set; } = "Huscarle_Barba1";
    public string CuerpoSelect { get; set; } = "Huscarle_Nude";
    public string EscudoSelect { get; set; } = "Huscarle_EscudoMadera";
    public string PeloSelect { get; set; } = "Huscarle_Pelo1";

    /// <summary>
    /// Guarda el nombre del jugador a partir de un campo de texto.
    /// </summary>
    /// <param name="textMeshPro">El campo de texto que contiene el nombre.</param>
    public void SaveName(TMP_InputField textMeshPro)
    {
        if (textMeshPro != null) // Verifica que el componente TextMeshPro existe
        {
            Name = textMeshPro.text; // Extrae el texto y lo asigna a la variable name
        }
    }

    /// <summary>
    /// Guarda la configuración de la skin en archivos JSON.
    /// </summary>
    public void GuardarSkin()
    {
        // Crea una instancia de CharacterSkin con las rutas seleccionadas
        CharacterSkin skin = new CharacterSkin()
        {
            Arma = pathArma + ArmaSelect,
            Barbas = pathBarbas + BarbaSelect,
            Cuerpo = pathCuerpo + CuerpoSelect,
            Escudos = pathEscudos + EscudoSelect,
            Pelos = pathPelos + PeloSelect
        };

        try
        {
            // Guarda el nombre del jugador en el archivo JSON de jugador
            EscribirDatoJSON<PlayerData, string>(archivoPlayer, datos => datos.Name, (datos, valor) => datos.Name = valor, Name);

            // Guarda la configuración de la skin en el archivo JSON de personaje
            GuardarJSON<CharacterSkin>(archivoCharacter, skin);
        }
        catch (Exception e)
        {
            // En caso de error, muestra el mensaje de la excepción en el log de depuración
            Debug.LogError($"Error al guardar los datos: {e.Message}");
        }
    }
    //TODO add exception when saving skin
}
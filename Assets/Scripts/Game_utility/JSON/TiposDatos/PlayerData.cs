using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase serializable que representa los datos de un jugador, específicamente su nombre.
/// </summary>
[System.Serializable]
public class PlayerData 
{
    /// <summary>
    /// Obtiene o establece el nombre del jugador.
    /// </summary>
    public string Name;
    /// <summary>
    /// Obtiene o establece el valor del jugador.
    /// </summary>
    public string Value;

    // TODO: Considerar agregar más propiedades relacionadas con el jugador, como nivel o puntuación.
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase serializable que representa las diferentes partes del skin de un personaje.
/// </summary>
[System.Serializable]
public class CharacterSkin 
{
    /// <summary>
    /// Obtiene o establece el nombre del arma.
    /// </summary>
    public string Arma;

    /// <summary>
    /// Obtiene o establece el nombre de la barba.
    /// </summary>
    public string Barbas;

    /// <summary>
    /// Obtiene o establece el tipo de cuerpo.
    /// </summary>
    public string Cuerpo;
    /// <summary>
    /// Obtiene o establece el nombre del escudo.
    /// </summary>
    public string Escudos;

    /// <summary>
    /// Obtiene o establece el tipo de pelo.
    /// </summary>
    public string Pelos;
    // TODO: Implementar validación para asegurarse de que todos los campos tengan valores válidos.
}

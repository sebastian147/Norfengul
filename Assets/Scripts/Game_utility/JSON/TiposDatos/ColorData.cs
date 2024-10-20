using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase serializable que representa los datos de un color en formato RGBA y su nombre.
/// </summary>
[System.Serializable]
public class ColorData
{
    // Getters y setters para los componentes de color y el nombre.
    
    /// <summary>
    /// Obtiene o establece el valor del componente rojo.
    /// </summary>
    public float R;

    /// <summary>
    /// Obtiene o establece el valor del componente verde.
    /// </summary>
    public float G;

    /// <summary>
    /// Obtiene o establece el valor del componente azul.
    /// </summary>
    public float B;

    /// <summary>
    /// Obtiene o establece el valor del componente alfa (transparencia).
    /// </summary>
    public float A;

    /// <summary>
    /// Obtiene o establece el nombre del color.
    /// </summary>
    public string Name;
    // TODO: Agregar validación para los nombres de colores repetidos en una lista de colores.
}
/// <summary>
/// Clase serializable que representa una lista de objetos ColorData.
/// </summary>
[System.Serializable]
public class ColorList
{
    /// <summary>
    /// Obtiene o establece la lista de colores.
    /// </summary>
    [SerializeField]
    public List<ColorData> Colors = new List<ColorData>();

    /// <summary>
    /// Agrega un nuevo color a la lista de colores.
    /// </summary>
    /// <param name="color">El objeto ColorData a agregar.</param>
    public void AddColor(ColorData color)
    {
        Colors.Add(color);
    }

    /// <summary>
    /// Elimina un color de la lista de colores.
    /// </summary>
    /// <param name="color">El objeto ColorData a eliminar.</param>
    public void RemoveColor(ColorData color)
    {
        Colors.Remove(color);
    }

    // TODO: Implementar un método para buscar un color por su nombre en la lista.
}
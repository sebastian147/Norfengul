using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase serializable que representa los datos de un color en formato RGBA y su nombre.
/// </summary>
[System.Serializable]
public class ColorData
{
    // Componentes de color (rojo, verde, azul, alfa) y el nombre del color.
    private float r;     // Componente rojo.
    private float g;     // Componente verde.
    private float b;     // Componente azul.
    private float a;     // Componente alfa (transparencia).
    private string name; // Nombre del color.

    // Getters y setters para los componentes de color y el nombre.
    
    /// <summary>
    /// Obtiene o establece el valor del componente rojo.
    /// </summary>
    public float R
    {
        get { return r; }
        set { r = Mathf.Clamp01(value); } // Limita el valor entre 0 y 1.
    }

    /// <summary>
    /// Obtiene o establece el valor del componente verde.
    /// </summary>
    public float G
    {
        get { return g; }
        set { g = Mathf.Clamp01(value); } // Limita el valor entre 0 y 1.
    }

    /// <summary>
    /// Obtiene o establece el valor del componente azul.
    /// </summary>
    public float B
    {
        get { return b; }
        set { b = Mathf.Clamp01(value); } // Limita el valor entre 0 y 1.
    }

    /// <summary>
    /// Obtiene o establece el valor del componente alfa (transparencia).
    /// </summary>
    public float A
    {
        get { return a; }
        set { a = Mathf.Clamp01(value); } // Limita el valor entre 0 y 1.
    }

    /// <summary>
    /// Obtiene o establece el nombre del color.
    /// </summary>
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    /// <summary>
    /// Constructor que inicializa los componentes de color y el nombre.
    /// </summary>
    /// <param name="r">Componente rojo.</param>
    /// <param name="g">Componente verde.</param>
    /// <param name="b">Componente azul.</param>
    /// <param name="a">Componente alfa (transparencia).</param>
    /// <param name="name">Nombre del color.</param>
    public ColorData(float r, float g, float b, float a, string name)
    {
        R = r;
        G = g;
        B = b;
        A = a;
        Name = name;
    }

    // TODO: Agregar validación para los nombres de colores repetidos en una lista de colores.
}
/// <summary>
/// Clase serializable que representa una lista de objetos ColorData.
/// </summary>
[System.Serializable]
public class ColorList
{
    // Lista de objetos ColorData.
    [SerializeField]
    private List<ColorData> colors = new List<ColorData>();

    /// <summary>
    /// Obtiene o establece la lista de colores.
    /// </summary>
    public List<ColorData> Colors
    {
        get { return colors; }
        set { colors = value; }
    }

    /// <summary>
    /// Agrega un nuevo color a la lista de colores.
    /// </summary>
    /// <param name="color">El objeto ColorData a agregar.</param>
    public void AddColor(ColorData color)
    {
        colors.Add(color);
    }

    /// <summary>
    /// Elimina un color de la lista de colores.
    /// </summary>
    /// <param name="color">El objeto ColorData a eliminar.</param>
    public void RemoveColor(ColorData color)
    {
        colors.Remove(color);
    }

    // TODO: Implementar un método para buscar un color por su nombre en la lista.
}
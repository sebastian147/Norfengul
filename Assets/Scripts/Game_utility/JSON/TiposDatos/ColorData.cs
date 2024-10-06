using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serializable class representing RGBA color data and a color name.
[System.Serializable]
public class ColorData
{
    public float r;     // Red component.
    public float g;     // Green component.
    public float b;     // Blue component.
    public float a;     // Alpha (transparency) component.
    public string name; // Color name.
}

// Serializable class representing a list of ColorData objects.
[System.Serializable]
public class ColorList
{
    public List<ColorData> colors; // List of ColorData objects.
}

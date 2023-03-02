using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class ColorList
{
    public List<ColorData> colors;
}
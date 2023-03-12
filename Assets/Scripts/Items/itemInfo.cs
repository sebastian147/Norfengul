using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemInfo : ScriptableObject
{
        [SerializeField]public int ID;
        [SerializeField]public string NombreDelItem;
        [SerializeField]public Sprite Sprite;
        [SerializeField]public string Animation;
        [SerializeField]public string Sound;
        [SerializeField]public int Value;

}

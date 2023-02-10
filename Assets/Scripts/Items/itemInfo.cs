using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemInfo : ScriptableObject
{
        [SerializeField]private int ID;
        [SerializeField]private string NombreDelItem;
        [SerializeField]private Sprite Sprite;
        [SerializeField]private string Animation;
        [SerializeField]private string Sound;
        [SerializeField]private int Value;

}

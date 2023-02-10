using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum weaponType{
    sword,
    spear,
    axe
}
[CreateAssetMenu(fileName = "Arma", menuName = "Arma", order = 1)]

public class Armas : itemInfo
{
        [SerializeField]public int damage;
        [SerializeField]public weaponType weaponType;
        [SerializeField]public int knockback;
        [SerializeField]public float damageArea;
        [SerializeField]public int effect;
        [SerializeField]public string category;
        [SerializeField]public string label;
        public UnityEngine.U2D.Animation.SpriteLibraryAsset library;
        public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;

 

}

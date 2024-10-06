using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Sword, Spear, Axe }

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapon", order = 1)]
public class Weapon : Items
{
    [SerializeField] public int damage;
    [SerializeField] public WeaponType weaponType;
    [SerializeField] public int knockback;
    [SerializeField] public float damageArea;
    [SerializeField] public int effect;
    [SerializeField] public string category;
    [SerializeField] public string label;
    [SerializeField] public UnityEngine.U2D.Animation.SpriteLibraryAsset library;
    [SerializeField] public Rect HitBox; // Defining weapon hitbox area
}

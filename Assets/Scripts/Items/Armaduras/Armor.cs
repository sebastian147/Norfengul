using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType {Light, Heavy}

[CreateAssetMenu(fileName = "NewArmor", menuName = "Items/Armor", order = 1)]
public class Armor : Items
{
    public ArmorType armorType;
    public int damageAbsorb;
    public int magicResistense;
    public UnityEngine.U2D.Animation.SpriteLibraryAsset library;
}

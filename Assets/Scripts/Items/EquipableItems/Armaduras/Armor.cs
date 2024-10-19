using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType { Light, Heavy, Medium, Magical }

[CreateAssetMenu(fileName = "NewArmor", menuName = "Items/Armor", order = 1)]
public class Armor : Items
{
    [SerializeField] public ArmorType armorType;
    [SerializeField] public int magicalArmor;
    [SerializeField] public int fisicalArmor;
    [SerializeField] public int durability;
    [SerializeField] public float weight;
}
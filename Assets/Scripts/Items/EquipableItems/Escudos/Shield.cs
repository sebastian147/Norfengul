using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShield", menuName = "Items/Shield", order = 1)]
public class Shield : Items
{
    [SerializeField]public int blocksToBreak; // Determines durability of the shield before breaking
    [SerializeField]public float cooldown; // Cooldown period for shield blocking
}
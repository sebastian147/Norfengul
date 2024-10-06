using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShield", menuName = "Items/Shield", order = 1)]
public class Shield : Items
{
    public int blocksToBreak; // Determines durability of the shield before breaking
    public float cooldown; // Cooldown period for shield blocking
    public UnityEngine.U2D.Animation.SpriteLibraryAsset library;
}
/* todo
*   check if library not null
*/
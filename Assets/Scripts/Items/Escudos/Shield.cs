using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShield", menuName = "Items/Shield", order = 1)]
public class Shield : Items
{
    public int blocksToBreak;
    public float cooldown;
    public UnityEngine.U2D.Animation.SpriteLibraryAsset library;
}

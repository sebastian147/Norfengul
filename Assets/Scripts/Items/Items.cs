using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
public class Items : ScriptableObject
{
    public int ID;
    public string itemName;
    public Sprite itemSprite;
    [TextArea] public string itemDescription;
    public int itemValue;
}

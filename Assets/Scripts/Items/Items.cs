using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
public class Items : ScriptableObject
{
    [SerializeField]public int ID;
    [SerializeField]public string itemName;
    [SerializeField]public Sprite itemSprite;
    [TextArea] public string itemDescription;
    [SerializeField]public int itemValue;
    [SerializeField]public UnityEngine.U2D.Animation.SpriteLibraryAsset library;
    [SerializeField]public string sound; //temporary TODO
}

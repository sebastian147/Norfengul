using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static JsonFunctions;


public class ArmorSlot : InventorySlot
{
    private Armor armorToPass;
    public UnityEngine.U2D.Animation.SpriteLibrary mySpriteLibrary;
    public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;
    
    private void Update ()
    {
        if (itemIn == null)
        {
            imageItem.gameObject.SetActive(false);
            imagePhantom.gameObject.SetActive(false);
        }
        else
        {
            DisplayItemSprite();
        }
        armorToPass = itemIn as Armor;
        ArmorChanger();
    }
    public override void ReciveItemSlot(InventorySlot slotToPass)
    {
        if (slotToPass.itemIn is Armor || slotToPass.itemIn == null)
        {
            Items itemInPass = slotToPass.itemIn;
            slotToPass.PassItemSlot(itemIn);
            itemIn = itemInPass;
        }
    }

    public override void PassItemSlot(Items itemToRecive)
    {
        if (itemToRecive is Armor || itemToRecive == null)
        {
            itemIn = itemToRecive;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("No hay menu desplegable en este slot");
    }


    public void ArmorChanger()
    {
        if (armorToPass != null)
        {
            mySpriteLibrary.spriteLibraryAsset = armorToPass.library;
            mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }
        else
        {
            mySpriteLibrary.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>("Skins_Huscarle/Cuerpo/Huscarle_Nude");
            mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }
    }
}



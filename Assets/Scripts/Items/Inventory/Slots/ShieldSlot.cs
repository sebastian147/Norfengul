using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static JsonFunctions;


public class ShieldSlot : InventorySlot
{
    private Shield shieldToPass;
    public MeleShieldLogic ShieldLogic;
    
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
        shieldToPass = itemIn as Shield;
        ShieldChanger();
    }

    public override void ReciveItemSlot(InventorySlot slotToPass)
    {
        if (slotToPass.itemIn is Shield || slotToPass.itemIn == null)
        {
            Items itemInPass = slotToPass.itemIn;
            slotToPass.PassItemSlot(itemIn);
            itemIn = itemInPass;
        }
    }

    public override void PassItemSlot(Items itemToRecive)
    {
        if (itemToRecive is Shield || itemToRecive == null)
        {
            itemIn = itemToRecive;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("No hay menu desplegable en este slot");
    }

    public void ShieldChanger()
    {
        ShieldLogic.shield = shieldToPass;
    }

}

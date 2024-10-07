using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static JsonFunctions;
using TMPro;


public class WeaponSlot : InventorySlot
{
    private Weapon weaponToPass;
    public MeleWeaponLogic WeaponLogic;

    private void Update ()
    {
        base.Update();
        weaponToPass = itemIn as Weapon;
        WeaponChanger();
    }

    public override void ReciveItemSlot(InventorySlot slotToPass)
    {
        if (slotToPass.itemIn is Weapon || slotToPass.itemIn == null)
        {
            Items itemInPass = slotToPass.itemIn;
            slotToPass.PassItemSlot(itemIn);
            itemIn = itemInPass;
        }
    }

    public override void PassItemSlot(Items itemToRecive)
    {
        if (itemToRecive is Weapon || itemToRecive == null)
        {
            base.PassItemSlot(itemToRecive);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("No hay menu desplegable en este slot");
    }
    
    public void WeaponChanger()
    {
        WeaponLogic.Armas = weaponToPass;
    }
}

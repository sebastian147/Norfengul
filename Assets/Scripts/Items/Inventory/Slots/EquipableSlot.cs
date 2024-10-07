using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static JsonFunctions;


public class EquipableSlot<T> : InventorySlot where T : Items
{

    private T iToPass;    
    private void Update ()
    {
        base.Update();
        iToPass = itemIn as T;
        Changer(iToPass);
    }

    public override void ReciveItemSlot(InventorySlot slotToPass)
    {
        if (slotToPass.itemIn is T || slotToPass.itemIn == null)
        {
            Items itemInPass = slotToPass.itemIn;
            slotToPass.PassItemSlot(itemIn);
            itemIn = itemInPass;
        }
    }

    public override void PassItemSlot(Items itemToRecive)
    {
        
        base.PassItemSlot(itemToRecive);
        
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("No hay menu desplegable en este slot");
    }

    public virtual void Changer(T iToPass){}

}



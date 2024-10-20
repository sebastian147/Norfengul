using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static JsonFunctions;


public class EquipableSlot<T> : InventorySlot where T : Items
{

    protected T _iToPass;
    public virtual T iToPass
    {
        get { return _iToPass; }
        set { if(_iToPass != value)
                {
                    _iToPass = value; 
                    Changer(_iToPass as T);

                }
            }
    }
    public override void Update ()
    {
        base.Update();
        iToPass  = itemIn as T; 
    }

    public override void ReciveItemSlot(InventorySlot slotToPass)
    {
        if (slotToPass.itemIn is T || slotToPass.itemIn == null)
        {
            Items _iToPass = slotToPass.itemIn;
            slotToPass.PassItemSlot(itemIn);
            itemIn = _iToPass;
        }
    }

    public override void PassItemSlot(Items itemToRecive)
    {
        if(itemToRecive is T || itemToRecive == null)
        {
            base.PassItemSlot(itemToRecive);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("No hay menu desplegable en este slot");
    }

    public virtual void Changer(T iToPass){
        Debug.Log("No hay nada que cambiar");
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeaponLogic : EquipableItemsLogic<Weapon>
{
    [HideInInspector]public Weapon Armas
    {
        get { return base.EquipableItem; }
        set { base.EquipableItem = value; } 
    }
}
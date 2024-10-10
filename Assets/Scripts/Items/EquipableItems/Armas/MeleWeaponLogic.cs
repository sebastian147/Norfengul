using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeaponLogic : EquipableItemsLogic<Weapon>
{
    public Weapon Armas
    {
        get { return base.EquipableItem; }
        set { base.EquipableItem = value; } 
    }
}
/* to do
* dont call ChangeWeapon() in update or add a conditional
* check if this is waponr or equipmet change ask Pablo
*/
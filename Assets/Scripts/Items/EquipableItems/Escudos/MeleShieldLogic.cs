using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleShieldLogic : EquipableItemsLogic<Shield>
{
    public Shield shield
    {
        get { return base.EquipableItem; }
        set { base.EquipableItem = value; } 
    }
}
/* to do
*  dont call ChangeShield() in update or add a conditional
*  dont check if this is waponr or equipmet change ask Pablo
*/
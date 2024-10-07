using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static JsonFunctions;


public class ShieldSlot : EquipableSlot<Shield>
{
    public MeleShieldLogic ShieldLogic;

    public override void Changer(Shield iToPass)
    {
        ShieldLogic.shield = iToPass;
    }

}

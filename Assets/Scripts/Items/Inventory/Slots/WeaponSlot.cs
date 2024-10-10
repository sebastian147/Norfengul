using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static JsonFunctions;
using TMPro;


public class WeaponSlot : EquipableSlot<Weapon>
{
    public MeleWeaponLogic WeaponLogic;

    public override void Changer(Weapon iToPass)
    {
        WeaponLogic.Armas = iToPass;
    }
}

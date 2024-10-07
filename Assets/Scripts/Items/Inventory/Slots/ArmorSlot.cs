using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static JsonFunctions;


public class ArmorSlot : EquipableSlot<Armor>
{
    public UnityEngine.U2D.Animation.SpriteLibrary mySpriteLibrary;
    public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;
    public override void Changer(Armor iToPass)
    {
        if (iToPass != null)
        {
            mySpriteLibrary.spriteLibraryAsset = iToPass.library;
            mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }
        else
        {
            mySpriteLibrary.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>("Skins_Huscarle/Cuerpo/Huscarle_Nude");
            mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItemsLogic<t> : MonoBehaviour where t : Items
{
    protected t equipableItem;
    public virtual t EquipableItem
    {
        get { return equipableItem; }
        set { equipableItem = value; 
              ChangeEquip();
            }
    }
    public SpriteRenderer mySpriteRenderer;
    public UnityEngine.U2D.Animation.SpriteLibrary mySpriteLibrary;
    public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;


    // Changes the weapon sprite based on the current Weapon object assigned
    public void ChangeEquip()
    {
        if (equipableItem != null)
        {

            if (mySpriteLibrary == null || mySpriteRenderer == null || mySpriteResolver == null) {
                Debug.LogError("Missing components in Logic");
                return;
            }
            mySpriteLibrary.spriteLibraryAsset = equipableItem.library;
            mySpriteResolver.ResolveSpriteToSpriteRenderer();
            mySpriteRenderer.enabled = true;
        }
        else
        {
            // Disables sprite renderer when no weapon is assigned
            mySpriteRenderer.enabled = false;
        }
    }
        /*void Update()
        {
                mySpriteResolver.SetCategoryAndLabel(Armas.category, Armas.label);
                mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }*/
}
/* to do
* dont call ChangeWeapon() in update or add a conditional
* check if this is waponr or equipmet change ask Pablo
*/
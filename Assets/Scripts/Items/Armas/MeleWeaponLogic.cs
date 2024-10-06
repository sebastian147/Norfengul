using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponLogic : MonoBehaviour
{
    public Weapon Armas;
    public SpriteRenderer mySpriteRenderer;
    public UnityEngine.U2D.Animation.SpriteLibrary mySpriteLibrary;
    public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;

    // Updates the weapon each frame, should consider moving to an event-based system if performance issues arise
    public void Update()
    {
        ChangeWeapon();
    }

    // Changes the weapon sprite based on the current Weapon object assigned
    void ChangeWeapon()
    {
        if (Armas != null)
        {
            if (mySpriteLibrary == null || mySpriteRenderer == null || mySpriteResolver == null) {
                Debug.LogError("Missing components in MeleeWeaponLogic");
                return;
            }
            mySpriteLibrary.spriteLibraryAsset = Armas.library;
            mySpriteResolver.ResolveSpriteToSpriteRenderer();
            mySpriteRenderer.enabled = true;
        }
        else
        {
            // Disables sprite renderer when no weapon is assigned
            mySpriteRenderer.enabled = false;
        }
    }
}
/* to do
* dont call ChangeWeapon() in update or add a conditional
* check if this is waponr or equipmet change ask Pablo
*/
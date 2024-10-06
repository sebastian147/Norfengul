using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleShieldLogic : MonoBehaviour
{
    public Shield shield;
    public SpriteRenderer mySpriteRenderer;
    public UnityEngine.U2D.Animation.SpriteLibrary mySpriteLibrary;
    public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;

    // Updates the shield each frame, should consider event-based updates for efficiency
    public void Update()
    {
        ChangeShield();
    }

    // Changes the shield sprite based on the current Shield object assigned
    void ChangeShield()
    {
        if (shield != null)
        {
            if (mySpriteLibrary == null || mySpriteRenderer == null || mySpriteResolver == null) {
                Debug.LogError("Missing components in MeleeWeaponLogic");
                return;
            }
            mySpriteLibrary.spriteLibraryAsset = shield.library;
            mySpriteResolver.ResolveSpriteToSpriteRenderer();
            mySpriteRenderer.enabled = true;
        }
        else
        {
            // Disables sprite renderer when no shield is assigned
            mySpriteRenderer.enabled = false;
        }
    }
}
/* to do
*  dont call ChangeShield() in update or add a conditional
*  dont check if this is waponr or equipmet change ask Pablo
*/
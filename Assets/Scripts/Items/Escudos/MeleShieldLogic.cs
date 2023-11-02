using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleShieldLogic : MonoBehaviour
{
    public Shield shield;
    public SpriteRenderer mySpriteRenderer;
    public UnityEngine.U2D.Animation.SpriteLibrary mySpriteLibrary;
    public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;

    public void Update()
    {
        ChangeShield();
    }

    void ChangeShield()
    {
        if(shield != null)
        {
            mySpriteLibrary.spriteLibraryAsset = shield.library;
            mySpriteResolver.ResolveSpriteToSpriteRenderer();
            mySpriteRenderer.enabled = true;
        }
        else
        {
            mySpriteRenderer.enabled = false;
        }
            
    }
}

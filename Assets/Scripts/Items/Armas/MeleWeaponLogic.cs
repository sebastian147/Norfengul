using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeaponLogic : MonoBehaviour
{
        public Weapon Armas;                   
        public SpriteRenderer mySpriteRenderer;
        public UnityEngine.U2D.Animation.SpriteLibrary mySpriteLibrary;
        public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;

        public void Update()
        {
                ChangeWeapon();
        }

        void ChangeWeapon()
        {
                if(Armas != null)
                {
                        mySpriteLibrary.spriteLibraryAsset = Armas.library;
                        mySpriteResolver.ResolveSpriteToSpriteRenderer();
                        mySpriteRenderer.enabled = true;
                }
                else
                {
                        mySpriteRenderer.enabled = false;
                }
                
        }

        /*void Update()
        {
                mySpriteResolver.SetCategoryAndLabel(Armas.category, Armas.label);
                mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }*/
}

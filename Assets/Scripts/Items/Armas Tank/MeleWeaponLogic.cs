using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeaponLogic : MonoBehaviour
{
        [SerializeField] public Armas Armas;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary mySpriteLibrary;
        public void Update()
        {
                ChangeWeapon();
        }
        void ChangeWeapon()
        {
                mySpriteLibrary.spriteLibraryAsset=Armas.library;
                mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }
        /*void Update()
        {
                mySpriteResolver.SetCategoryAndLabel(Armas.category, Armas.label);
                mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeaponLogic : MonoBehaviour
{
        [SerializeField] public Armas Armas;
        public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;
        

        void Update()
        {
                mySpriteResolver.SetCategoryAndLabel(Armas.category, Armas.label);
                mySpriteResolver.ResolveSpriteToSpriteRenderer();
        }
}

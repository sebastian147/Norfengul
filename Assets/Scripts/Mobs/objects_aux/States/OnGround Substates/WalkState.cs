using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : OnGroundState
{
    public override void animate(Mob myMob)
    {
        if ( horizontalMove > 0)
        {
            myMob.myAnimator.SetBool(WALK_ANIMATION, true);
            myMob.mySpriteRenderer.flipX = false;
        }
        else if (horizontalMove <0)
        {
            myMob.myAnimator.SetBool(WALK_ANIMATION, true);
            myMob.mySpriteRenderer.flipX = true;
        }
    }
}

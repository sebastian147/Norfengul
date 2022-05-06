using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : OnGroundState
{
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool(WALK_ANIMATION, false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MobBaseState
{
    public string WALK_ANIMATION = "Walk";

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
    public override void EndState(Mob myMob)
    {

    }
    public override void StarState(Mob myMob)
    {

    }
    public override void CheckChangeState()
    {

    }
    public override void UpdateState()
    {

    }
    public override void FixedUpdateState()
    {

    }
}

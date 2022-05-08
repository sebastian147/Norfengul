using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MobBaseState
{
    public string WALK_ANIMATION = "Walk";

    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetFloat("Speed", myMob.myRigidbody.velocity.x);
        if ( myMob.myRigidbody.velocity.x > 0)
        {
            myMob.mySpriteRenderer.flipX = false;
        }
        else if (myMob.myRigidbody.velocity.x <0)
        {
            myMob.mySpriteRenderer.flipX = true;
        }
    }
    public override void EndState(Mob myMob)
    {

    }
    public override void StarState(Mob myMob)
    {

    }
    public override void CheckChangeState(Mob myMob)
    {
        if(myMob.myRigidbody.velocity.x<0.01f)
        {
            myMob.actualState = myMob.myStateMachine.changeState(0);
        }
    }
    public override void UpdateState(Mob myMob)
    {
        animate(myMob);
        CheckChangeState(myMob);
    }
    public override void FixedUpdateState(Mob myMob)
    {

    }
}

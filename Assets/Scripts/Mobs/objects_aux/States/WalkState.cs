using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MobBaseState
{
    public string WALK_ANIMATION = "Walk";

    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetFloat("Speed", Mathf.Abs(myMob.myRigidbody.velocity.x));
    }
    public override void EndState(Mob myMob)
    {

    }
    public override void StarState(Mob myMob)
    {
        animate(myMob);
    }
    public override void CheckChangeState(Mob myMob)
    {
        if(myMob.attacking == true)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Attack,myMob);
            return;
        }
        if(myMob.jumpBufferCounter>0 || !myMob.m_Grounded)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump,myMob);
            return;
        }
        if(Mathf.Abs(myMob.myRigidbody.velocity.x) < 1)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle,myMob);
            return;
        }
        if((myMob.dashRight || myMob.dashLeft) && myMob.canDash)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Dash,myMob);
            return;
        }
    }
    public override void UpdateState(Mob myMob)
    {
        CheckChangeState(myMob);
    }
    public override void FixedUpdateState(Mob myMob)
    {
		Vector3 targetVelocity = new Vector2(myMob.horizontalMove * 10f/*apexModifierCurrent*/, myMob.myRigidbody.velocity.y);
		// And then smoothing it out and applying it to the character
		myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : MobBaseState
{
    private float timer;
    public override void animate(Mob myMob)
    {
		myMob.myAnimator.SetBool("isInWall", true);
        return;
    }
    public override void EndState(Mob myMob)
    {
		myMob.myAnimator.SetBool("isInWall", false);
		myMob.wallGrabing = true;
    }
    public override void StarState(Mob myMob)
    {
        timer = 0f;
        myMob.wallGrabingDirection = myMob.horizontalMove;
    }
    public override void CheckChangeState(Mob myMob)
    {
        if(myMob.jumpBufferCounter>0 && myMob.horizontalMove !=  0)
        {
            myMob.wallGrabing = true;
            myMob.jumpsends = myMob.amountOfJumps-1;
		    myMob.jumpdones = myMob.amountOfJumps-1;
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump,myMob);
            return;
        }
        if(timer > myMob.timeInwallBuffer)
        {
            myMob.jumpsends = myMob.amountOfJumps;
		    myMob.jumpdones = myMob.amountOfJumps;
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump,myMob);
            return;
        }
        if(!myMob._inWallRight && !myMob._inWallLeft && myMob.horizontalMove ==  myMob.wallGrabingDirection)
        {
            myMob.wallGrabing = true;
            myMob.jumpsends = myMob.amountOfJumps;
		    myMob.jumpdones = myMob.amountOfJumps;
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump,myMob);
            return;
        }
        if(myMob.m_Grounded)
        {

            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle,myMob);
			return;
        }
    }
    public override void UpdateState(Mob myMob)
    {
        animate(myMob);
        if(myMob.horizontalMove !=  myMob.wallGrabingDirection)
        {
            timer += Time.fixedDeltaTime;
        }
        CheckChangeState(myMob);//revisar
    }
    public override void FixedUpdateState(Mob myMob)
    {
        myMob.myRigidbody.velocity = new Vector2(myMob.myRigidbody.velocity.x,Mathf.Clamp(myMob.myRigidbody.velocity.y, Vector2.down.y*myMob.wallSlidingSpeed, float.MaxValue));//mover a fixed update
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MobBaseState
{
        public override void animate(Mob myMob)
        {
                myMob.myAnimator.SetBool("idle", true);
        }
        public override void EndState(Mob myMob)
        {
                myMob.myAnimator.SetBool("idle", false);
        }
        public override void StarState(Mob myMob)
        {
                animate(myMob);
        }
        public override void CheckChangeState(Mob myMob)
        {
                if((myMob.dashRight || myMob.dashLeft) && myMob.canDash)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Dash,myMob);
                        return;
                }
                if(myMob.attacking == true)
                {
                myMob.actualState = myMob.myStateMachine.changeState(myStates.Attack,myMob);
                return;
                }
                if(Mathf.Abs(myMob.horizontalMove) != 0)
                {
                myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk,myMob);
                return;
                }
                if(myMob.jumpBufferCounter>0 || !myMob.m_Grounded)
                {
                myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump,myMob);
                return;
                }
                if(myMob.victory)
                {
                myMob.actualState = myMob.myStateMachine.changeState(myStates.Victory,myMob);
                return;
                }
        }
        public override void UpdateState(Mob myMob)
        {
                base.UpdateState(myMob);
                CheckChangeState(myMob);
        }
        public override void FixedUpdateState(Mob myMob)
        {
		Vector3 targetVelocity = new Vector2(myMob.horizontalMove * myMob.moveSpeed/*apexModifierCurrent*/, myMob.myRigidbody.velocity.y);
		// And then smoothing it out and applying it to the character
		myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing); 
        }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MobBaseState
{
        float time;
        float originalGravity;
        public override void animate(Mob myMob)
        {
                //myMob.myAnimator.SetFloat("Speed", 0);
        }
        public override void EndState(Mob myMob)
        {
                myMob.myRigidbody.gravityScale = originalGravity;
                myMob.tr.emitting = false;
                myMob.canDash = false;
        }
        public override void StarState(Mob myMob)
        {
                int direction = 1;
                originalGravity = myMob.myRigidbody.gravityScale;
                myMob.myRigidbody.gravityScale = 0f;
                if(myMob.dashRight)
                {
                        direction = 1;
                }
                else if(myMob.dashLeft)
                {
                        direction = -1;
                }
                else
                {
                        Debug.Log("error");
                }
                myMob.myRigidbody.velocity = new Vector2(direction * myMob.dashingPower, 0f);
                myMob.tr.emitting = true;
                time = myMob.dashingTime;
                Fliping(myMob);
        }
        public override void CheckChangeState(Mob myMob)
        {
                if(time <= 0 && myMob.m_Grounded)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk,myMob);
                        return;
                }
                if(time <= 0 && !myMob.m_Grounded)
                {
                        myMob.jumpsends = myMob.amountOfJumps;
                        myMob.jumpdones = myMob.amountOfJumps;
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Jump,myMob);
                        return;
                }
        }
        public override void UpdateState(Mob myMob)
        {
                animate(myMob);
                CheckChangeState(myMob);

        }
        public override void FixedUpdateState(Mob myMob)
        {
                time -= Time.fixedDeltaTime;
        }
        public override void Fliping(Mob myMob)
        {
                if(myMob.m_FacingRight && myMob.dashLeft)
                {
                        Flip(myMob);
                }
                else if(!myMob.m_FacingRight && myMob.dashRight)
                {
                        Flip(myMob);
                }
        }
        
}

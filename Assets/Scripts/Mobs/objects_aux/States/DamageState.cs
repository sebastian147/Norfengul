using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : MobBaseState
{

        public override void animate(Mob myMob)
        {
                myMob.myAnimator.SetBool("isHit", true);
        }
        public override void EndState(Mob myMob)
        {
                myMob.myAnimator.SetBool("isHit", false);
        }
        public override void StarState(Mob myMob)
        {
                //myMob.gameObject.layer = LayerMask.NameToLayer("Player");

                AnimatorControllerParameter[] parametros = myMob.myAnimator.parameters;


                foreach (AnimatorControllerParameter parametro in parametros)
                {
                        myMob.myAnimator.SetBool(parametro.name, false);
                }
        }
        public override void CheckChangeState(Mob myMob)
        {
                if(myMob.attacking == true)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Attack,myMob);
                        return;
                }
                if(Mathf.Abs(myMob.horizontalMove) != 0 && myMob.running == true)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Running,myMob);
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
                if(Mathf.Abs(myMob.myRigidbody.velocity.x) < 1)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle,myMob);
                        return;
                }
        }
        public override void UpdateState(Mob myMob)
        {

                animate(myMob);
                if( !(myMob.myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) && (myMob.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit") ))
                        CheckChangeState(myMob);

        }

        public override void FixedUpdateState(Mob myMob)
        {
		Vector3 targetVelocity = new Vector2(myMob.horizontalMove * myMob.moveSpeed/*apexModifierCurrent*/, myMob.myRigidbody.velocity.y);
		// And then smoothing it out and applying it to the character
		myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing); 
        }
}

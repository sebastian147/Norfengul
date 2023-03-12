using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : MobBaseState
{
        float diry = 0;
        public override void animate(Mob myMob)
        {
                myMob.myAnimator.SetBool("isHit", true);
        }
        public override void EndState(Mob myMob)
        {
                myMob.myAnimator.SetBool("isHit", false);
                myMob.changeLayer("Player");
        }
        public override void StarState(Mob myMob)
        {
                //myMob.gameObject.layer = LayerMask.NameToLayer("Player");

                AnimatorControllerParameter[] parametros = myMob.myAnimator.parameters;


                foreach (AnimatorControllerParameter parametro in parametros)
                {
                        myMob.myAnimator.SetBool(parametro.name, false);
                }
                myMob.changeLayer("Inmunity");

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
                if(myMob.m_Grounded)
                {
                        diry = 1;
                }
                else
                {
                        diry = 0;
                }//esta hace que si esta en el aire no lo empuje para abajo ni para arriba, puedo calcular la poscicion del personaje que ataco y el que recibio daño y en funcion de eso mandarlo para abajo oa arriba
		Vector3 targetVelocity = new Vector2(myMob.dir * myMob.knockBackTake/*apexModifierCurrent*/, diry*myMob.knockBackTake*2);
		// And then smoothing it out and applying it to the character
		myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing); 
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TakeDamageState : MobBaseState
{
    public override void animate(Mob myMob)
    {
        //myMob.myAnimator.SetFloat("Speed", 0);
        //animation take damage
    }
    public override void EndState(Mob myMob)
    {

    }
    public override void StarState(Mob myMob)
    {
        float knockBack = 200f;
        animate(myMob);
    }
    public override void CheckChangeState(Mob myMob)
    {
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
        if(myMob.attacking == true)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Attack,myMob);
            return;
        }
    }
    public override void UpdateState(Mob myMob)
    {
        CheckChangeState(myMob);
    }
    public override void FixedUpdateState(Mob myMob)
    {

    }


}

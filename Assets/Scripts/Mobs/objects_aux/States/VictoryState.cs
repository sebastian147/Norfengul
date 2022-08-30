using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : MobBaseState
{
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool("IsVictory", true);
    }
    public override void EndState(Mob myMob)
    {
        myMob.myAnimator.SetBool("IsVictory", false);
    }
    public override void StarState(Mob myMob)
    {

    }
    public override void CheckChangeState(Mob myMob)
    {
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
        if(!myMob.victory)
        {
            myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle,myMob);
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

    }
}

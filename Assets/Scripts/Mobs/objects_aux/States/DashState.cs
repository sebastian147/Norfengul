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
        myMob.canDash = true; //se deberia poner en true en otro lado luego de pasado el tiempo
        myMob.tr.emitting = false;

    }
    public override void StarState(Mob myMob)
    {
        myMob.canDash = false;
        originalGravity = myMob.myRigidbody.gravityScale;
        myMob.myRigidbody.gravityScale = 0f;
        myMob.myRigidbody.velocity = new Vector2(Mathf.Sign(myMob.horizontalMove) * myMob.dashingPower, 0f);
        myMob.tr.emitting = true;
        time = myMob.dashingTime;
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
    
}

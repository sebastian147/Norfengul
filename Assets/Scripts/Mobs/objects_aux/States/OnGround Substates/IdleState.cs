using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MobBaseState
{
    public override void animate(Mob myMob)
    {
        myMob.myAnimator.SetBool("IDLE", false);
    }
    public override void EndState(Mob myMob)
    {

    }
    public override void StarState(Mob myMob)
    {

    }
    public override void CheckChangeState()
    {

    }
    public override void UpdateState()
    {

    }
    public override void FixedUpdateState()
    {

    }
}

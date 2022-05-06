using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : OnGroundState
{
    public CrouchState(){
        this.moveSpeed = 10f;
    }


    public override void animate(Mob myMob)
    {
        return;
    }
}

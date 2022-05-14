using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobBaseState
{



    /*public virtual int jump(Mob myMob){

    }

    public virtual int crouch(Mob myMob){

    }

    public virtual int onCollision(Mob myMob, Collision2D collision);*/
    private void UpdateStates() {
        
    }
    

    
    public abstract void animate(Mob  myMob);
    public abstract void EndState(Mob myMob);
    public abstract void StarState(Mob myMob);
    public abstract void CheckChangeState(Mob myMob);
    public abstract void UpdateState(Mob myMob);
    public abstract void FixedUpdateState(Mob myMob);
    public virtual void SwitchState(Mob myMob){

    }

}

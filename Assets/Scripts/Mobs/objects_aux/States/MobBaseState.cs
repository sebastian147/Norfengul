using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobBaseState
{
    protected float horizontalMove = 0f;
    protected float moveSpeed = 40f;


    /*public virtual int jump(Mob myMob){

    }

    public virtual int crouch(Mob myMob){

    }

    public virtual int onCollision(Mob myMob, Collision2D collision);*/
    
    public virtual int move(Mob myMob)
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        myMob.myTransform.position += new Vector3(horizontalMove, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
        return 1;
    }
    
    public abstract void animate(Mob  myMob);
}

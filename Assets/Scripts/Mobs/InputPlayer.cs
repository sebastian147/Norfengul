using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public void InputChecks(Mob myMob)
    {
        JumpCheck(myMob);
        AttackCheck();
        MoveCheck(myMob);
    }

    public void JumpCheck(Mob myMob)//mover logica del tiempo a salto
    {
        if(Input.GetButtonDown("Jump"))
        {
            myMob.jumpBufferCounter = myMob.allowedTimeInAir;
            myMob.jumpStop = false;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            myMob.jumpStop = true;
        }
        else if(myMob.jumpBufferCounter > 0 && myMob.jumpStop == true)
        {
            myMob.jumpBufferCounter -= Time.fixedDeltaTime;
        }

    }
    public void AttackCheck()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //if(Time.time >= nextAttackTime)
            //{
                //Attack();
                //nextAttackTime = Time.time + 1f/attackRate;
            //}
        } 
    }
    public void MoveCheck(Mob myMob)
    {
        myMob.horizontalMove = Input.GetAxisRaw("Horizontal");
    }
}

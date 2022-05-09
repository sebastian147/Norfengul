using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public void InputChecks(Mob myMob)
    {
        JumpCheck();
        AttackCheck();
        MoveCheck(myMob);
    }

    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter = 0f;
    public void JumpCheck()//mover logica del tiempo a salto
    {
        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            //jumpStop = false;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            //jumpStop = true;
        }
        else
        {
            jumpBufferCounter -= Time.fixedDeltaTime;
        }
        if(jumpBufferCounter > 0)
        {
            //jumpBufferCounter = Jump(jumpBufferCounter);
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

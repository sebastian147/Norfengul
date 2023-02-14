using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    float DoubleTapTimeL = 0f;
    float DoubleTapTimeMaxL = 2f;
    int DoubleTapCounterL = 0;
    float DoubleTapTimeR = 0f;
    float DoubleTapTimeMaxR = 2f;
    int DoubleTapCounterR = 0;

    public void InputChecks(Mob myMob)
    {
        JumpCheck(myMob);
        AttackCheck(myMob);
        MoveCheck(myMob);
        myMob.victory=ChangeBoolStateInput("v");
        myMob.dashLeft=ChangeBoolStateInput("q");
        myMob.dashRight=ChangeBoolStateInput("e");
        myMob.running=IsRuning(myMob);        
        DropCheck(myMob);
        SlotOneCheck(myMob);
        SlotTwoCheck(myMob);
        //myMob.dashLeft = DoubleTap(myMob, "a", ref DoubleTapTimeL, DoubleTapTimeMaxL, ref DoubleTapCounterL);
        //myMob.dashRight = DoubleTap(myMob, "d", ref DoubleTapTimeR, DoubleTapTimeMaxR, ref DoubleTapCounterR);
    }
    public bool ChangeBoolStateInput(string key)
    {
        if(Input.GetButton(key))
        {
            return true;
        }
        else{
            return false;
        }
    }
    public bool IsRuning(Mob myMob)
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            return true;
        }
        else{
            return false;
        }
    }
    public void JumpCheck(Mob myMob)//mover logica del tiempo a salto
    {
        if(Input.GetButtonDown("Jump"))
        {
            myMob.jumpBufferCounter = myMob.jumpBufferTime;
            myMob.jumpStop = false;
            myMob.jumping = true;
            if(myMob.jumpdones == 0)
            {
                myMob.jumpsends = 0;//esto es para que salte despues de golpear yno se bugue cuando cae haciendo un salto simple con el buffer
            }
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
    public void AttackCheck(Mob myMob)
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(Time.time >= myMob.nextAttackTime)
            {
                
                myMob.nextAttackTime = Time.time + 1f/myMob.attackRate;
                myMob.attacking = true;
            }
        } 
    }
    public void VictoryCheck(Mob myMob)
    {

    }
    public void SlotOneCheck(Mob myMob)
    {
        if(Input.GetButton("1"))
        {
            myMob.changeWeapon("Armas_Huscarle/Arma");
        }
    }
    public void SlotTwoCheck(Mob myMob)
    {
        if(Input.GetButton("2"))
        {
            myMob.changeWeapon("Armas_Huscarle/Arma1");
        }
    }
    public void MoveCheck(Mob myMob)
    {
        myMob.horizontalMove = Input.GetAxisRaw("Horizontal");
    }
    public void DropCheck(Mob myMob)
    {
        if(Input.GetAxisRaw("Vertical")<0)
        {
            myMob.drop = true;
        }
        else{
            myMob.drop = false;
        }
    }
    public bool DoubleTap(Mob myMob, string key, ref float DoubleTapTime, float DoubleTapTimeMax, ref int DoubleTapCounter)
    {
        if(DoubleTapCounter < 2 && Input.GetButtonDown(key))
        {
            DoubleTapCounter++;
            if(DoubleTapCounter == 1)
            {
                DoubleTapTime = DoubleTapTimeMax;
            }
        }
        else
        {
            DoubleTapTime -= Time.fixedDeltaTime;
        }
        if(DoubleTapTime <= 0)
        {
            DoubleTapCounter = 0;
        }
        if(DoubleTapCounter >= 2)
        {
            DoubleTapTime = DoubleTapTimeMax;
            DoubleTapCounter = 0;
            return true;
        }
        return false;
    }

}

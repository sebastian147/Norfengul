using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : mob
{
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter = 0f;

    public override void Awake()
    {
        base.Awake();
    }
    
    public override void Star()
    {
        if(!Pv.IsMine)
            return;

    }
    public override void Update()
    {

        if(!Pv.IsMine)
            return;
        base.Update();

        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            jumpStop = false;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            jumpStop = true;
        }
        else
        {
            jumpBufferCounter -= Time.fixedDeltaTime;
        }
        if(jumpBufferCounter > 0)
        {
            jumpBufferCounter = Jump(jumpBufferCounter);
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            if(Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f/attackRate;
            }
        } 
    }
    public override void FixedUpdate() 
    {
        if(!Pv.IsMine)
            return;
        base.FixedUpdate();
    }

}

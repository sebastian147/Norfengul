using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : mob
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {

        if(!Pv.IsMine)
            return;
        base.Update();

        if(Input.GetButtonDown("Jump"))
        {
            jumpStop = false;
            Jump();
        }
        else if(Input.GetButtonUp("Jump"))
        {
            jumpStop = true;
        }
        if(Input.GetMouseButtonDown(2))
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

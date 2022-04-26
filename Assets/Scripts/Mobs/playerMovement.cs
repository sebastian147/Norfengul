using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : mob
{
    public override void Awake() {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
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
        base.FixedUpdate();
    }
}

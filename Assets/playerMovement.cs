using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : mob
{
    public override void Update()
    {
        base.Update();

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
}

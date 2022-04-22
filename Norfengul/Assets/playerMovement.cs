using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : mob
{
    PhotonView view;
    public override void Start() {
        view = GetComponent<PhotonView>();
    }
    public override void Update()
    {
        if(view.IsMine)
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
    }
    public override void FixedUpdate() {
        if(view.IsMine)
        {
            base.FixedUpdate();
        }
    }
}

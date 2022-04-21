using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : mob
{
    PhotonView view;
    public void Start() {
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
                Attack();
            }
        }
    }
}

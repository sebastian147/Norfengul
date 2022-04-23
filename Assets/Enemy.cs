using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : mob
{
    protected PhotonView pv;

    public override void Start()
    {
		pv = GetComponent<PhotonView>();
    }
    public override void Update()
    {
        if(pv.IsMine)
        {
            base.Update();
        }
    }
    public override void FixedUpdate() {
        if(pv.IsMine)
        {
            base.FixedUpdate();
        }
    }
}

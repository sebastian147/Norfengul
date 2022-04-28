using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : mob
{
    public Queue<KeyCode> inputBuffer;
    public override void Awake()
    {
        base.Awake();
        inputBuffer = new Queue<KeyCode>();
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
            inputBuffer.Enqueue(KeyCode.Space);
            jumpStop = false;
            Invoke("BufferDeQueue",2f);
        }
        else if(Input.GetButtonUp("Jump"))
        {
            jumpStop = true;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }        
        if(inputBuffer.Count > 0)
        {
            if(inputBuffer.Peek() == KeyCode.Space)
            {
                Jump();
                inputBuffer.Dequeue();
            }
        }
    }
    public override void FixedUpdate() 
    {
        if(!Pv.IsMine)
            return;
        base.FixedUpdate();
    }
    public void BufferDeQueue()
    {
        if(inputBuffer.Count > 0)
        {
            inputBuffer.Dequeue();
        }
    }
}

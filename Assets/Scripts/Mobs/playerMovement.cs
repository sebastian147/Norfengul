using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : mob
{
    private float jumpForce= 5f;
    bool isJumping = false;
    bool isJumping2 = false;
    bool jumpKeyHeld;
    public Vector2 counterJumpForce;
    public float jumpHeight = 10f;
    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }    
    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);

        if(!Pv.IsMine)
            return;
        base.Update();

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if(Input.GetMouseButtonDown(1) && m_Grounded == true)
        {
            isJumping =true;

            m_Rigidbody2D.velocity = Vector2.up *jumpForce;
            animator.SetBool("isJumping", isJumping);

        }
        if(Input.GetMouseButton(1) && isJumping)
        {
            if(jumpTimeCounter>0)
            {
                m_Rigidbody2D.velocity = Vector2.up *jumpForce;
                jumpTimeCounter -= Time.deltaTime; 
            }
            else
            {
                isJumping=false;
            }
        }
        if(Input.GetMouseButtonUp(1))
        {
            jumpTimeCounter = 0.35f;
            isJumping = false;
            animator.SetBool("isJumping", isJumping);

        }
        /*if(Input.GetMouseButtonDown(0))
        {
            if(Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }*/
        if(Input.GetMouseButtonDown(0))
        {
            jumpKeyHeld = true;
            if(m_Grounded)
            {
                isJumping2 = true;
                m_Rigidbody2D.AddForce(Vector2.up * jumpForce * m_Rigidbody2D.mass, ForceMode2D.Impulse);
                animator.SetBool("isJumping", isJumping2);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {

            jumpKeyHeld = false;
            animator.SetBool("isJumping", isJumping2);
        }

    }
    public override void FixedUpdate() 
    {
        if(!Pv.IsMine)
            return;
        base.FixedUpdate();
        if(isJumping2)
        {
            if(!jumpKeyHeld && Vector2.Dot(m_Rigidbody2D.velocity, Vector2.up) > 0)
            {
                m_Rigidbody2D.AddForce(counterJumpForce * m_Rigidbody2D.mass);
            }
        }
    }

}

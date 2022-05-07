using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionUpdates : MonoBehaviour
{
	
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    [SerializeField] private LayerMask m_whatIsDeath;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	[SerializeField] private float _groundRayCastLenght = 0.25f;//move variable ?
	[SerializeField] private float offset = 0.23f;

    public void CollisionCheck()
    {
        GizmosDraw();
        IsDeathZoneCheck();
        IsGroundedCheck();
    }
	public void IsDeathZoneCheck()
	{
		Collider2D[] collidersD = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_whatIsDeath);

        for (int i = 0; i < collidersD.Length; i++)
        {
			//Die();
        }
	}

	protected bool m_Grounded;            // Whether or not the player is grounded.
    public void IsGroundedCheck()
	{
        //bool wasGrounded = m_Grounded;
		//m_Grounded = false;
		RaycastHit2D raycastSuelo = Physics2D.Raycast(m_GroundCheck.position,Vector2.down, _groundRayCastLenght ,m_WhatIsGround);
		RaycastHit2D raycastSuelo2 = Physics2D.Raycast(m_GroundCheck.position-new Vector3(offset,0,0),Vector2.down, _groundRayCastLenght ,m_WhatIsGround);
		RaycastHit2D raycastSuelo3 = Physics2D.Raycast(m_GroundCheck.position+new Vector3(offset,0,0),Vector2.down, _groundRayCastLenght ,m_WhatIsGround);

		if(raycastSuelo || raycastSuelo2 || raycastSuelo3)
		{
			m_Grounded = true;
            /*if (!wasGrounded)
            {
                jumping = false;
                animator.SetBool("isJumping", false);
				jumpsends = 0;
				jumpdones = 0;
				timeInAir = 0;
            }*/
		}
		else
		{
			m_Grounded = false;
		}
		/*else if(!jumping)
		{
			timeInAir += Time.fixedDeltaTime;
			if(timeInAir < allowedTimeInAir)
			{
				m_Grounded = true;
			}
		}*/
	}
    public void GizmosDraw()
    {
		//Gizmos.color = Color.red;
        //Gizmos.DrawLine(m_GroundCheck.position, m_GroundCheck.position+Vector3.down*_groundRayCastLenght);
		//Gizmos.DrawLine(m_GroundCheck.position-new Vector3(offset,0,0), m_GroundCheck.position+Vector3.down*_groundRayCastLenght-new Vector3(offset,0,0));
		//Gizmos.DrawLine(m_GroundCheck.position+new Vector3(offset,0,0), m_GroundCheck.position+Vector3.down*_groundRayCastLenght+new Vector3(offset,0,0));
    }
}

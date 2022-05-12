using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionUpdates : MonoBehaviour
{
	


    public void CollisionCheck(Mob myMob)
    {
        IsDeathZoneCheck(myMob);
        IsGroundedCheck(myMob);
    }
	public void IsDeathZoneCheck(Mob myMob)
	{
		Collider2D[] collidersD = Physics2D.OverlapCircleAll(myMob.m_GroundCheck.position, myMob.k_GroundedRadius, myMob.m_whatIsDeath);

        for (int i = 0; i < collidersD.Length; i++)
        {
			//Die();
        }
	}

    public void IsGroundedCheck(Mob myMob)
	{
        //bool wasGrounded = m_Grounded;
		//m_Grounded = false;
		RaycastHit2D raycastSuelo = Physics2D.Raycast(myMob.m_GroundCheck.position,Vector2.down, myMob._groundRayCastLenght ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSuelo2 = Physics2D.Raycast(myMob.m_GroundCheck.position-new Vector3(myMob.offset,0,0),Vector2.down, myMob._groundRayCastLenght ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSuelo3 = Physics2D.Raycast(myMob.m_GroundCheck.position+new Vector3(myMob.offset,0,0),Vector2.down, myMob._groundRayCastLenght ,myMob.m_WhatIsGround);

		if(raycastSuelo || raycastSuelo2 || raycastSuelo3)
		{
			myMob.m_Grounded = true;
            /*if (!wasGrounded)
            {

            }*/
		}
		else
		{
			myMob.m_Grounded = false;
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
}

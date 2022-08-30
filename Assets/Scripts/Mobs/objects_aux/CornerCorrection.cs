using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCorrection
{
    public void CornerCorrectionAll(Mob myMob)
    {
        CornerCorrectionTop(myMob);
        //CornerCorrectionTopSize(myMob);
        CornerCorrectionbottom(myMob);
    }
	public void CornerCorrectionTopSize(Mob myMob)
    {
        RaycastHit2D raycastSueloLeft = Physics2D.Raycast(myMob.m_CeilingCheck.position-new Vector3(myMob.distanceFromMidle,-myMob.offsetInB,0),Vector2.left, myMob._topRayCastLenghtB ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloLeft2 = Physics2D.Raycast(myMob.m_CeilingCheck.position-new Vector3(myMob.distanceFromMidle,-myMob.offsetOutB,0),Vector2.left, myMob._topRayCastLenghtB ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloRight = Physics2D.Raycast(myMob.m_CeilingCheck.position+new Vector3(myMob.distanceFromMidle,myMob.offsetInB,0),Vector2.right, myMob._topRayCastLenghtB ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloRight2 = Physics2D.Raycast(myMob.m_CeilingCheck.position+new Vector3(myMob.distanceFromMidle,myMob.offsetOutB,0),Vector2.right, myMob._topRayCastLenghtB ,myMob.m_WhatIsGround);
		if((!raycastSueloLeft && raycastSueloLeft2) && (!raycastSueloRight && !raycastSueloRight2) && !myMob.m_Grounded && myMob.myRigidbody.velocity.x <0)
		{
			myMob.transform.position += new Vector3(-0.1f,0.1f,0);
		}
		else if((!raycastSueloLeft && !raycastSueloLeft2) && (!raycastSueloRight && raycastSueloRight2) && !myMob.m_Grounded && myMob.myRigidbody.velocity.x >0)
		{
			myMob.transform.position += new Vector3(0.1f,0.1f,0);
		}
    }
	public void CornerCorrectionTop(Mob myMob)
	{
		RaycastHit2D raycastSueloLeft = Physics2D.Raycast(myMob.m_CeilingCheck.position-new Vector3(myMob.offsetIn,0,0),Vector2.up, myMob._topRayCastLenght ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloLeft2 = Physics2D.Raycast(myMob.m_CeilingCheck.position-new Vector3(myMob.offsetOut,0,0),Vector2.up, myMob._topRayCastLenght ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloRight = Physics2D.Raycast(myMob.m_CeilingCheck.position+new Vector3(myMob.offsetIn,0,0),Vector2.up, myMob._topRayCastLenght ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloRight2 = Physics2D.Raycast(myMob.m_CeilingCheck.position+new Vector3(myMob.offsetOut,0,0),Vector2.up, myMob._topRayCastLenght ,myMob.m_WhatIsGround);
		if((!raycastSueloLeft && raycastSueloLeft2) && (!raycastSueloRight && !raycastSueloRight2))
		{
			myMob.transform.position += new Vector3(myMob.offsetOut-myMob.offsetIn,0,0);
		}
		else if((!raycastSueloLeft && !raycastSueloLeft2) && (!raycastSueloRight && raycastSueloRight2))
		{
			myMob.transform.position -= new Vector3(myMob.offsetOut-myMob.offsetIn,0,0);
		}
	
	}
	public void CornerCorrectionbottom (Mob myMob)
	{
		RaycastHit2D raycastSueloLeft = Physics2D.Raycast(myMob.m_GroundCheck.position-new Vector3(myMob.distanceFromMidle,-myMob.offsetInB,0),Vector2.left, myMob._topRayCastLenghtB ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloLeft2 = Physics2D.Raycast(myMob.m_GroundCheck.position-new Vector3(myMob.distanceFromMidle,-myMob.offsetOutB,0),Vector2.left, myMob._topRayCastLenghtB ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloRight = Physics2D.Raycast(myMob.m_GroundCheck.position+new Vector3(myMob.distanceFromMidle,myMob.offsetInB,0),Vector2.right, myMob._topRayCastLenghtB ,myMob.m_WhatIsGround);
		RaycastHit2D raycastSueloRight2 = Physics2D.Raycast(myMob.m_GroundCheck.position+new Vector3(myMob.distanceFromMidle,myMob.offsetOutB,0),Vector2.right, myMob._topRayCastLenghtB ,myMob.m_WhatIsGround);
		if((!raycastSueloLeft && raycastSueloLeft2) && (!raycastSueloRight && !raycastSueloRight2) && !myMob.m_Grounded && myMob.myRigidbody.velocity.x <0)
		{
			myMob.transform.position += new Vector3(-0.1f,0.1f,0);
		}
		else if((!raycastSueloLeft && !raycastSueloLeft2) && (!raycastSueloRight && raycastSueloRight2) && !myMob.m_Grounded && myMob.myRigidbody.velocity.x >0)
		{
			myMob.transform.position += new Vector3(0.1f,0.1f,0);
		}
	}
}

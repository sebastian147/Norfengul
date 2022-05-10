using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnJumpState : MobBaseState
{
    public override void animate(Mob myMob)
    {
        return;
    }
    public override void EndState(Mob myMob)
    {
        myMob.jumping = false;
        myMob.myAnimator.SetBool("isJumping", false);
		myMob.jumpsends = 0;
		myMob.jumpdones = 0;
		myMob.timeInAir = 0;
    }
    public override void StarState(Mob myMob)
    {
		myMob.m_JumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, myMob.jumpHeight);//mover a start o awake cuando se sete el valor
    }
    public override void CheckChangeState(Mob myMob)
    {
        if(myMob.m_Grounded)
        {
            myMob.actualState = myMob.myStateMachine.changeState(0,2,myMob);
        }
        if(myMob.m_Grounded && Mathf.Abs(myMob.horizontalMove)>0)
        {
            myMob.actualState = myMob.myStateMachine.changeState(1,2,myMob);
        }
    }
    public override void UpdateState(Mob myMob)
    {

		if(myMob.jumpsends<myMob.amountOfJumps)
		{
			myMob.jumpsends++;
			myMob.jumping = true;
			myMob.jumpBufferCounter = 0f;
		}
		/*if(myMob.jumpsends == 1 && !myMob.m_Grounded)
		{
			myMob.jumpsends = 0;
			myMob.jumping = false;
			myMob.jumpBufferCounter = 0f;
		}*/
		/*if(myMob.jumpBufferCounter > 0 && myMob.m_Grounded && myMob.jumpsends == 0 )
		{
			myMob.jumpsends++;
			myMob.jumpBufferCounter = 0f;
			myMob.jumping = true;
		}*/
		myMob.myAnimator.SetBool("isJumping", true);
        CheckChangeState(myMob);

    }
    public override void FixedUpdateState(Mob myMob)
    {
        // If the player should jump...
		if (myMob.jumpdones < myMob.jumpsends && myMob.jumping == true)
		{
            Debug.Log("hola");
			myMob.jumpdones ++;
			// Add a vertical force to the player.
			myMob.myRigidbody.velocity = new Vector2(myMob.myRigidbody.velocity.x, 0);
			myMob.myRigidbody.AddForce(Vector2.up * myMob.m_JumpForce * myMob.myRigidbody.mass, ForceMode2D.Impulse);
		}
		/*if(myMob.jumping)
        {
            if(myMob.jumpStop && Vector2.Dot(myMob.myRigidbody.velocity, Vector2.up) > 0)
            {
                myMob.myRigidbody.AddForce(new Vector2(0f, -myMob.counterJumpForce) * myMob.myRigidbody.mass);
            }
			if(Mathf.Abs(myMob.myRigidbody.velocity.y) <  0.15f && myMob.apexModifierTimeCount > 0)
			{

				myMob.apexModifierTimeCount -= Time.fixedDeltaTime;
				myMob.apexModifierCurrent = myMob.apexModifier;
				myMob.myRigidbody.gravityScale = 0;
				myMob.myRigidbody.velocity = new Vector3(myMob.myRigidbody.velocity.x, 0,1);
			}
			else
			{
				myMob.apexModifierTimeCount = myMob.apexModifierTime;
				myMob.myRigidbody.gravityScale = 3;
			}
        }
		else
		{
			myMob.apexModifierCurrent = 1;
			myMob.apexModifierTimeCount = myMob.apexModifierTime;
			myMob.myRigidbody.gravityScale = 3;
		}*/
    }
    private static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }  
}

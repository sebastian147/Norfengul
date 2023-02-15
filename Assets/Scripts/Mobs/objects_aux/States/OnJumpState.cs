using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnJumpState : MobBaseState
{
        float originalGravity;
	private bool jumpMade;
	private float timeInAir;
	private int timesEnter;
	private float move;
	private float jumpTimeFromWall = 0;
	private float jumpTimeFromWallMax = 0.4f;
	private float jumpTime = 0;
	private float jumpTimeMax = 0.4f;
	CornerCorrection c = new CornerCorrection();
        public override void animate(Mob myMob)
        {
                myMob.myAnimator.SetBool("isJumping", true);
                return;
        }
        public override void EndState(Mob myMob)
        {
                myMob.jumping = false;
		myMob.myAnimator.SetBool("isJumping", false);
		//myMob.jumpsends = 0;
		//myMob.jumpdones = 0;
		//myMob.timeInAir = 0;

		//wall grabbing
                myMob.wallGrabingJumpforce = 0;
		myMob.wallGrabingDirection = 0;

		myMob.apexModifierCurrent = 1;
		myMob.apexModifierTimeCount = myMob.apexModifierTime;
		myMob.myRigidbody.gravityScale = 3;

		myMob.wallGrabing = false;
                myMob.myRigidbody.gravityScale = originalGravity;

        }
        public override void StarState(Mob myMob)
        {
                animate(myMob);
                originalGravity = myMob.myRigidbody.gravityScale;
                myMob.m_JumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, myMob.jumpHeight);//mover a start o awake cuando se sete el valor
                jumpMade = false;
                myMob.timeInAir = myMob.allowedTimeInAir;
                jumpTime = jumpTimeMax;
                if(myMob.wallGrabing == true)//time from wall jump
                {
                        Flip(myMob);
                        move = myMob.horizontalMove;
                        jumpTimeFromWall = jumpTimeFromWallMax;
                }
        }
        public override void CheckChangeState(Mob myMob)
        {
                if(((myMob._inWallRight && myMob.horizontalMove > 0)  || (myMob._inWallLeft && myMob.horizontalMove<0)) && !myMob.m_Grounded && !myMob.drop)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.WallGrabing,myMob);
                        return;	
                }
                if(myMob.attacking == true)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Attack,myMob);
                        return;
                }
                if(myMob.m_Grounded && Mathf.Abs(myMob.myRigidbody.velocity.x) > 1)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk,myMob);
                        return;
                }
                if(myMob.m_Grounded)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle,myMob);
                        return;
                }
        }
        public override void UpdateState(Mob myMob)
        {
                if(!myMob.wallGrabing)
                        base.UpdateState(myMob);
                if(myMob.jumping)
                        MakeAJump(myMob);
                if(myMob.jumpBufferCounter > 0 && myMob.m_Grounded && myMob.jumpsends == 0 )//buffer
                {
                        myMob.jumpsends++;
                        myMob.jumpBufferCounter = 0f;
                }
                jumpMade = JumpCheck(myMob);

                c.CornerCorrectionAll(myMob);

                if(jumpMade || jumpTime <= 0)
                        CheckChangeState(myMob);//revisar
                else
                {
                        jumpTime -= Time.fixedDeltaTime;
                }
                if((myMob.dashRight || myMob.dashLeft) && myMob.canDash)//arreglar
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Dash,myMob);
                }
        }
        public override void FixedUpdateState(Mob myMob)
        {
                // If the player should jump...
                if (myMob.jumpdones < myMob.jumpsends)
                {
                        myMob.jumpdones ++;
                        // Add a vertical force to the player.
                        myMob.myRigidbody.velocity = new Vector2(myMob.myRigidbody.velocity.x, 0);
                        myMob.myRigidbody.AddForce(Vector2.up * myMob.m_JumpForce * myMob.myRigidbody.mass, ForceMode2D.Impulse);
                }
                if(myMob.jumpStop && Vector2.Dot(myMob.myRigidbody.velocity, Vector2.up) > 0)
                {
                        myMob.myRigidbody.AddForce(new Vector2(0f, -myMob.counterJumpForce) * myMob.myRigidbody.mass);
                }
                if(Mathf.Abs(myMob.myRigidbody.velocity.y) <  0.15f && myMob.apexModifierTimeCount > 0)
                {

                        myMob.apexModifierTimeCount -= Time.fixedDeltaTime;
                        myMob.apexModifierCurrent = myMob.apexModifier;
                        myMob.myRigidbody.gravityScale = 0;
                        myMob.myRigidbody.velocity = new Vector3(myMob.myRigidbody.velocity.x, 0, 0);

                }
                else
                {
                        myMob.apexModifierCurrent = 1;
                        myMob.apexModifierTimeCount = myMob.apexModifierTime;
                        myMob.myRigidbody.gravityScale = originalGravity;
                }

                /*if(myMob.jumping)
                {


                }
                else
                {
                        myMob.apexModifierCurrent = 1;
                        myMob.apexModifierTimeCount = myMob.apexModifierTime;
                        myMob.myRigidbody.gravityScale = 3;
                }*/
                if(!myMob.m_Grounded && myMob.timeInAir > 0)//coyote time timer
                {
                        myMob.timeInAir -= Time.fixedDeltaTime;
                }
                if(myMob.wallGrabing == true && jumpTimeFromWall>=0)
                {
                        myMob.horizontalMove = move;
                        jumpTimeFromWall -= Time.fixedDeltaTime;
                }
                else
                {
                        myMob.wallGrabing = false;
                }
                //move on air falta checkear
                Vector3 targetVelocity = new Vector2(myMob.horizontalMove * myMob.moveSpeed*myMob.apexModifierCurrent, myMob.myRigidbody.velocity.y);
                // And then smoothing it out and applying it to the character
                myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing); 
        }
        private void MakeAJump(Mob myMob)
        {
                if(myMob.jumpsends<myMob.amountOfJumps)//normal jumps
                {
                        myMob.jumpsends++;
                        myMob.jumpBufferCounter = 0f;
                }
                if(myMob.jumpsends == 1 && !myMob.m_Grounded && myMob.timeInAir <= 0)//coyote time
                {
                        myMob.jumpsends = 0;
                        myMob.jumpBufferCounter = 0f;
                }
                myMob.jumping = false;
        }

        private static float CalculateJumpForce(float gravityStrength, float jumpHeight)
        {
                //h = v^2/2g
                //2gh = v^2
                //sqrt(2gh) = v
                return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
        } 
        private bool JumpCheck (Mob myMob)
        {
                if(myMob.m_Grounded == false)
                {
                        jumpMade = true;
                }
                return jumpMade;
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobBaseState
{



        /*public virtual int jump(Mob myMob){

        }

        public virtual int crouch(Mob myMob){

        }

        public virtual int onCollision(Mob myMob, Collision2D collision);*/

        
        public virtual void UpdateState(Mob myMob)
        {
                Fliping(myMob);
        }
        
        public abstract void animate(Mob  myMob);
        public abstract void EndState(Mob myMob);
        public abstract void StarState(Mob myMob);
        public abstract void CheckChangeState(Mob myMob);
        public abstract void FixedUpdateState(Mob myMob);
        public virtual void SwitchState(Mob myMob){

        }
    
    
        public virtual void Fliping(Mob myMob)//mover dentro de los estados
        {
        // If the input is moving the player right and the player is facing left...
		if (myMob.horizontalMove > 0 && !myMob.m_FacingRight)
		{
			// ... flip the player.
			Flip(myMob);
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (myMob.horizontalMove < 0 && myMob.m_FacingRight)
		{
			// ... flip the player.
			Flip(myMob);
		}
        }
        protected void Flip(Mob myMob)
	{

		// Switch the way the player is labelled as facing.
		myMob.m_FacingRight = !myMob.m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = myMob.transform.localScale;
		theScale.x *= -1;
		myMob.transform.localScale = theScale;
	}

}

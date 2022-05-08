using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobBaseState
{
    protected float horizontalMove = 0f;
    protected float moveSpeed = 40f;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement


    /*public virtual int jump(Mob myMob){

    }

    public virtual int crouch(Mob myMob){

    }

    public virtual int onCollision(Mob myMob, Collision2D collision);*/
    private void UpdateStates() {
        
    }
    
    public virtual int move(Mob myMob)//mover de aca
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
		Vector3 targetVelocity = new Vector2(horizontalMove * 10f/*apexModifierCurrent*/, myMob.myRigidbody.velocity.y);
		// And then smoothing it out and applying it to the character
		myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);        return 1;
        return 1;
    }
    
    public abstract void animate(Mob  myMob);
    public abstract void EndState(Mob myMob);
    public abstract void StarState(Mob myMob);
    public abstract void CheckChangeState(Mob myMob);
    public abstract void UpdateState(Mob myMob);
    public abstract void FixedUpdateState(Mob myMob);
    public virtual void SwitchState(Mob myMob){

    }
 
}

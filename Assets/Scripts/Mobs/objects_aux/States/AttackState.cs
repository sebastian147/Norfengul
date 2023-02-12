using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MobBaseState
{
        private string attackTipe = "";
        private float attackDirection;
        private HashSet<int> hitPlayerIDs;
        public override void animate(Mob myMob)
        {
                //  GameObject.Instantiate(myMob.HitParticles, myMob.attackPoint.position, Quaternion.identity);
                myMob.myAnimator.SetBool(attackTipe, true);
        }
        public override void EndState(Mob myMob)
        {
                myMob.myAnimator.SetBool(attackTipe, false);
                //myMob.myAnimator.Weapon;
        }
        public override void StarState(Mob myMob)
        {
                hitPlayerIDs = new HashSet<int>();
                attackDirection=myMob.horizontalMove;
                attackTipe =myMob.arma.Armas.weaponType.ToString();
                myMob.attacking = false;
                animate(myMob);
                CheckEnemysToAttack(myMob);
        }
        public override void CheckChangeState(Mob myMob)
        {
                if(Mathf.Abs(myMob.horizontalMove) != 0)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Walk,myMob);
                        return;
                }
                /*if(myMob.jumpBufferCounter>0 || !myMob.m_Grounded)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(2,0,myMob);
                        return;
                }*/
                if(Mathf.Abs(myMob.horizontalMove) == 0)
                {
                        myMob.actualState = myMob.myStateMachine.changeState(myStates.Idle,myMob);
                        return;
                }
        }
        public override void UpdateState(Mob myMob)
        {
                //base.UpdateState(myMob);//dont flip while attack
                CheckPlayersToAttack(myMob);
                if( !(myMob.myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1))
                        CheckChangeState(myMob);

        }
        public override void FixedUpdateState(Mob myMob)
        {
                Vector3 targetVelocity = new Vector2(myMob.horizontalMove * myMob.moveSpeed/*apexModifierCurrent*/, myMob.myRigidbody.velocity.y);
                // And then smoothing it out and applying it to the character
                myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref myMob.m_Velocity, myMob.m_MovementSmoothing); 
        }
	private void CheckPlayersToAttack(Mob myMob)
	{
		//attack players
		if(myMob.friendlyFire)
		{
			Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(myMob.attackPoint.position, myMob.arma.Armas.damageArea, myMob.playerLayers);
			//damage them
			for (int i = 0; i < hitPlayer.Length; i++)
			{
                                int hitPlayerID = hitPlayer[i].gameObject.GetInstanceID();
				if (hitPlayer[i].gameObject.GetInstanceID() != myMob.gameObject.GetInstanceID() && !hitPlayerIDs.Contains(hitPlayerID))//rev
				{
                                        hitPlayerIDs.Add(hitPlayerID);
					//hitPlayer[i].GetComponent<Mob>().actualState = hitPlayer[i].GetComponent<Mob>().myStateMachine.changeState(4,3,myMob);
                                        hitPlayer[i].GetComponent<Mob>().TakeDamage(myMob.arma.Armas.damage);
                                        //hitPlayer[i].GetComponent<Mob>().myStateMachine.changeState(myStates.Damage,hitPlayer[i].GetComponent<Mob>());
                                        return;
				}
			}
		}
	}
	private void CheckEnemysToAttack(Mob myMob)
	{
                //detect enemis
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(myMob.attackPoint.position, myMob.arma.Armas.damageArea, myMob.enemyLayers);
                //damage them
                foreach (Collider2D enemy in hitEnemies)
                {
                //enemy.GetComponent<Enemy>().TakeDamage(myMob.attackDamage);
                }
	}
}

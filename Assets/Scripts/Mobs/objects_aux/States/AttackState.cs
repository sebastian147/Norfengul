using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MobBaseState
{
        private int pass = 10000;
        public override void animate(Mob myMob)
        {
                //  GameObject.Instantiate(myMob.HitParticles, myMob.attackPoint.position, Quaternion.identity);
                myMob.myAnimator.SetBool("isAttack", true);
        }
        public override void EndState(Mob myMob)
        {
                myMob.myAnimator.SetBool("isAttack", false);
        }
        public override void StarState(Mob myMob)
        {
                myMob.attacking = false;
                pass = 10;
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
                base.UpdateState(myMob);//rev
                animate(myMob);
                if(pass <=0)
                        CheckChangeState(myMob);
                else
                        pass--;

                CheckEnemysToAttack(myMob);
                CheckPlayersToAttack(myMob);
        }
        public override void FixedUpdateState(Mob myMob)
        {

        }
	private void CheckPlayersToAttack(Mob myMob)
	{
		//attack players
		if(myMob.friendlyFire)
		{
			Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(myMob.attackPoint.position, myMob.attackRange, myMob.playerLayers);
			//damage them
			for (int i = 0; i < hitPlayer.Length; i++)
			{
				if (hitPlayer[i].gameObject.GetInstanceID() != myMob.gameObject.GetInstanceID())//rev
				{
					//hitPlayer[i].GetComponent<Mob>().actualState = hitPlayer[i].GetComponent<Mob>().myStateMachine.changeState(4,3,myMob);
                                        hitPlayer[i].GetComponent<Mob>().TakeDamage(myMob.attackDamage);
                                        return;
				}
			}
		}
	}
	private void CheckEnemysToAttack(Mob myMob)
	{
                //detect enemis
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(myMob.attackPoint.position, myMob.attackRange, myMob.enemyLayers);
                //damage them
                foreach (Collider2D enemy in hitEnemies)
                {
                //enemy.GetComponent<Enemy>().TakeDamage(myMob.attackDamage);
                }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class _mob : MonoBehaviourPunCallbacks
{
	/*




    [Header("test")]
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching



	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	protected Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[SerializeField] private int maxHealth = 100;
	private int currentHealth = 0;
	[SerializeField] private HealthBar healthBar;
	[SerializeField] GameObject ui;
	public int attackDamage = 10;
	public bool friendlyFire = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask playerLayers;
	protected float attackRate = 2f;
	protected float nextAttackTime = 0f;
	[SerializeField] private Collider2D HitBoxColliders;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	protected PhotonView Pv;
	PlayerManager playerManager;

    public virtual void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
		Pv = GetComponent<PhotonView>();
		playerManager = PhotonView.Find((int)Pv.InstantiationData[0]).GetComponent<PlayerManager>();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
		if(Pv.IsMine)
			healthBar.SetMaxHealth(maxHealth);
		else
			Destroy(ui);

	}
	public virtual void Star()
	{

	}
    // Update is called once per frame
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {


        //move or character
        Move(horizontalMove * Time.fixedDeltaTime, false);
		FixedJump();


    }






	public void TakeDamage(int damage)
	{
		Pv.RPC("RPC_TakeDamage", RpcTarget.AllBuffered, damage);//nombre funcion, a quien se lo paso, valor
	}

    //funcion para attacar mele
    protected virtual void Attack()
    {
        //update animation
        animator.SetTrigger("Attack");
		CheckEnemysToAttack();
		CheckPlayersToAttack();
    }
	private void CheckPlayersToAttack()
	{
		//attack players
		if(friendlyFire)
		{
			Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
			//damage them
			for (int i = 0; i < hitPlayer.Length; i++)
			{
				if (hitPlayer[i].gameObject.GetInstanceID() != gameObject.GetInstanceID())//rev
				{
					hitPlayer[i].GetComponent<playerMovement>().TakeDamage(attackDamage);
				}
			}
		}
	}
	private void CheckEnemysToAttack()
	{
        //detect enemis
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
	}
    protected void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

		
		Gizmos.DrawLine(m_CeilingCheck.position-new Vector3(offsetIn,0,0),Vector3.up*_topRayCastLenght+m_CeilingCheck.position-new Vector3(offsetIn,0,0));
		Gizmos.DrawLine(m_CeilingCheck.position-new Vector3(offsetOut,0,0),Vector3.up*_topRayCastLenght+m_CeilingCheck.position-new Vector3(offsetOut,0,0));
		Gizmos.DrawLine(m_CeilingCheck.position+new Vector3(offsetIn,0,0),Vector3.up*_topRayCastLenght+m_CeilingCheck.position+new Vector3(offsetIn,0,0));
		Gizmos.DrawLine(m_CeilingCheck.position+new Vector3(offsetOut,0,0),Vector3.up*_topRayCastLenght+m_CeilingCheck.position+new Vector3(offsetOut,0,0));

		Gizmos.DrawLine(m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetInB,0),Vector3.left*_topRayCastLenghtB+m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetInB,0));
		Gizmos.DrawLine(m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetOutB,0),Vector3.left*_topRayCastLenghtB+m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetOutB,0));
		Gizmos.DrawLine(m_GroundCheck.position+new Vector3(distanceFromMidle,offsetInB,0),Vector3.right*_topRayCastLenghtB+m_GroundCheck.position+new Vector3(distanceFromMidle,offsetInB,0));
		Gizmos.DrawLine(m_GroundCheck.position+new Vector3(distanceFromMidle,offsetOutB,0),Vector3.right*_topRayCastLenghtB+m_GroundCheck.position+new Vector3(distanceFromMidle,offsetOutB,0));
	}
    protected void Die()
    {
		animator.SetBool("IsDead", true);
		gameObject.GetComponent<Dissolve>().Active();
		//GetComponent<BoxCollider2D>().enabled = false;
		//GetComponent<CircleCollider2D>().enabled = false;
		//m_Rigidbody2D.isKinematic = true;
		this.enabled = false;
		if(Pv.IsMine)
			playerManager.Die();
    }
	[PunRPC]
	protected void RPC_TakeDamage(int damage)
	{
		currentHealth -= damage;
		if(Pv.IsMine)
			healthBar.SetHealth(currentHealth);
		//animacion de lastimado
		if(currentHealth <= 0)
		{
			Die();
		}
	}
	//funcion para actualizar controlador salto
    protected float Jump(float counter = 0f)
    {

    }
	//move character
    public void Move(float move, bool crouch)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f*apexModifierCurrent, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

	}
	private void FixedJump()
	{

	}
  
	//flip character
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
*/
}

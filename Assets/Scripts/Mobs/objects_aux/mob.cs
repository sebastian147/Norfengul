using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class mob : MonoBehaviour
{
    public Animator animator;
    protected bool jump = false; //verify

    float horizontalMove = 0f;
    public float runSpeed = 40f;
	public int amountOfJumps = 1;
	private int jumpdones = 0;
	private int jumpsends = 0;
    private bool jumping = false;//boorame?

    [Header("test")]

	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	public int maxHealth = 100;
	private int currentHealth = 0;
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

	protected PhotonView view;


    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}
    public virtual void Start()
    {
		view = GetComponent<PhotonView>();
    }
    // Update is called once per frame
    public virtual void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
    }
    public virtual void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    jumping = false;
                    animator.SetBool("isJumping", false);
					jumpsends = 0;
					jumpdones = 0;
                }
            }
        }
        //move or character
        Move(horizontalMove * Time.fixedDeltaTime, false, jumping);
    }


	public void TakeDamage(int damage)
	{
		RPC_TakeDamage(damage, true);
		view.RPC("RPC_TakeDamage", RpcTarget.AllBuffered, damage, false);//nombre funcion, a quien se lo paso, valor
	}

    //funcion para attacar mele
    protected virtual void Attack()
    {
        //update animation
        animator.SetTrigger("Attack");
        //detect enemis
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
		//attack players
		if(friendlyFire)
		{
			Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
			//damage them
			for (int i = 0; i < hitPlayer.Length; i++)
			{
				if (hitPlayer[i].gameObject.GetInstanceID() != gameObject.GetInstanceID())
				{
					hitPlayer[i].GetComponent<playerMovement>().TakeDamage(attackDamage);
				}
			}
		}
    }
    protected void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    protected void Die()
    {
		animator.SetBool("IsDead", true);
		gameObject.GetComponent<Dissolve>().Active();
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<CircleCollider2D>().enabled = false;
		m_Rigidbody2D.isKinematic = true;
 		this.enabled = false;
    }
	
	
	//funcion para actualizar controlador salto
    protected void Jump()
    {
		if(jumpsends<amountOfJumps)
		{
			jumpsends++;
			jumping = true;
		}
		if(jumpsends == 1 && !m_Grounded)
		{
			jumpsends = 0;
			jumping = false;
		}
		animator.SetBool("isJumping", jumping);
    }
    public void Move(float move, bool crouch, bool jump)
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
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
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
		// If the player should jump...
		if (jumpdones < jumpsends && jumping == true)
		{
			jumpdones ++;
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	[PunRPC]
	protected void RPC_TakeDamage(int damage, bool updateClient)
	{
		if(!view.IsMine && !updateClient)//solo funciona en la compu del otro//rev update
			return;
		Debug.Log(" diee");
		currentHealth -= damage;
		//animacion de lastimado
		if(currentHealth <= 0)
		{
			Die();
		}
	}
}
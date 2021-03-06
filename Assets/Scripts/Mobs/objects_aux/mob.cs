using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class mob : MonoBehaviourPunCallbacks
{
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 40f;

	[Header("Jump")]
	[SerializeField] public int amountOfJumps = 1;
	[SerializeField] public float counterJumpForce = 40f;
    [SerializeField] public float jumpHeight = 10f;
	[SerializeField] private float allowedTimeInAir = 0.1f;
	[SerializeField] private float _groundRayCastLenght = 0.25f;//move variable ?
	[SerializeField] private float offset = 0.23f;
	[SerializeField] private float apexModifier = 2f;
	[SerializeField] private float apexModifierTime = 0.3f;
	private float apexModifierCurrent = 1f;
	private float apexModifierTimeCount = 0.3f;
	private float timeInAir = 0;
	protected bool jumpStop = false;
 	private float m_JumpForce = 5f;						// Amount of force added when the player jumps.
	private int jumpdones = 0;
	private int jumpsends = 0;
    private bool jumping = false;//boorame?

	[Header("CornerCorrection")]
	[SerializeField] private float offsetOut = 0.27f;
	[SerializeField] private float offsetIn = 0.15f;
	[SerializeField] private float _topRayCastLenght = 0.5f;
	[SerializeField] private float _topRayCastLenghtB = 0.5f;
	[SerializeField] private float offsetOutB = 0.27f;
	[SerializeField] private float offsetInB = 0.15f;
	[SerializeField] private float distanceFromMidle = 0.5f;

    [Header("test")]
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
	[SerializeField] private LayerMask m_whatIsDeath;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	protected bool m_Grounded;            // Whether or not the player is grounded.
	protected bool _inWall = false;
	[SerializeField] protected Transform m_WallCheck;
	[SerializeField] private float _wallRayCastLenght = 0.2f;
	protected float wallGrabingJumpforce=0;
	protected float wallGrabingDirection=0;
	[SerializeField] private float wallSlidingSpeed = 1;
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
		m_JumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, jumpHeight);//mover a start o awake cuando se sete el valor

		wallGrabing();

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;//move to player
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

    }
    public virtual void FixedUpdate()
    {
		IsGroundedCheck();
		IsDeathZoneCheck();
		IsWallCheck();


        //move or character
        Move(horizontalMove * Time.fixedDeltaTime, false);
		FixedJump();
		CornerCorrectionTop();
		CornerCorrectionbottom();

    }

	private void IsDeathZoneCheck()
	{
		Collider2D[] collidersD = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_whatIsDeath);

        for (int i = 0; i < collidersD.Length; i++)
        {
			Die();
        }
	}
	private void IsWallCheck()
	{
		RaycastHit2D raycastPared = Physics2D.Raycast(m_WallCheck.position,new Vector2(Input.GetAxisRaw("Horizontal"), 0), _wallRayCastLenght,m_WhatIsGround);
		if(Input.GetAxisRaw("Horizontal") == 0)
			return;			

		if(raycastPared)
		{
			_inWall = true;
		}
		else
		{
			_inWall = false;
		}

	}
	private void IsGroundedCheck()
	{
        bool wasGrounded = m_Grounded;
		m_Grounded = false;
		RaycastHit2D raycastSuelo = Physics2D.Raycast(m_GroundCheck.position,Vector2.down, _groundRayCastLenght ,m_WhatIsGround);
		RaycastHit2D raycastSuelo2 = Physics2D.Raycast(m_GroundCheck.position-new Vector3(offset,0,0),Vector2.down, _groundRayCastLenght ,m_WhatIsGround);
		RaycastHit2D raycastSuelo3 = Physics2D.Raycast(m_GroundCheck.position+new Vector3(offset,0,0),Vector2.down, _groundRayCastLenght ,m_WhatIsGround);

		if(raycastSuelo || raycastSuelo2 || raycastSuelo3)
		{
			m_Grounded = true;
            if (!wasGrounded)
            {
                jumping = false;
                animator.SetBool("isJumping", false);
				jumpsends = 0;
				jumpdones = 0;
				timeInAir = 0;
            }
		}
		else if(!jumping)
		{
			timeInAir += Time.fixedDeltaTime;
			if(timeInAir < allowedTimeInAir)
			{
				m_Grounded = true;
			}
		}
	}
	public void CornerCorrectionTop()
	{
		RaycastHit2D raycastSueloLeft = Physics2D.Raycast(m_CeilingCheck.position-new Vector3(offsetIn,0,0),Vector2.up, _topRayCastLenght ,m_WhatIsGround);
		RaycastHit2D raycastSueloLeft2 = Physics2D.Raycast(m_CeilingCheck.position-new Vector3(offsetOut,0,0),Vector2.up, _topRayCastLenght ,m_WhatIsGround);
		RaycastHit2D raycastSueloRight = Physics2D.Raycast(m_CeilingCheck.position+new Vector3(offsetIn,0,0),Vector2.up, _topRayCastLenght ,m_WhatIsGround);
		RaycastHit2D raycastSueloRight2 = Physics2D.Raycast(m_CeilingCheck.position+new Vector3(offsetOut,0,0),Vector2.up, _topRayCastLenght ,m_WhatIsGround);
		if((!raycastSueloLeft && raycastSueloLeft2) && (!raycastSueloRight && !raycastSueloRight2))
		{
			transform.position += new Vector3(offsetOut-offsetIn,0,0);
		}
		else if((!raycastSueloLeft && !raycastSueloLeft2) && (!raycastSueloRight && raycastSueloRight2))
		{
			transform.position -= new Vector3(offsetOut-offsetIn,0,0);
		}
	
	}
	public void CornerCorrectionbottom ()
	{
		RaycastHit2D raycastSueloLeft = Physics2D.Raycast(m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetInB,0),Vector2.left, _topRayCastLenghtB ,m_WhatIsGround);
		RaycastHit2D raycastSueloLeft2 = Physics2D.Raycast(m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetOutB,0),Vector2.left, _topRayCastLenghtB ,m_WhatIsGround);
		RaycastHit2D raycastSueloRight = Physics2D.Raycast(m_GroundCheck.position+new Vector3(distanceFromMidle,offsetInB,0),Vector2.right, _topRayCastLenghtB ,m_WhatIsGround);
		RaycastHit2D raycastSueloRight2 = Physics2D.Raycast(m_GroundCheck.position+new Vector3(distanceFromMidle,offsetOutB,0),Vector2.right, _topRayCastLenghtB ,m_WhatIsGround);
		if((!raycastSueloLeft && raycastSueloLeft2) && (!raycastSueloRight && !raycastSueloRight2) && !m_Grounded && m_Rigidbody2D.velocity.x <0)
		{
			Debug.Log("izquierda");
			transform.position += new Vector3(-0.1f,0.1f,0);
		}
		else if((!raycastSueloLeft && !raycastSueloLeft2) && (!raycastSueloRight && raycastSueloRight2) && !m_Grounded && m_Rigidbody2D.velocity.x >0)
		{
			Debug.Log("derecha");
			transform.position += new Vector3(0.1f,0.1f,0);
		}
	}
	bool wasInWall = false;
	public void wallGrabing()
	{
		if(_inWall)
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x,Mathf.Clamp(m_Rigidbody2D.velocity.y, -wallSlidingSpeed, float.MaxValue));//mover a fixed update
			//horizontalMove = 0;
			jumpsends=amountOfJumps;
			jumpdones=amountOfJumps;
			//wallGrabingDirection = Input.GetAxisRaw("Horizontal");
			//wallGrabingJumpforce = -20;
			//timeInAir = 0;
		}
		else
		{
			if(wasInWall)
			{
				jumping = false;
				jumpsends = amountOfJumps-1;
				jumpdones = amountOfJumps-1;
				timeInAir = -allowedTimeInAir;
			}

			wallGrabingJumpforce = 0;
			wallGrabingDirection = 0;
		}
		wasInWall = _inWall;
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
        //Gizmos.DrawWireSphere(attackPoint.position, attackRange);
		Gizmos.DrawLine(m_GroundCheck.position, m_GroundCheck.position+Vector3.down*_groundRayCastLenght);
		Gizmos.DrawLine(m_GroundCheck.position-new Vector3(offset,0,0), m_GroundCheck.position+Vector3.down*_groundRayCastLenght-new Vector3(offset,0,0));
		Gizmos.DrawLine(m_GroundCheck.position+new Vector3(offset,0,0), m_GroundCheck.position+Vector3.down*_groundRayCastLenght+new Vector3(offset,0,0));
		
		Gizmos.DrawLine(m_CeilingCheck.position-new Vector3(offsetIn,0,0),Vector3.up*_topRayCastLenght+m_CeilingCheck.position-new Vector3(offsetIn,0,0));
		Gizmos.DrawLine(m_CeilingCheck.position-new Vector3(offsetOut,0,0),Vector3.up*_topRayCastLenght+m_CeilingCheck.position-new Vector3(offsetOut,0,0));
		Gizmos.DrawLine(m_CeilingCheck.position+new Vector3(offsetIn,0,0),Vector3.up*_topRayCastLenght+m_CeilingCheck.position+new Vector3(offsetIn,0,0));
		Gizmos.DrawLine(m_CeilingCheck.position+new Vector3(offsetOut,0,0),Vector3.up*_topRayCastLenght+m_CeilingCheck.position+new Vector3(offsetOut,0,0));

		Gizmos.DrawLine(m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetInB,0),Vector3.left*_topRayCastLenghtB+m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetInB,0));
		Gizmos.DrawLine(m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetOutB,0),Vector3.left*_topRayCastLenghtB+m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetOutB,0));
		Gizmos.DrawLine(m_GroundCheck.position+new Vector3(distanceFromMidle,offsetInB,0),Vector3.right*_topRayCastLenghtB+m_GroundCheck.position+new Vector3(distanceFromMidle,offsetInB,0));
		Gizmos.DrawLine(m_GroundCheck.position+new Vector3(distanceFromMidle,offsetOutB,0),Vector3.right*_topRayCastLenghtB+m_GroundCheck.position+new Vector3(distanceFromMidle,offsetOutB,0));
	
		Gizmos.DrawLine(m_WallCheck.position, m_WallCheck.position+_wallRayCastLenght*new Vector3(transform.localScale.x, 0,0));

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
		if(jumpsends<amountOfJumps)
		{
			jumpsends++;
			jumping = true;
			counter = 0f;
		}
		if(jumpsends == 1 && !m_Grounded)
		{
			jumpsends = 0;
			jumping = false;
			counter = 0f;
		}
		if(counter > 0 && m_Grounded && jumpsends == 0 )
		{
			jumpsends++;
			counter = 0f;
			jumping = true;
		}
		animator.SetBool("isJumping", true);
		return counter;
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
		// If the player should jump...
		if (jumpdones < jumpsends && jumping == true)
		{
			jumpdones ++;
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x+wallGrabingDirection*wallGrabingJumpforce, 0);
			m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce * m_Rigidbody2D.mass, ForceMode2D.Impulse);

		}
		if(jumping)
        {
            if(jumpStop && Vector2.Dot(m_Rigidbody2D.velocity, Vector2.up) > 0)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, -counterJumpForce) * m_Rigidbody2D.mass);
            }
			if(Mathf.Abs(m_Rigidbody2D.velocity.y) <  0.15f && apexModifierTimeCount > 0)
			{

				apexModifierTimeCount -= Time.fixedDeltaTime;
				apexModifierCurrent = apexModifier;
				m_Rigidbody2D.gravityScale = 0;
				m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, 0,1);
			}
			else
			{
				apexModifierTimeCount = apexModifierTime;
				m_Rigidbody2D.gravityScale = 3;
			}
        }
		else
		{
			apexModifierCurrent = 1;
			apexModifierTimeCount = apexModifierTime;
			m_Rigidbody2D.gravityScale = 3;
		}
	}
    private static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
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
}

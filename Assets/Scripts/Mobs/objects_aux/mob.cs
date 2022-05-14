using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Mob : MonoBehaviourPunCallbacks
{
    //Declaration of variables that are from player.
    public Transform myTransform;
    public Animator myAnimator;
    public SpriteRenderer mySpriteRenderer;
    public Rigidbody2D myRigidbody;
    public PhotonView Pv;
	PlayerManager playerManager;


    //Mob has a StateMachine that changes the state always has 1 state active. Initializes as idleState
    public StateMachine myStateMachine;
    public int actualState;
    InputPlayer inputPlayer;
    [SerializeField] public CollisionUpdates collisionCheck;

    [Header("Move")]
    public float horizontalMove = 0f;
    [SerializeField] protected float moveSpeed = 40f;
    public Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] public  float m_MovementSmoothing = .05f;	// How much to smooth out the movement

	[Header("Jump")]
	[SerializeField] public int amountOfJumps = 1;
	[SerializeField] public float counterJumpForce = 40f;
    [SerializeField] public float jumpHeight = 10f;
	[SerializeField] public float allowedTimeInAir = 0.1f;
    public float jumpBufferTime = 0.5f;
	public float jumpBufferCounter = 0f;
	[SerializeField] public float apexModifier = 2f;
	[SerializeField] public float apexModifierTime = 0.3f;
	public float apexModifierCurrent = 1f;
	public float apexModifierTimeCount = 0.3f;
	public float timeInAir = 0;
	public bool jumpStop = false;
 	public float m_JumpForce = 5f;						// Amount of force added when the player jumps.
	public int jumpdones = 0;
	public int jumpsends = 0;
    public bool jumping = false;//boorame?

	[Header("CornerCorrection")]
	[SerializeField] public float offsetOut = 0.27f;
	[SerializeField] public float offsetIn = 0.15f;
	[SerializeField] public float _topRayCastLenght = 0.5f;
	[SerializeField] public float _topRayCastLenghtB = 0.5f;
	[SerializeField] public float offsetOutB = 0.27f;
	[SerializeField] public float offsetInB = 0.15f;
	[SerializeField] public float distanceFromMidle = 0.5f;
	[SerializeField] public bool m_Grounded;							// A position marking where to check if the player is grounded.

	[Header("CollisionDetection")]
    [SerializeField] public LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] public Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    [SerializeField] public LayerMask m_whatIsDeath;
    public float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	[SerializeField] public float _groundRayCastLenght = 0.25f;//move variable ?
	[SerializeField] public float offset = 0.23f;
	[SerializeField] public Transform m_CeilingCheck;							// A position marking where to check for ceilings

    [Header("Attack")]
    public bool attacking = false;
    [SerializeField] public float attackRate = 2f;
	[SerializeField] public float nextAttackTime = 0f;
	[SerializeField] public int maxHealth = 100;
	public int currentHealth = 0;
	[SerializeField] public HealthBar healthBar;
	[SerializeField] GameObject ui;
	public int attackDamage = 10;
	public bool friendlyFire = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask playerLayers;

    void Awake()
    {
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        Pv = GetComponent<PhotonView>();
		playerManager = PhotonView.Find((int)Pv.InstantiationData[0]).GetComponent<PlayerManager>();

        myStateMachine = new StateMachine();
        inputPlayer = new InputPlayer();
        //collisionCheck = new CollisionUpdates();
        currentHealth = maxHealth;
        myStateMachine.initializeStates();
        if(Pv.IsMine)
			healthBar.SetMaxHealth(maxHealth);
		else
			Destroy(ui);
    }


    // Update is called once per frame'
    void Update()
    {
        if(!Pv.IsMine)
            return;
        myStateMachine.myDictionary[actualState].UpdateState(this);
        Fliping();
        inputPlayer.InputChecks(this);//ver mejor manera
		collisionCheck.CollisionCheck(this);
    }

    void FixedUpdate()
    {
        if(!Pv.IsMine)
            return;
        myStateMachine.myDictionary[actualState].FixedUpdateState(this);
    }
    public void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
        Gizmos.DrawLine(m_GroundCheck.position, m_GroundCheck.position+Vector3.down*_groundRayCastLenght);
		Gizmos.DrawLine(m_GroundCheck.position-new Vector3(offset,0,0), m_GroundCheck.position+Vector3.down*_groundRayCastLenght-new Vector3(offset,0,0));
		Gizmos.DrawLine(m_GroundCheck.position+new Vector3(offset,0,0), m_GroundCheck.position+Vector3.down*_groundRayCastLenght+new Vector3(offset,0,0));
    }
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private void Fliping()
    {
        // If the input is moving the player right and the player is facing left...
		if (horizontalMove > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (horizontalMove < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
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
	public void TakeDamage(int damage)
	{
		Pv.RPC("RPC_TakeDamage", RpcTarget.AllBuffered, damage);//nombre funcion, a quien se lo paso, valor
	}
    [PunRPC]
	protected void RPC_TakeDamage(int damage)//try to move me
	{
		currentHealth -= damage;
		if(Pv.IsMine)
			healthBar.SetHealth(currentHealth);
		//animacion de lastimado
		if(currentHealth <= 0)
		{
            Debug.Log("die");
			//Die();
		}
        return;
	}
}

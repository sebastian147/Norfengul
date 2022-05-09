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
    protected PhotonView Pv;
	PlayerManager playerManager;


    //Mob has a StateMachine that changes the state always has 1 state active. Initializes as idleState
    public StateMachine myStateMachine;
    public int actualState;
    InputPlayer inputPlayer;
    [SerializeField] public CollisionUpdates collisionCheck;

    [Header("Move")]
    public float horizontalMove = 0f;
    protected float moveSpeed = 40f;
    public Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] public  float m_MovementSmoothing = .05f;	// How much to smooth out the movement

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
        myStateMachine.initializeStates();
    }


    // Update is called once per frame'
    void Update()
    {
        if(!Pv.IsMine)
            return;
        myStateMachine.myDictionary[actualState].UpdateState(this);
        Fliping();
        inputPlayer.InputChecks(this);//ver mejor manera
    }

    void FixedUpdate()
    {
        if(!Pv.IsMine)
            return;
        myStateMachine.myDictionary[actualState].FixedUpdateState(this);
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


}

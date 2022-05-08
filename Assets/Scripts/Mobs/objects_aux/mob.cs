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
    protected float horizontalMove = 0f;
    protected float moveSpeed = 40f;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement

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
    

    void FixedUpdate()
    {
        if(!Pv.IsMine)
        {
            return;
        }
        myStateMachine.myDictionary[actualState].UpdateState(this);
        move(this);
        inputPlayer.InputChecks();//ver mejor manera
        collisionCheck.CollisionCheck();
    }

    public virtual int move(Mob myMob)//mover de aca
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
		Vector3 targetVelocity = new Vector2(horizontalMove * 10f/*apexModifierCurrent*/, myMob.myRigidbody.velocity.y);
		// And then smoothing it out and applying it to the character
		myMob.myRigidbody.velocity = Vector3.SmoothDamp(myMob.myRigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);        return 1;
        return 1;
    }


}

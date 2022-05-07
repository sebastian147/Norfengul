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
    public MobBaseState actualState;
    InputPlayer inputPlayer;
    [SerializeField] public CollisionUpdates collisionCheck;
    

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

        actualState = myStateMachine.initializeStates();
    }


    // Update is called once per frame'
    

    void FixedUpdate()
    {
        if(!Pv.IsMine)
        {
            return;
        }
        mobMove();
        inputPlayer.InputChecks();//ver mejor manera
        collisionCheck.CollisionCheck();
    }

    void mobMove()
    {
        myStateMachine.changeState(actualState.move(this));
    }


}

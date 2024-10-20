using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
//TODO CHECK NOT USED VARIABLES
public class Mob : MonoBehaviourPunCallbacks
{
        //Declaration of variables that are from player.
        public Transform myTransform;
        public Animator myAnimator;
        //public SpriteRenderer mySpriteRenderer;
        public Rigidbody2D myRigidbody;
        public PhotonView Pv;
        PlayerManager playerManager;

        Timers t = new Timers();
        public bool m_FacingRight = true;

        //Mob has a StateMachine that changes the state always has 1 state active. Initializes as idleState
        public StateMachine myStateMachine;
        public int actualState;
        InputPlayer inputPlayer;
        [SerializeField] public CollisionUpdates collisionCheck;

        [Header("Move")]
        public float horizontalMove = 0f;
        [SerializeField] public float moveSpeed = 40f;
        public Vector3 m_Velocity = Vector3.zero;
        [Range(0, .3f)] [SerializeField] public  float m_MovementSmoothing = .05f;	// How much to smooth out the movement
        [SerializeField] public float runningSpeed = 40f;
        public bool running = false; 

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

        [Header("Wall Grabing")]
        public bool _inWallLeft = false;
        public bool _inWallRight = false;
        [SerializeField] public Transform m_WallCheck;
        [SerializeField] public float _wallRayCastLenght = 0.2f;
        public float wallGrabingJumpforce=0;
        public float wallGrabingDirection=0;
        [SerializeField] public float wallSlidingSpeed = 1;
        [SerializeField] public float timeInwallBuffer = 4f;
        [SerializeField] public float distanceFromGrabs = 0.5f;
        public bool wallGrabing = false;
        [SerializeField] public LayerMask m_WhatIsWall;
        public bool drop = false;

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
	public bool friendlyFire = false;
        public Transform attackPoint;
        public LayerMask enemyLayers;
        public LayerMask playerLayers;
        [SerializeField] public GameObject HitParticles; 
        [SerializeField]public GameObject damageNumber;
        public MeleWeaponLogic arma;
        public float knockBackTake;
        public float dir;

        [Header("victory")]
        public bool victory = false;
        
        [Header("dash")]
        public bool dashLeft = false;
        public bool dashRight = false;
        public float dashingPower = 24f;
        public float dashingTime = 0.2f;
        public float dashingCoolDown = 0;
        public float dashingCoolDownMax = 1f;
        public bool canDash = false;
        [SerializeField] public TrailRenderer tr;

        [Header("skin")]
        [SerializeField] public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary Barbas;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary Cuerpo;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary Escudos;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary Pelos;
        [SerializeField] public Renderer ShaderPelos;
        [SerializeField] public Renderer ShaderBarbas;
        [SerializeField] public Color color;
        [SerializeField] public GameObject nickName;

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
                //m_WhatIsWall = m_WhatIsGround;//borrame

                if(Pv.IsMine)
                {
                        healthBar.SetMaxHealth(maxHealth);
                }
                else
                        Destroy(ui);
        }
        private void Start() {
    
        }


        // Update is called once per frame'
        void Update()
        {
                if(!Pv.IsMine)
                        return;
                myStateMachine.myDictionary[actualState].UpdateState(this);
                //Fliping();
                inputPlayer.InputChecks(this, arma);//ver mejor manera
                collisionCheck.CollisionCheck(this);
        }

        void FixedUpdate()
        {
                if(!Pv.IsMine)
                        return;
                myStateMachine.myDictionary[actualState].FixedUpdateState(this);
                canDash = t.timePassFixed(ref dashingCoolDown, dashingCoolDownMax, !canDash);
        }
        public void OnDrawGizmosSelected()
        {
                Gizmos.color = Color.red;
                //floor check
                Gizmos.DrawLine(m_GroundCheck.position, m_GroundCheck.position+Vector3.down*_groundRayCastLenght);
                Gizmos.DrawLine(m_GroundCheck.position-new Vector3(offset,0,0), m_GroundCheck.position+Vector3.down*_groundRayCastLenght-new Vector3(offset,0,0));
                Gizmos.DrawLine(m_GroundCheck.position+new Vector3(offset,0,0), m_GroundCheck.position+Vector3.down*_groundRayCastLenght+new Vector3(offset,0,0));

                //wall check
                Gizmos.DrawLine(m_WallCheck.position, m_WallCheck.position+Vector3.right*_wallRayCastLenght);

                //top jump correction
                Gizmos.DrawLine(m_CeilingCheck.position+new Vector3(offsetIn,0,0), m_CeilingCheck.position+Vector3.up*_topRayCastLenght+new Vector3(offsetIn,0,0));
                Gizmos.DrawLine(m_CeilingCheck.position+new Vector3(offsetOut,0,0), m_CeilingCheck.position+Vector3.up*_topRayCastLenght+new Vector3(offsetOut,0,0));
                Gizmos.DrawLine(m_CeilingCheck.position-new Vector3(offsetIn,0,0), m_CeilingCheck.position+Vector3.up*_topRayCastLenght-new Vector3(offsetIn,0,0));
                Gizmos.DrawLine(m_CeilingCheck.position-new Vector3(offsetOut,0,0), m_CeilingCheck.position+Vector3.up*_topRayCastLenght-new Vector3(offsetOut,0,0));

                //bottom correction
                Gizmos.DrawLine(m_GroundCheck.position+new Vector3(distanceFromMidle,offsetOutB,0), m_GroundCheck.position+Vector3.right*_topRayCastLenghtB+new Vector3(0,offsetOutB,0));
                Gizmos.DrawLine(m_GroundCheck.position+new Vector3(distanceFromMidle,offsetInB,0), m_GroundCheck.position+Vector3.right*_topRayCastLenghtB+new Vector3(0,offsetInB,0));
                Gizmos.DrawLine(m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetInB,0), m_GroundCheck.position+Vector3.left*_topRayCastLenghtB-new Vector3(0,-offsetInB,0));
                Gizmos.DrawLine(m_GroundCheck.position-new Vector3(distanceFromMidle,-offsetOutB,0), m_GroundCheck.position+Vector3.left*_topRayCastLenghtB-new Vector3(0,-offsetOutB,0));

                //Wall grabing
                Gizmos.DrawLine(m_WallCheck.position, m_WallCheck.position+Vector3.right*_wallRayCastLenght);
                Gizmos.DrawLine(m_WallCheck.position+new Vector3(0, -distanceFromGrabs, 0), m_WallCheck.position+Vector3.right*_wallRayCastLenght+new Vector3(0,-distanceFromGrabs,0));
                Gizmos.DrawLine(m_WallCheck.position, m_WallCheck.position+Vector3.left*_wallRayCastLenght);
                Gizmos.DrawLine(m_WallCheck.position+new Vector3(0, -distanceFromGrabs, 0), m_WallCheck.position+Vector3.left*_wallRayCastLenght+new Vector3(0,-distanceFromGrabs,0));

                //attack
                Gizmos.DrawWireSphere(attackPoint.position, arma.Armas.damageArea);

                
        }

	public void TakeDamage(int damage, bool rightAttack, int knockBack)
	{
		Pv.RPC("RPC_TakeDamage", RpcTarget.AllBuffered, damage, rightAttack, knockBack);//nombre funcion, a quien se lo paso, valor
	}
        [PunRPC]
	protected void RPC_TakeDamage(int damage, bool rightAttack, int knockBack)//try to move me
	{

                if(currentHealth > 0)//bug de muerte en respawn
                {       
                        currentHealth -= damage;
                        GameObject instance = Instantiate(damageNumber, myTransform.position, myTransform.rotation);
                        instance.transform.Find("Damage").GetComponent<TextMesh>().text = damage.ToString();
                        if(rightAttack)
                        {
                                dir = 1;
                        }
                        else if(!rightAttack)
                        {
                                dir = -1;
                        }
                        knockBackTake=knockBack;
                        //this.dir = dir;
                        //myRigidbody.AddForce(new Vector2(dir,1) * myRigidbody.mass * knockBack, ForceMode2D.Impulse);
                        if(Pv.IsMine)
                                healthBar.SetHealth(currentHealth);
                        //animacion de lastimado
                        if(currentHealth <= 0)
                        {
                                Die();
                                return;
                        }
                        actualState = myStateMachine.changeState(myStates.Damage,this);

                }
	}
        //moveme de aca
        public void Die()
        {
                currentHealth = 0;
                healthBar.SetHealth(currentHealth);
                myAnimator.SetBool("IsDead", true);
		gameObject.GetComponent<Dissolve>().TriggerDissolve();
                Barbas.GetComponent<Dissolve>().TriggerDissolve();
                Pelos.GetComponent<Dissolve>().TriggerDissolve();
                //GetComponent<BoxCollider2D>().enabled = false;
		//GetComponent<CircleCollider2D>().enabled = false;
		//m_Rigidbody2D.isKinematic = true;
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y); 
		this.enabled = false;
		if(Pv.IsMine)
			playerManager.Die();
        }
        public void changeWeapon(string weaponName)
        {
                Pv.RPC("RPC_changeWeapon", RpcTarget.AllBuffered, weaponName);//nombre funcion, a quien se lo paso, valor
        }
        [PunRPC]
        public void RPC_changeWeapon(string weaponName)
        {
                arma.Armas = Resources.Load<Weapon>(weaponName);
        }
        public void changeLayer(string layer)
        {
                Pv.RPC("RPC_changeLayer", RpcTarget.AllBuffered, layer);//nombre funcion, a quien se lo paso, valor
        }
        [PunRPC]
        public void RPC_changeLayer(string layer)
        {
                this.gameObject.layer = LayerMask.NameToLayer(layer);
        }
        public void changeSkin(string Arma,string Barbas,string Cuerpo,string Escudos,string Pelos, float R, float G, float B, string name)
        {
                Pv.RPC("RPC_changeSkin", RpcTarget.AllBuffered,  Arma, Barbas, Cuerpo, Escudos, Pelos, R, G, B, name);//nombre funcion, a quien se lo paso, valor
        }
        [PunRPC]
        public void RPC_changeSkin(string Arma,string Barbas,string Cuerpo,string Escudos,string Pelos, float R, float G, float B, string name)
        {
                Color color = new Color(
                        R,
                        G,
                        B
                );
                this.ShaderPelos.material.SetColor("_Color1", color);
                this.ShaderBarbas.material.SetColor("_Color1", color);
                this.Barbas.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>(Barbas);
                this.Cuerpo.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>(Cuerpo);
                this.Escudos.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>(Escudos);
                this.Pelos.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>(Pelos);
                this.arma.Armas = Resources.Load<Weapon>(Arma);
                mySpriteResolver.ResolveSpriteToSpriteRenderer();
                nickName.GetComponent<TextMesh>().text =  name;


        }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public GameObject introMessage;
    private float time;
    private State state = State.INTRO;
    private float introTime = 3.0f;
    [Header("Pick the Ground Layer")]
    [SerializeField]
    private LayerMask whatIsGround;

    [Header("Pick the Wall layer")]
    [SerializeField]
    private LayerMask wallLayer;
    [Header("Movement speed")]
    [SerializeField]
    private int speed;

    [Header("Amount of hits to kill")]
    [SerializeField]
    private int hp = 10;

    public GameObject deathAnimation;
    public bool isQuitting = false;

    private Collider2D[] colliders;
    private Collider2D[] wall;

    Rigidbody2D enemy_Rigidbody;
    private bool moveLeft = true;
    private bool facingLeft = true;
    private Transform groundCheck;
    private bool isFlying = false;
    private float maxHeight;
    private Animator bossAnim;
    public GameObject flyAnimation;
    public GameObject spawnPoint;
    public GameObject projectile;
    private bool isSpinning = false;
    private Vector3 startpos;
    private float damageDelay = 0.07f;
    private bool spawningProjectiles = false;
    private Dude2D player;
   private bool isIdle = false;
    private bool grounded = true;
    private Camera main;
    private SpriteRenderer bossSpriteRenderer;

    public GameObject arenaWall;

    // Use this for initialization
    void Start()
    {
        main = FindObjectOfType<Camera>();

        enemy_Rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("enemyGroundCheck");
        maxHeight = transform.position.y + 6.5f;
        bossAnim = GetComponent<Animator>();
        player = FindObjectOfType<Dude2D>();
        startpos = transform.position;
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    IEnumerator Intro()
    {
        time = 0.0f;
        introMessage.SetActive(true);
        yield return new WaitForSeconds(3);
        introMessage.SetActive(false);
        
        //state = State.RUNNING;F
        state = State.FLYING_SPIKES;
    }

    IEnumerator toRunning()
    {
        time = 0.0f;
        isIdle = false;
        isFlying = false;
        isSpinning = false;
        enemy_Rigidbody.velocity = new Vector2(0, 0);
       // enemy_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        transform.position = startpos;
        yield return new WaitForSeconds(1);
        state = State.RUNNING;
        
    }
    public int getHp()
    {
        return hp;
    }
    public void removeHp()
    {
        if (hp <= 1)
        {
            testlol xd = FindObjectOfType<testlol>();
            if (xd != null) Destroy(xd);
         
        }
        hp--;
    }

    
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
   

    }
    void JumpAttack()
    {

    }
    void LookForPlayer()
    {
        isIdle = true;
        print("looking for player...");
        isFlying = false;
        isSpinning = false;
        enemy_Rigidbody.velocity = new Vector2(0, 0);
        //enemy_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        transform.position = startpos;
       
        StartCoroutine(waitasecond(0.2f, 1));



    }
    private IEnumerator waitasecond(float i, int k)
    {

            

            yield return new WaitForSeconds(i);
            int target = 4;
            k += 1;
            if (player.transform.position.x < transform.position.x) target += 1;

            Flip();
            if (k < target)
            {
                print("k : " + k);
                yield return waitasecond(i, k);
            }
            else
            {
                print("det virker altså");
                StartCoroutine(toRunning());
            }
        
       

    }
    private IEnumerator Look()
    {
    
        Flip();
        yield return new WaitForSeconds(0.2f);
        Flip();
        yield return new WaitForSeconds(0.2f);
        Flip();
        yield return new WaitForSeconds(0.2f);
        Flip();
        yield return new WaitForSeconds(0.2f);
           
        if (player.transform.position.x < transform.position.x) Flip();

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(toRunning());

    }

    void flyWithSpikes()
    {

        if (!isFlying)
        {
            isIdle = false;
            flyAnimation.SetActive(true);
            
            if (transform.position.y >= maxHeight)
            {
                
                enemy_Rigidbody.velocity = new Vector2(0, 0);
                if (!isSpinning)
                {
                    StartCoroutine(Rotate(8));
                    isSpinning = true;
                    grounded = true;
                }               
            }

            else 
            {
                grounded = false;
                enemy_Rigidbody.AddForce(new Vector2(0, 4));
            //    enemy_Rigidbody.constraints = RigidbodyConstraints2D.None;
             //   isFlying = false;
             //   flyAnimation.SetActive(false);
            }
        }
        else
        {
            enemy_Rigidbody.AddForce(new Vector2(0, -8));
            flyAnimation.SetActive(false);
            if (transform.position.y <= maxHeight - 6.5f)
            {
                grounded = true;
         //       transform.position = startpos;
    //            enemy_Rigidbody.velocity = new Vector2(0, 0);
                //StartCoroutine(toRunning());
                state = State.IDLE;
            }
        }
       
    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private IEnumerator Rotate(float duration)
    {
    
    //    StartCoroutine(FireProjectile());
        Quaternion startRot = transform.rotation;
        float t = 0.0f;
        float t2 = 0.0f;
        while (t < duration)
        {
            t2 += Time.deltaTime;
            if (t2 > 0.045f)
            {
                FireProjectile();
                t2 = 0.0f;
            }
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / 2f * 360f, new Vector3(0, 0, 1)); //or transform.right if you want it to be locally based
            yield return null;
            
        }
        isFlying = true;
        grounded = !grounded;
        print("done");
        transform.rotation = startRot;
        



    }
    private void FireProjectile()
    {
        Quaternion newRot = transform.rotation;
        newRot = transform.rotation * Quaternion.AngleAxis(-180, new Vector3(0, 0, 1));
        Instantiate(projectile, spawnPoint.transform.position, newRot);
        
     
        // if (spawningProjectiles) StartCoroutine(FireProjectile());

    }

    private void OnDestroy()
    {
        if (deathAnimation != null && !isQuitting)
        {
            deathAnimation.transform.position = transform.position;
            Instantiate(deathAnimation);      
            arenaWall.SetActive(false);
         
        }

    }

    private void FixedUpdate()
    {

        colliders = Physics2D.OverlapCircleAll(groundCheck.position, .2f, whatIsGround);
        wall = Physics2D.OverlapCircleAll(groundCheck.position, .2f, LayerMask.GetMask("Wall"));


        //if (grounded)
        //{
        //    enemy_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        //}
        //else
        //{
        //    enemy_Rigidbody.constraints = RigidbodyConstraints2D.None;
        //}

        time += Time.deltaTime;

        if (time > 15f && state != State.INTRO)
        {

            time = 0.0f;
            startpos = transform.position;
            state = State.FLYING_SPIKES;


        }
        switch (state)
        {
            case State.INTRO:
                //do intro anim
                //RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, LayerMask.GetMask("Player"));
                //print(ray.collider.name);
                //if (ray.collider != null)
                //{
                //    Intro();
                //}


                if (player.transform.position.x > (transform.position.x - 20) && bossSpriteRenderer.isVisible && player.transform.position.x < transform.position.x)
                {
                //    main.orthographicSize = 15;
                  //  main.transform.Find("backgrounds").transform.localScale = new Vector3(2, 2, 2);
                    StartCoroutine(Intro());
                    arenaWall.SetActive(true);
                }
                //if (time >= introTime)
                //{
                //    state = State.IDLE;
                //    time -= introTime;
                //}
                break;
            case State.IDLE:
                if (!isIdle)
                {

                    LookForPlayer();
                }

                break;
            case State.ATTACK_1:
                //do attack one
                break;
            case State.ATTACK_2:
                //do attack two
                break;
            case State.JUMP_ATTACK:
                //jump towards player
                JumpAttack();
                break;
            case State.FRENZY:
                //go berserk
                break;
            case State.DEATH:
                //die lmfao
                break;
            case State.FLYING_SPIKES:
                flyWithSpikes();
                break;
            case State.RUNNING:
                Running();
                break;
        }



    }


    void Flip()
    {
        facingLeft = !facingLeft;
        moveLeft = !moveLeft;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;


        speed *= -1;
    }
    void Running()
    {
        
        enemy_Rigidbody.AddForce(Vector2.left * speed);



        if (colliders.Length == 0 || wall.Length != 0) Flip();
    }

    // Update is called once per frame


    public enum State
    {
        INTRO,
        IDLE,
        RUNNING,
        ATTACK_1,
        ATTACK_2,
        JUMP_ATTACK,
        FLYING_SPIKES,
        FRENZY,
        DEATH
    }


}

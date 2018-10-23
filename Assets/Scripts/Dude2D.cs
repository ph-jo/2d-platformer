using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude2D : MonoBehaviour
{
    [Header("Jump and Run")]
    [SerializeField]
    private float runSpeed = 5f;
    [SerializeField] private float jumpForce = 2f;
    [Header("Can Dude move in the air?")]
    [SerializeField]
    private bool airControl = false;
    [Header("Can Dude sprint using shift?")]
    [SerializeField]
    private bool sprint = false;
    private bool isSprinting = false;
    [Header("Pick the Ground Layer")]
    [SerializeField]
    private LayerMask whatIsGround;

    const float GROUND_CHECK_RADIUS = .2f; // Radius of the overlap circle to determine if grounded
    private Transform groundCheck;         // A position marking where to check if the player is grounded.
    private bool grounded;                 // Whether or not the player is grounded.
    private GameController controller;
    private CheckpointController checkController;
    private Animator animator;             // Reference to the player's animator component.
    private Rigidbody2D rb2D;              // Reference to the player's Rigidbody2D component.
    private bool facingRight = true;       // For determining which way the player is currently facing.
    private bool jumping;                  // Is the player currently jumping?
    private Animator parentAnim;
    private bool doubleJump = false;
    private float startSpeed;

    private void Start()
    {
        parentAnim = gameObject.transform.parent.GetComponent<Animator>();
        checkController = FindObjectOfType<CheckpointController>();
        controller = FindObjectOfType<GameController>();

        try{
            
            if(checkController.getCheckpointPos().x != 0.0f) setSpawnPosition(checkController.getCheckpointPos());
        }catch(NullReferenceException e)
        {
            //LOL
        }
        startSpeed = runSpeed;


    }
    public void setSpawnPosition(Vector3 spawnPos)
    {
        transform.position = spawnPos;
    }
    private void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

       
    }

    private void Update()
    {

        if (!jumping)
        {
            jumping = Input.GetButtonDown("Jump");
        }
        if (sprint)
        {
            isSprinting = Input.GetKey(KeyCode.LeftShift);

        }
    }

    private void FixedUpdate()

    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Move(horizontal, jumping);
        jumping = false;
        if(horizontal != 0 || !grounded)
        {
            parentAnim.SetBool("idle", false);
        }
        else
        {
            parentAnim.SetBool("idle", true);
        }
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground.
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GROUND_CHECK_RADIUS, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            grounded = true;
        }
        //animator.SetBool("Ground", grounded);      
        animator.SetBool("jumping", !grounded);
       
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript es = collision.GetComponentInParent<EnemyScript>();
        BossScript bs = collision.GetComponentInParent<BossScript>();
        if(collision.transform.tag == "IrregularEnemy")
        {
            controller.youDiedLOL();
        }
        if (collision.transform.tag == "enemy" || collision.transform.tag == "boss")
            
        {
            
        
            // Debug.Log(collision.ToString());
            //  Debug.Log("y velocity: " + rb2D.velocity.y);
            if (grounded || rb2D.velocity.y > 0 && rb2D.velocity.y != 10)
            {

                controller.youDiedLOL();
               
            }
            else {
                if(es != null && es.getHp() >= 1)
                { 
                        es.removeHp();
                    //Destroy(collision.
                    print("removing 1 hp lol");

                }else if (bs != null && bs.getHp() >= 1)
                {
                    bs.removeHp();
                    print("Removing 1 hp from boss lol");
                }
              
                else
                {
                    Destroy(collision.transform.parent.gameObject);
                  

                }
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                rb2D.AddForce(new Vector2(0f, 10), ForceMode2D.Impulse);
            }
        }

        else if(collision.transform.tag == "DoubleJump")
        {
            doubleJump = true;
            StartCoroutine(Wait(collision.gameObject));
            
        }
        
    }

    private IEnumerator Wait(GameObject go)
    { 
        go.SetActive(false);
        yield return new WaitForSeconds(2);
        go.SetActive(true);
    }
   
    public void Move(float move, bool jump)
    {
        if (isSprinting)
        {
            runSpeed = startSpeed * 1.5f;
        }
        else
        {
            runSpeed = startSpeed;
        }
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        // animator.SetFloat("Speed", Mathf.Abs(move));
        if (move != 0)
        {
            animator.SetBool("running", true);
            
        }
        else
        { 
            animator.SetBool("running", false);
           
        }
        // Move the character
        if (grounded || airControl)
        {
            rb2D.velocity = new Vector2(move * runSpeed, rb2D.velocity.y);
        }

        // Flip the character if his face is not in the direction of movement
        if (move > 0 && !facingRight || (move < 0 && facingRight))
        {
            Flip();
        }
        
        // If on the ground at player want to jump
        if (jump)
        {
            if (grounded || doubleJump)
            {
                // Add a vertical force to the player.
                grounded = false;
                //animator.SetBool("Ground", false);
                animator.SetBool("jumping", true);
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
                //  rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump = false;
            }
            
        }
        //if (jumpOnEnemy)
       // {
         //   grounded = false;
           // rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            //jumpOnEnemy = false;
       // }
    }

    private void Flip()
    {
        //Debug.Log("flipping");
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Flip the player transform
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}


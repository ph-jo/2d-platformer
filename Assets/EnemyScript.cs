using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    [Header("Pick the Ground Layer")]
    [SerializeField]
    private LayerMask whatIsGround;
    
    [Header("Pick the Wall layer")]
    [SerializeField]
    private LayerMask wall;
    [Header("Movement speed")]
    [SerializeField]
    private int speed;

    [Header("Amount of hits to kill")]
    [SerializeField]
    private int hp = 1;

    public GameObject deathAnimation;
    public bool isQuitting = false;
    
    


    Rigidbody2D enemy_Rigidbody;
    private bool moveLeft = true;
    private bool facingLeft = true;
    private Transform groundCheck;
    // Use this for initialization
    void Start () {
        enemy_Rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("enemyGroundCheck");

    }
    public int getHp()
    {
        return hp;
    }
    public void removeHp()
    {
        hp--;
    }
    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update () {
        
       
        

    }
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }



    private void OnDestroy()
    {
        if(deathAnimation != null && !isQuitting)
        {
            deathAnimation.transform.position = transform.position;
            Instantiate(deathAnimation);
        }
        
    }

    private void FixedUpdate()
    {
        //if (moveLeft)
        //{
        //    enemy_Rigidbody.AddForce(-Vector2.right * speed *Time.deltaTime);
        //}
        //else
        //{
        //    enemy_Rigidbody.AddForce(Vector2.right * speed * Time.deltaTime);
        //}
        enemy_Rigidbody.AddForce(-Vector2.right * speed * Time.deltaTime);
        
       // Collider2D colliders = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .2f, whatIsGround);
        /*  for(int i = 0; i < colliders.Length; i++)
          {
              Debug.Log(colliders[i] + "  #" + i);
          }*/
        Collider2D[] wall = Physics2D.OverlapCircleAll(groundCheck.position, .2f, LayerMask.GetMask("Wall"));


        if (colliders.Length == 0 || wall.Length != 0) Flip();
      

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
}

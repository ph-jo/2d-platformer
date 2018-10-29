using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterScript : MonoBehaviour {

    private State state = State.IDLE;
    private Rigidbody2D rb2d;
    private Animator anim;
    private float time = 0.0f;
    private GameController gc;
    public GameObject endWall;
    public GameObject endPos;
    private bool isAttacking = false;
    private Vector3 startPos;
    private bool running = false;
    private bool started = false;
    private bool isdying = false;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gc = FindObjectOfType<GameController>();
	}

    

    // Update is called once per frame
    void Update () {
		
	}

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 1f && !started)
        {
            started = true;
            state = State.RUNNING;
            anim.SetBool("Running", true);
        }
        switch (state)
        {
            case State.IDLE:
                break;
            case State.RUNNING:
                if(transform.position.x >= endPos.transform.position.x)
                {
                    rb2d.velocity = new Vector2(0, 0);
                    state = State.ATTACK;
                }else
                {
                    rb2d.AddForce(new Vector2(1.1f, 0));
                }
                break;
            case State.ATTACK:
                //print("in attack");
                //if (!isAttacking)
                //{
                //    isAttacking = true;
                //    StartCoroutine(Attack());
                //}
                Vector3 attackPos = transform.position;
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("lobsterDizzy") && !isdying)
                {
                    anim.speed = 1f;
                    isdying = true;
                    attackPos = transform.position;
                    attackPos.y -= 3.27f;
                    transform.position = attackPos;
                    endWall.SetActive(false);
                }
                
                if (!isAttacking)
                {
                    anim.speed = 0.8f;
                    attackPos.y += 3.27f;
                    transform.position = attackPos;
                    isAttacking = true;
                }
                 anim.SetBool("Running", false);
                anim.SetBool("End", true);
               
                
               
              
                
                
                break;
        }
        
    }

    private IEnumerator Attack()
    {
        float t = 0.0f;
        print("in attack");
        anim.SetBool("Running", false);
        anim.SetBool("End", true);
        Vector3 attackPos = transform.position;
        attackPos.y += 3.27f;
        transform.position = attackPos;
        anim.speed = 0.8f;
        while (t < 10f)
        {
            t += Time.deltaTime;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("lobsterDizzy"))
            {
                attackPos = transform.position;
                attackPos.y -= 3.27f;
                transform.position = attackPos;
                endWall.SetActive(false);
            }
            yield return null;
        }
        
        
    }
    private IEnumerator Running()
    {
        while (transform.position.x <= endPos.transform.position.x)
        {
            rb2d.AddForce(new Vector2(0.5f, 0));

            yield return null;
        }
        print("END OF BOSS HEJ HEJ");
        rb2d.velocity = new Vector2(0, 0);
        state = State.ATTACK;
    }
    public enum State
    {
        IDLE,
        RUNNING,
        ATTACK,
        BLINKSTRIKE
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") gc.youDiedLOL();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireBossScript : MonoBehaviour {

    public GameObject deathAnimation;
    public GameObject safeSpot;
    public GameObject arenaWalls;
    public GameObject bat;
    public GameObject batSpawner;
    public GameObject garlicBread;
    public GameObject doubleJumps;
    public GameObject maxX;
    public GameObject tospawn;
    public bool specialEnemy = false;
    private int health = 2;
    private State state = State.IDLE;
    private float time = 0.0f;
    private Animator anim;
    private GameController gameController;
    private Dude2D dude;
    private bool doingSpecial = false;
    private bool doneIntro = false;
    private int attackCount = 0;
    private bool isAttacking = false;
    private bool active = false;
    private bool flipped = false;
    private BoxCollider2D coll;

    private CapsuleCollider2D coll2;
	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
        dude = FindObjectOfType<Dude2D>();
        anim = GetComponent<Animator>();
	    coll = GetComponent<BoxCollider2D>();
	    coll2 = GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate()
    {
        active = true;
    }
    private void FixedUpdate()
    {
        if (specialEnemy) state = State.ATTACK;
        time += Time.deltaTime;

        if(active && state == State.IDLE && !specialEnemy)
        {
       
            state = State.INTRO;
            if (!doneIntro)
            {
                arenaWalls.SetActive(true);
                doneIntro = true;
            }


        }


        switch (state)
        {
            case State.INTRO:
                IntroBehaviour();
                break;
            case State.SPECIAL:
                BatParade();
                break;
            case State.ATTACK:
                if (dude.transform.position.x >= transform.position.x)
                {
                    if (!flipped) Flip();
                }
                else
                {
                    if (flipped) Flip();
                }

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("appear"))
                {
                    coll.enabled = false;
                }
                else
                {
                    coll.enabled = true;
                }

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                {
                    coll2.enabled = true;
                }
                else
                {
                    coll2.enabled = false;
                }
                TpAttack();
                break;
            default:
                break;
        }
    }

    void IntroBehaviour()
    {
        anim.SetBool("IsVanishing", true);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("appear"))
        {
            doubleJumps.SetActive(true);
            Vector3 newPos = safeSpot.transform.position;
            transform.position = newPos;
            state = State.SPECIAL;
        }

    }
    void BatParade()
    {

        if (!doingSpecial)
        {
            
            doingSpecial = true;
            time = 0.0f;
            StartCoroutine(Special());
        }

    }

    //private IEnumerator SpecialToAttack()
    //{
        
    //}

    private IEnumerator Special()
    {
        anim.SetBool("IsVanishing", false);
        anim.SetBool("SpecialAttack", true);
        float timeBetweenBats = 0.18f;
        float t2 = 0.0f;
        int count = 0;
        int randomBuff = Mathf.CeilToInt(Random.Range(0f, 50f));
       
        while (time < 10f)
        {
            
            t2 += Time.deltaTime;
            if(t2 > timeBetweenBats && time > 0.6f)
              {
                count++;

                if (count == randomBuff)
                {
                    Instantiate(garlicBread, batSpawner.transform);
                }
                else
                {
                    SpawnBat();
                }
                t2 = 0.0f;
            }
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        doubleJumps.SetActive(false);
        doingSpecial = false;
        anim.SetBool("SpecialAttack", false);
        state = State.ATTACK;
    }

    void TpAttack()
    {
        anim.SetBool("IsVanishing", true);
        if (!isAttacking && anim.GetCurrentAnimatorStateInfo(0).IsName("appear"))
        {
            StartCoroutine(SpawnClones());
            StartCoroutine(Attack());
            isAttacking = true;
        }

    }

    private void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        flipped = !flipped;
    }
    private IEnumerator SpawnClones()
    {
        GameObject bossRoom = GameObject.Find("BossRoom");
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        Instantiate(tospawn, bossRoom.transform);
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        Instantiate(tospawn, bossRoom.transform);
    }
    private IEnumerator Attack()
    {
        
        anim.SetBool("SpecialAttack", false);



        float t = 0.0f;
        Vector3 tpPos = maxX.transform.position;
        anim.speed = 0.8f;
        bool shouldAttack = true;
        while (t < 10f)
        {

            t += Time.deltaTime;
            
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("appear") && shouldAttack)
            {


                
                anim.SetBool("IsVanishing", false);

                shouldAttack = false;
                attackCount++;
                
                tpPos.x += Mathf.CeilToInt(Random.Range(-12.5f, 12.5f));
                transform.position = tpPos;
                tpPos = maxX.transform.position;
                print("do the thing");
                yield return new WaitForSeconds(Random.Range(0.2f, 1.2f));
                anim.SetBool("IsVanishing", true);

            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("disappear"))
            {
                shouldAttack = true;

               
            }


               
                yield return null;
            
           
            
           
            //t += Time.deltaTime;
            //tpPos.y = -23.2f;
            //if (dude.transform.position.x > transform.position.x || dude.transform.position.x < minX.transform.position.x)
            //{
            //    //  -23.2
            //    Vector3 newScale = transform.localScale;
            //    newScale.x *= -1;
            //    transform.localScale = newScale;


            //    tpPos.x = dude.transform.position.x + 2;

            //}
            //else if (dude.transform.position.x < transform.position.x || dude.transform.position.x > maxX.transform.position.x)
            //{
            //    tpPos.x = dude.transform.position.x - 2;
            //}
            //transform.position = tpPos;
            //print(tpPos + "ttpos");
            //print(transform.position + "trans pos");
        }
        anim.speed = 1;
        if (specialEnemy) Destroy(gameObject);
        state = State.IDLE;
        isAttacking = false;
        if (flipped) Flip();



    }

    void SpawnBat()
    {
        Instantiate(bat, batSpawner.transform.position, batSpawner.transform.rotation);
    }
    public enum State
    {
        INTRO,
        IDLE,
        ATTACK,
        SPECIAL,
        DEATH
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameController.youDiedLOL();
        }
    }

    public void TakeDamage()
    {
        if (health <= 1)
        {
            arenaWalls.SetActive(false);
            doubleJumps.SetActive(false);
            Destroy(batSpawner.gameObject, 0.5f);
            Destroy(gameObject, 0.5f);
        }
        health--;
        print("boss took damage, health: " + health);
        
    }
    private void OnDestroy()
    {
        Instantiate(deathAnimation, transform.position, transform.rotation);
        arenaWalls.SetActive(false);
    }

    public void setSpecial()
    {

    }
}

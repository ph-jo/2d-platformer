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
    public GameObject minX;
    
    private int health = 3;
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
	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
        dude = FindObjectOfType<Dude2D>();
        anim = GetComponent<Animator>();
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
        time += Time.deltaTime;

        if(active && state == State.IDLE && !doneIntro)
        {
       
                state = State.INTRO;
                arenaWalls.SetActive(true);
                doneIntro = true;
            
            
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
        if(time > 10f)
        {

           // StartCoroutine(SpecialToAttack());
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
        doubleJumps.SetActive(false);
        doingSpecial = false;
        anim.SetBool("SpecialAttack", false);
    }

    void TpAttack()
    {
        if (!isAttacking && attackCount == 0)
        {
            StartCoroutine(Attack());
            isAttacking = true;
        }

    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("IsVanishing", true);
        while (isAttacking)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("appear"))
            {
                //Vector3 tpPos = transform.position;
                //tpPos.y = -23.2f;
                //if (dude.transform.position.x > transform.position.x || dude.transform.position.x < minX.transform.position.x)
                //{
                //  //  -23.2
                //    Vector3 newScale = transform.localScale;
                //    newScale.x *= -1;
                //    transform.localScale = newScale;


                //    tpPos.x = dude.transform.position.x + 2;

                //}else if(dude.transform.position.x < transform.position.x || dude.transform.position.x > maxX.transform.position.x)
                //{
                //    tpPos.x = dude.transform.position.x - 2;
                //}
                //transform.position = tpPos;
                //print(tpPos + "ttpos");
                //print(transform.position + "trans pos");
                anim.SetBool("IsVanishing", false);
                attackCount++;
                Vector3 tpPos = maxX.transform.position;
                tpPos.x += Mathf.CeilToInt(Random.Range(-12f, 12f));
                transform.position = tpPos;
                print("do the thing");
            }
            if (attackCount < 3)
            {
                StartCoroutine(Attack());
            }
            else
            {
                isAttacking = false;
            }

        }




        yield return null;
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
    }
}

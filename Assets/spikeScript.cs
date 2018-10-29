using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeScript : MonoBehaviour {
    [Header("Speed of the spike")]
    [SerializeField]
    private float spikeSpeed = 25;
    private Rigidbody2D rb2D;
    private bool falling = false;
    [Header("What is target?")]
    [SerializeField]
    private LayerMask target;
    private GameController controller;
    private SpriteRenderer rend;
    private Quaternion startRot;
    private bool active = true;
	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
        rend = GetComponent<SpriteRenderer>();
        startRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (!rend.isVisible)
        {
            active = false;
        }
        else
        {
            active = true;
        }
	}

    private void FixedUpdate()
    {
        if (!falling && active)
        {
         
            Vector2 direction = -transform.up;
            if(transform.rotation.z != 0 && transform.rotation.z != 180)
            {
                direction = transform.forward;
            }
            RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, target);
          
            if (ray.collider != null)
            {
                print("hit");
                falling = true;
                Destroy(gameObject, 3);
            }
        }
        else if(falling && active)
        {

            
            //if (transform.rotation.z == 0) rb2D.velocity = new Vector2(0, -spikeSpeed);
            //else if (transform.localRotation.z == 90) rb2D.velocity = new Vector2(spikeSpeed, 0);
            //else if (transform.rotation.z == 180) rb2D.velocity = new Vector2(0, spikeSpeed);
            //else if (transform.rotation.z == 270) rb2D.velocity = new Vector2(-spikeSpeed, 0);
            //rb2D.AddForce(-Vector2.up * spikeSpeed * Time.time);
            rb2D.velocity = new Vector2(-transform.up.x * spikeSpeed*1.25f, -transform.up.y * spikeSpeed*1.25f);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            controller.youDiedLOL();
        }
    }
}

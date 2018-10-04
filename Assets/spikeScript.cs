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
	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (!falling)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, target);

            if (ray.collider != null)
            {
                falling = true;
                Destroy(gameObject, 15);
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0, -spikeSpeed);
            //rb2D.AddForce(-Vector2.up * spikeSpeed * Time.time);
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

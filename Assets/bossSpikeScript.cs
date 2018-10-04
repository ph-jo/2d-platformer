using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpikeScript : MonoBehaviour {
    [Header("Speed of the spike")]
    [SerializeField]
    private float spikeSpeed = 25;
    private Rigidbody2D rb2D;

    private GameController controller;
	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
        


        Destroy(this, 10f);
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
        
    private void FixedUpdate()
    {
        rb2D.AddForce(-transform.up * 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            controller.youDiedLOL();
        }
    }
}

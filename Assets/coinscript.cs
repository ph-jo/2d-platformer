using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinscript : MonoBehaviour {
    private GameController controller;
    public GameObject explosion;
    // Use this for initialization
    void Start () {
      controller = FindObjectOfType<GameController>();
      
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //do stuff
            controller.pickupCoin();
            explosion.transform.position = transform.position;
            Instantiate(explosion);
            Destroy(gameObject);

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

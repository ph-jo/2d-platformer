using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beet_flying_script : MonoBehaviour {

    private Rigidbody2D rb2d;
    private float topPos;
    private float bottomPos;
    private bool goingUp;
    public GameObject deathAnimation;
    private bool isQuitting = false;
    // Use this for initialization  
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        topPos = transform.position.y + 2f;
        bottomPos = transform.position.y - 2f;
        float xd = Random.Range(-1f, 1f);
        transform.position = new Vector2(transform.position.x, transform.position.y + xd);
       
     
        if (xd > 0)
        {
            goingUp = true;
        }
        else
        {
            goingUp = false;
        }
      
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
        if (deathAnimation != null && !isQuitting)
        {
            deathAnimation.transform.position = transform.position;
            Instantiate(deathAnimation);
        }

    }

    private void FixedUpdate()
    {
        if (transform.position.y > topPos || transform.position.y < bottomPos) goingUp = !goingUp;
        if (goingUp)
        {
              rb2d.velocity = new Vector2(0, 3);
            //rb2d.AddForce(Vector2.up * 20);

        }else
        {
           // rb2d.AddForce(-Vector2.down * 20);
            rb2d.velocity = new Vector2(0, -3);
        }

        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisintegratingBlock : MonoBehaviour {

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("End")){

            Destroy(gameObject, 0.05f);
        }
    }


  
        

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            anim.SetBool("isDisintegrating", true);
        }
    }

}

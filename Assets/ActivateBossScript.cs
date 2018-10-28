using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBossScript : MonoBehaviour {

    private VampireBossScript boss;
	// Use this for initialization
	void Start () {
        boss = FindObjectOfType<VampireBossScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            boss.Activate();
        }
    }
}

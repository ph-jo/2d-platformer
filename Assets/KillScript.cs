using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour {
    private GameController controller;
	// Use this for initialization
	void Start () {
        controller = FindObjectOfType<GameController>();

	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            controller.youDiedLOL();
        }
    }
   
    // Update is called once per frame
    void Update () {
		
	}
}

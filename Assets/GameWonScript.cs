using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonScript : MonoBehaviour {
    private GameController controller;
	// Use this for initialization
	void Start () {
        controller = FindObjectOfType<GameController>();
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            controller.gameCompleted();

        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour {

    public GameObject toMove;
    private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        rb2d = toMove.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb2d.AddForce(new Vector2(0, 30));
    }
}

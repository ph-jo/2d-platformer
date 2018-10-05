using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWarpScript : MonoBehaviour {
    private Dude2D dude;
    private SpriteRenderer dudeRenderer;

    private Camera cam;
	// Use this for initialization
	void Start () {
        dude = FindObjectOfType<Dude2D>();
        dudeRenderer = dude.GetComponent<SpriteRenderer>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
    }
}

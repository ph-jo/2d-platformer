using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShiftScript : MonoBehaviour {
    private Dude2D dude;
    private SpriteRenderer dudeRenderer;
    private Camera cam;

    private float deltaY;
    private float deltaX;

    private float toMoveX = 35;
    private float toMoveY = 19;
    
    // Use this for initialization
    void Start()
    {
        dude = FindObjectOfType<Dude2D>();
        dudeRenderer = dude.GetComponent<SpriteRenderer>();
        cam = GetComponent<Camera>();
        cam.aspect = 16f / 9f;
    }



	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        //x: 17.5
        //y. 9.8
        deltaY = dude.transform.position.y - transform.position.y;
        deltaX = dude.transform.position.x - transform.position.x;
        //print("Delta y: " + deltaY);
        //print("Delta x: " + deltaX);
        if (!dudeRenderer.isVisible)
        {
            float previousposX = transform.position.x;
            float previousposY = transform.position.y;
            if(deltaX >= 17.5 || deltaX <= -17.5)
            {
                if (deltaX > 0) previousposX += toMoveX;
                else previousposX -= toMoveX;     

            }
            else if(deltaY >= 9 || deltaY <= -9)
            {
                if(deltaY > 0) previousposY += toMoveY;
                else previousposY -= toMoveY;
            }
            transform.position = new Vector3(previousposX, previousposY, transform.position.z);
        }
    }
}

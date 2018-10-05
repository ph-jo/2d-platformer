using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour {

    public Sprite activeSprite;
    private Sprite inactiveSprite;
    private bool active = false;
    private SpriteRenderer rend;
    private CheckpointController controller;
	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        controller = FindObjectOfType<CheckpointController>();
        inactiveSprite = rend.sprite;
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            rend.sprite = activeSprite;
        }
        else
        {
            rend.sprite = inactiveSprite;
        }
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!active)
            {
                active = true;
                rend.sprite = activeSprite;
                controller.setCheckPointPos(collision.transform.position);
                controller.setCurrentCheckpoint(this);
            }
        }
        
        
    }

    public void setActive(bool set)
    {
        active = set;
    }
}

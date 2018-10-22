﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerScript : MonoBehaviour {
    private Dude2D player;
    private float speed = 2f;
    // Use this for initialization
    private void Start()
    {
        player = FindObjectOfType<Dude2D>();
    }
    private void Awake()
    {
        
    }
    
   
    // Update is called once per frame
    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}
}
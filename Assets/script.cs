using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {
    public GameObject tofollow;

    private Vector3 offset;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {

        
	}

    private void FixedUpdate()
    {
        offset = transform.position - tofollow.transform.position;
        gameObject.transform.position = tofollow.transform.position;
        tofollow.transform.position = tofollow.transform.position + offset;
    }
}

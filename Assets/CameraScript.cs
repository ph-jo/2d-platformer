using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject tofollow;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - tofollow.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = tofollow.transform.position;
	}

    private void LateUpdate()
    {
        transform.position = tofollow.transform.position + offset;
    }
}

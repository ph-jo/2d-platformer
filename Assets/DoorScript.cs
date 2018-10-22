using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    private float _id;
    private CheckpointController cController;

	// Use this for initialization
	void Start () {
     _id = transform.position.sqrMagnitude;
        cController = FindObjectOfType<CheckpointController>();
    }

    // Update is called once per frame
    void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        cController.UnlockDoor(this);
        Destroy(gameObject);
    }
}

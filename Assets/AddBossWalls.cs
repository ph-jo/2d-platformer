using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBossWalls : MonoBehaviour {
    public GameObject bossWall;
    private VampireBossScript bossScript;
	// Use this for initialization
	void Start () {
        bossScript = FindObjectOfType<VampireBossScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossWall.SetActive(true);
    }
}

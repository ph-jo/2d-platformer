using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpReplenisher : MonoBehaviour {
    [Header("Doublejump respawn timer")]
    [SerializeField]
    private int respawnTimer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.transform.tag == "Player")
    //    {
    //        gameObject.SetActive(false);
    //        Wait(respawnTimer);


    //    }
    //}

    //    private IEnumerator Wait(int seconds)
    //{
    //    gameObject.SetActive(false);
    //    yield return new WaitForSeconds(seconds);
    //    gameObject.SetActive(true);
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //    if(collision.otherCollider.transform.tag == "Player")
    //    {
    //        Debug.Log("player hit double jump thing lmfao lol");
    //    }
    //}
}

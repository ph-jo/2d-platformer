using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearScript : MonoBehaviour {

    public float waitTime = 1.2f;
    private float t;
    [Header("Does this block start on?")]
    public bool active;
    private GameObject child;
	// Use this for initialization
	void Start () {

        child = transform.Find("w").gameObject;
        
        StartCoroutine(wait(waitTime));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        t = Time.time;
        if (t > waitTime)
        {
            
        }
    }

    private IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        child.SetActive(active);
        yield return new WaitForSeconds(time);
        child.SetActive(!active);
        
        StartCoroutine(wait(waitTime));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCount : MonoBehaviour {
    public int Lives;

	// Use this for initialization
	void Start () {
		
	}
    private void Awake()
    {
       
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void setLives(int lives)
    {
        Lives = lives;
    }
    public int getLives()
    {
        return Lives;
    }
    public void playerDeath()
    {
        Lives--;
    }
}

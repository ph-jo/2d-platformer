using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCount : MonoBehaviour {
    public int Lives = 0;
    public static LifeCount instance;

	// Use this for initialization
	void Start () {
	    if(instance == null)
        {
            instance = this;
            Lives = PlayerPrefs.GetInt("lives");
            
        }	else if(instance != this)
        {
            Destroy(gameObject);
        }
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
        PlayerPrefs.SetInt("lives", lives);
    }
    public int getLives()
    {
        //   return Lives;
        return PlayerPrefs.GetInt("lives");
    }
    public void playerDeath()
    {
        Lives++;
        PlayerPrefs.SetInt("lives", Lives);
    }
}

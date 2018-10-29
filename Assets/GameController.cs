using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject gameOver;
    public GameObject lostTheGame;
    public bool gameLost = false;
    public GameObject player;
    public GameObject gameWon;
    private LifeCount lives;
    public Text lifeText;
    private bool dead;
    private int saveID;
   

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
            lives = FindObjectOfType<LifeCount>();
            if(lives.getLives() == 1)
            {
                lifeText.text = lives.getLives() + " Death";
            }
            else
            {
                lifeText.text = lives.getLives() + " Deaths";

            }
            dead = false;   
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
        
	}




    // Update is called once per frame
    void Update () {
		
        if(gameLost && Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

           // EnemyScript enemies = FindObjectOfType<EnemyScript>();

           // Application.LoadLevel(Application.loadedLevel);

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            youDiedLOL();
        }

	}
    public void gameCompleted()
    {
        gameWon.SetActive(true);
        player.SetActive(false);
    }
    public void youDiedLOL()

    {
      
        if (dead) return;
        dead = true;
        lives.playerDeath();

        gameOver.SetActive(true);
        gameLost = true;

        player.SetActive(false);

    }

    public void gameCompletelyOver()
    {
        
    }


   


}

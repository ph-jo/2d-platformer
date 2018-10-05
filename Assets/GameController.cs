using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public Text coinsText;
    public GameObject gameOver;
    public GameObject lostTheGame;
    public int coins = 0;
    public bool gameLost = false;
    public GameObject player;
    public GameObject gameWon;
    private LifeCount lives;
    public Text lifeText;
    private bool dead;
    private bool outOfLives = false;

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
            lives = FindObjectOfType<LifeCount>();
            lifeText.text = lives.getLives() + "";
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

        }else if(outOfLives && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            lives.setLives(3);
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
        if (lives.getLives() == 0)
        {
            lostTheGame.SetActive(true);
            outOfLives = true;
        }else
        {
            print(lives.getLives());
            gameOver.SetActive(true);
            gameLost = true;
        }
        player.SetActive(false);

    }

    public void gameCompletelyOver()
    {
        
    }

    public void pickupCoin()
    {
        coins++;
        coinsText.text = coins + "";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Canvas otherCanvas;
    public static MenuController instance;
    public static MenuController settingsInstance;
    private bool isActive;
	// Use this for initialization
	void Start () {
        if(instance == null)
        {
            isActive = true;
            instance = this;

        }
        else
        {
            if(gameObject.name == "SettingsCanvas")
            {
                if(settingsInstance == null)
                {
                    instance = this;
                    isActive = true;
                }
                else
                {
                    
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        if(isActive) Time.timeScale = 0;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void startGame()
    {
        Time.timeScale = 1;
        isActive = false;
        gameObject.SetActive(false);
        print("Starting game");


    }
    public void OpenOther()
    {
        isActive = false;
        otherCanvas.gameObject.SetActive(true);
        otherCanvas.transform.gameObject.SetActive(true);
        gameObject.SetActive(false);
        Debug.Log(otherCanvas.name);
        

    }
 
    public void ExitGame()
    {
        Application.Quit();
    }
}

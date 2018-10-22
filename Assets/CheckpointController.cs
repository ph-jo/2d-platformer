using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public static CheckpointController instance;
    private Vector3 currentCheckpointPos;
    private CheckPointScript currentPoint;
    private CheckPointScript[] allcheckpoints;
    private List<DoorScript> unlockedDoors;
    private Dude2D player;
    //public int currentLevelX { get; set; }
    //public int currentLevelY {  get; set; }
    // Use this for initialization
    void Start () {
		if(instance == null)
        {
            instance = this;
            player = FindObjectOfType<Dude2D>();
            currentCheckpointPos = new Vector3();
            allcheckpoints = FindObjectsOfType<CheckPointScript>();
            foreach (CheckPointScript s in allcheckpoints)
            {
                print("Checkpoint with ID: " + s.getId());
                if (s.getId() == PlayerPrefs.GetFloat("checkpoint"))
                {
                    setCurrentCheckpoint(s);
                    currentPoint.setActive(true);
                    player.setSpawnPosition(currentPoint.transform.position);
                }
            }
        }
        else if(instance != this)
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
    public void resetCheckpoints()
    {
        foreach (CheckPointScript item in allcheckpoints)
        {
            item.setActive(false);
        }
        setCurrentCheckpoint(null);
    }
    
    public Vector3 getCheckpointPos()
    {

        if (currentCheckpointPos != null)
        {
            return currentCheckpointPos;
        }
        return new Vector3();
    }

    public void setCheckPointPos(Vector3 t)
    {
        currentCheckpointPos = t;
    }
    public void setCurrentCheckpoint(CheckPointScript cps)
    {
        if(currentPoint != null) currentPoint.setActive(false);
        if(cps!=null)
        {
            setCheckPointPos(cps.transform.position);
            PlayerPrefs.SetFloat("checkpoint", cps.getId());

        }
        else
        {
            setCheckPointPos(new Vector3());
        }

        PlayerPrefs.Save();
        currentPoint = cps;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void UnlockDoor(DoorScript ds)
    {
        
    }


}

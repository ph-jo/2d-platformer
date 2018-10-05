using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public static CheckpointController instance;
    private Vector3 currentCheckpointPos;
    private CheckPointScript currentPoint;
    private CheckPointScript[] allcheckpoints;
    // Use this for initialization
    void Start () {
		if(instance == null)
        {
            instance = this;
            currentCheckpointPos = new Vector3();
            allcheckpoints = FindObjectsOfType<CheckPointScript>();

            foreach (CheckPointScript s in allcheckpoints)
            {
                if (currentPoint == s) s.setActive(true);
                
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

        currentPoint = cps;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersManager : MonoBehaviour {
    public GameObject leader;
    public int laneWidth = 5; //width of each seperate lane
    public int workersSepDis = 5;//distance workers keep from each other
    public int arrivalSlowingRad = 5;//slow when entering this rad
    public int maxSepForce = 10;
    public int maxFolForce = 10;
    public int maxSpeed = 10;
    public int aheadFollowPoint = 5;//distance infront of workers to follow

    // Use this for initialization
    void Start () {
        GlobalData.leader = leader;
        //leader rb useless for now
        GlobalData.leaderRb = leader.GetComponent<Rigidbody>();

        GlobalData.workersSepDis = workersSepDis;
        GlobalData.maxSepForce = maxSepForce;
        GlobalData.maxFolForce = maxFolForce;
        GlobalData.maxSpeed = maxSpeed;
        GlobalData.arrivalSlowingRad = arrivalSlowingRad;
        GlobalData.aheadFollowPoint = aheadFollowPoint;
        GlobalData.workers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Worker"));

        //workers rigidbody mostly won't be used(maybe removed soon)
        //GlobalData.workers.Remove(leader);
        foreach (GameObject worker in GlobalData.workers)
        {
            GlobalData.workersRb.Add(worker.GetComponent<Rigidbody>());
        }
        GlobalData.leaderRb.velocity = new Vector3(5, 0);
    }
	
	// Update is called once per frame
	void Update () {
        updateLeaderPos();
        GlobalData.laneWidth = laneWidth;

        //used for testing at this point for WorkersManager update should be empt
        if(leader.transform.position.x > 100)
        {
            Vector3 newPos = leader.transform.position;
            newPos.x = -100;
            leader.transform.position = newPos;
            SetWorkersNewPos();
        }
    }

    void SetWorkersNewPos()
    {
        
    }

    private void FixedUpdate()
    {

    }

    void updateLeaderPos()
    {
        GlobalData.leaderPos.x = GlobalData.leader.transform.position.x;
        GlobalData.leaderPos.y = GlobalData.leader.transform.position.y;
    }

}

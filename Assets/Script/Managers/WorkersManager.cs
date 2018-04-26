using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersManager : MonoBehaviour {
    public GameObject leader;
    public int LaneWidth = 5;
    // Use this for initialization
    void Start () {
        GlobalData.Leader = leader;
        GlobalData.workers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Worker"));
        GlobalData.workersRb = new List<Rigidbody>();
        //GlobalData.workers.Remove(leader);
        foreach (GameObject worker in GlobalData.workers)
        {
            GlobalData.workersRb.Add(worker.GetComponent<Rigidbody>());
        }
    }
	
	// Update is called once per frame
	void Update () {
        leader.transform.position += new Vector3(0.1f, 0);
        if(leader.transform.position.x > 12)
        {
            Vector3 newPos = leader.transform.position;
            newPos.x = -12;
            leader.transform.position = newPos;
            StartCoroutine(SetWorkersNewPos());
        }
    }
    IEnumerator SetWorkersNewPos()
    {
        for (int i = 0; i < 1000; i++)
        {
            yield return null;
        }
        foreach (GameObject worker in GlobalData.workers)
        {
            GlobalData.workersRb.Add(worker.GetComponent<Rigidbody>());
        }
    }
    private void FixedUpdate()
    {
        GlobalData.LaneWidth = LaneWidth;
    }

}

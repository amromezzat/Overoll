using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersManager : MonoBehaviour
{
    public GameObject leader;
    public WorkerConfig wc;

    // Use this for initialization
    void Start()
    {
        wc.leader = leader;
        wc.leaderRb = leader.GetComponent<Rigidbody>();
        //wc.leaderRb.velocity = new Vector3(10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeaderPos();
    }

    void SetWorkersNewPos()
    {

    }

    private void FixedUpdate()
    {

    }

    void UpdateLeaderPos()
    {
    }

}

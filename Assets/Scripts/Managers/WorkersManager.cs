using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersManager : MonoBehaviour
{
    public GameObject leader;
    public WorkerConfig wc;
    public TileConfig tc;

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
    }

    private void FixedUpdate()
    {
    }

    public void AddWorker()
    {
        GameObject worker= ObjectPool.instance.GetFromPool(wc.worker);
        float newXPos = Random.Range(leader.transform.position.x - tc.laneWidth, leader.transform.position.x + tc.laneWidth);
        float newZPos = Random.Range(leader.transform.position.z - tc.laneWidth, leader.transform.position.z + tc.laneWidth);
        worker.transform.position = new Vector3(newXPos, worker.transform.position.y, newZPos);
    }
}

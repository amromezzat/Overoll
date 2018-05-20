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
    }

    private void FixedUpdate()
    {
    }

    public void AddWorker()
    {
        ObjectPool.instance.GetFromPool(wc.worker);
        //Instantiate(wc.workerPrefab, transform.position + new Vector3(Random.Range(-5,5), 0, Random.Range(-5, 5)),
        //    Quaternion.identity, transform);
    }
}

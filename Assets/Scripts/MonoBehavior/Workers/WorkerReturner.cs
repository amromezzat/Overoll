using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerReturner : ObjectReturner
{

    public WorkerConfig wc;
    WorkerFollowState wfs;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        wfs = GetComponent<WorkerFollowState>();
    }

    public IEnumerator PoolReturnCoroutine(float returnTime)
    {
        rb.velocity = Vector3.back * tc.tileSpeed;
        wc.workers.Remove(gameObject);
        if (wfs.leader)
        {
            wc.onLeaderDeath.Invoke();
        }
        wfs.followType = FollowType.Dying;
        yield return new WaitForSeconds(returnTime);
        wfs.followType = FollowType.LeaderFollow;
        ReturnToPool();
    }
}

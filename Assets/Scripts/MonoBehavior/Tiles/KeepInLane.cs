using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInLane : MonoBehaviour {

    public TileConfig tc;
    public Transform otherCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            //if (!other.GetComponent<WorkerLifeCycle>().isLeader)
            //{
            //    other.GetComponent<Rigidbody>().AddForce(
            //        (otherCollider.position - transform.position).normalized * tc.keepInLaneForce,
            //        ForceMode.VelocityChange);
            //}
        }
    }
}

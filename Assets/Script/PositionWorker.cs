using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionWorker : MonoBehaviour
{
    Rigidbody rb;
    int maxForce = 200;
    int maxVel = 5;
    float neighborDist = 2f;
    Vector3 steerForce;

    // Use this for initialization
    void Start()
    {
        steerForce = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        steerForce = FollowLeader() + KeepDistance();
    }

    Vector3 FollowLeader()
    {
        //Vector3 LeaderFutureLocation = GlobalData.Leader.GetComponent<Rigidbody>().velocity.normalized * 100 + GlobalData.Leader.transform.position;
        Vector3 desiredVelocity = GlobalData.Leader.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * maxVel;
        Vector3 steerToLeader = desiredVelocity - rb.velocity;
        steerToLeader = steerToLeader.normalized * maxForce;
        float decelerate = GetDeceleration(desiredVelocity.sqrMagnitude, steerToLeader.magnitude);
        steerToLeader *= decelerate * Time.deltaTime;
        if (GlobalData.Leader.transform.position.z - GlobalData.LaneWidth < transform.position.z
            && GlobalData.Leader.transform.position.z + GlobalData.LaneWidth > transform.position.z)
        {
            steerToLeader.z = 0;
        }
        return steerToLeader;
    }

    Vector3 KeepDistance()
    {
        Vector3 averageDesiredVelocity = Vector3.zero;
        int count = 0;
        for (int i = 0; i < GlobalData.workers.Count; i++)
        {
            if (GlobalData.workers[i].GetInstanceID() != GetInstanceID())
            {
                Vector3 averageDist = GlobalData.workers[i].transform.position - transform.position;
                if (averageDist.sqrMagnitude <= neighborDist)
                {
                    averageDesiredVelocity += averageDist;
                    count++;
                }
            }
        }
        if (count > 0)
        {
            averageDesiredVelocity /= count;
            averageDesiredVelocity = averageDesiredVelocity.normalized * maxVel;
            Vector3 SteerToKeepDist = averageDesiredVelocity - rb.velocity;
            SteerToKeepDist = -SteerToKeepDist.normalized * maxForce / 40;
            //float decelerate = GetDeceleration(SteerToKeepDist.magnitude, SteerToKeepDist.magnitude);
            //steerToLeader *= decelerate * Time.deltaTime;
            return SteerToKeepDist;
        }
        return Vector3.zero;
    }

    private void FixedUpdate()
    {
        //rb.angularVelocity = Vector3.zero;
        rb.velocity = steerForce;
        //rb.AddForce(steerForce, ForceMode.VelocityChange);
    }

    //decelerate before hitting the leader
    public static float GetDeceleration(float dist, float speed)
    {
        if (dist < 0.5)
        {
            return 0.1f / speed;
        }
        else if (dist < 2)
        {
            return dist / speed;
        }

        return 1;
    }
}

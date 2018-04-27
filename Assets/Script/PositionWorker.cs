using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionWorker : MonoBehaviour
{
    Rigidbody rb;
    Vector2 newVelocity;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        newVelocity = Vector2.ClampMagnitude(SteeringForce(), GlobalData.maxSpeed);
        rb.AddForce(new Vector3(newVelocity.x, 0, newVelocity.y));
    }

    Vector2 SteeringForce()
    {
        // Creates a force to arrive at the behind point
        Vector2 steeringForce = FollowLeader(); // 50 is the arrive radius

        //seperate workers
        steeringForce += StayAway();
        return steeringForce;

    }

    //seperating worker force
    Vector2 StayAway()
    {
        //the point and magnitude at which we give to the worker
        //to the avoid the crowd
        Vector2 seperationForce = Vector2.zero;
        int neighborCount = 0;

        foreach (GameObject worker in GlobalData.workers)
        {
            if (worker.GetInstanceID() != GetInstanceID() && CalculateDisFrom(worker) < GlobalData.workersSepDis)
            {
                seperationForce.x += worker.transform.position.x - transform.position.x;
                seperationForce.y += worker.transform.position.z - transform.position.z;
                neighborCount++;
            }
        }
        if (neighborCount == 0)
            return seperationForce;
        //get the average point to apply the seperation
        seperationForce /= neighborCount;
        //move in the opposite direction from the average direction from the workers
        seperationForce *= -1;
        seperationForce = seperationForce.normalized * GlobalData.maxSepForce;
        return seperationForce;
    }

    //chase leader while maintaining a distance behind him
    Vector2 FollowLeader()
    {
        Vector2 traverseVec = Vector2.zero;
        traverseVec.x = GlobalData.leaderRb.velocity.x;
        traverseVec.y = GlobalData.leaderRb.velocity.z;
        traverseVec = traverseVec.normalized * GlobalData.aheadFollowPoint;
        Vector2 aheadDis = Vector2.zero;
        aheadDis.x = GlobalData.leader.transform.position.x + traverseVec.x;
        aheadDis.y = GlobalData.leader.transform.position.z + traverseVec.y;
        // Calculate the desired velocity
        Vector2 desiredVelocity = Vector2.zero;
        desiredVelocity.x = aheadDis.x - transform.position.x;
        desiredVelocity.y = aheadDis.y - transform.position.z;
        float distance = desiredVelocity.magnitude;

        // Check the distance to detect whether the character
        // is inside the slowing area
        if (distance < GlobalData.arrivalSlowingRad)
        {
            // Inside the slowing area
            desiredVelocity *= distance / GlobalData.arrivalSlowingRad;
        }

        // Set the steering based on this
        Vector2 folForce = Vector2.zero;
        folForce.x = desiredVelocity.x - rb.velocity.x;
        folForce.y = desiredVelocity.y - rb.velocity.z;
        folForce = Vector3.ClampMagnitude(folForce, GlobalData.maxFolForce);
        return folForce;
    }

    float CalculateDisFrom(GameObject entity)
    {
        return (entity.transform.position - transform.position).magnitude;
    }
}

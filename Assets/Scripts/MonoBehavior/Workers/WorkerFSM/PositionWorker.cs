
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PositionWorker : IDoAction
//{
//    Rigidbody rb;
//    Vector2 steeringForce;

//    bool onHalt;

//    private void FixedUpdate()
//    {
//        steeringForce = Vector2.ClampMagnitude(SteeringForce(), wc.maxSpeed);
//        rb.AddForce(new Vector3(steeringForce.x, 0, steeringForce.y));
//    }

//    Vector2 SteeringForce()
//    {
//        // Creates a force to arrive at the behind point
//        Vector2 steeringForce = Vector2.zero;
//        if (wsh.workerType == WorkerType.Worker)
//        {
//            steeringForce = FollowLeader(wc.leader.transform.position, wc.aheadFollowPoint);
//            //Seperate workers
//            steeringForce += StayAway();
//        }
//        else if(wsh.mergeType == MergeType.SlaveMerger)
//        {
//            steeringForce = FollowLeader(wm.followedTransform.position, 0, false);
//        }
//        return steeringForce;
//    }

//    //seperating worker force
//    Vector2 StayAway()
//    {
//        //the point and magnitude at which we give to the worker
//        //to the avoid the crowd
//        Vector2 seperationForce = Vector2.zero;
//        int neighborCount = 0;

//        foreach (GameObject worker in wc.workers)
//        {
//            if (worker.GetInstanceID() != GetInstanceID() && CalculateDisFrom(worker) < wc.workersSepDis)
//            {
//                seperationForce.x += worker.transform.position.x - transform.position.x;
//                seperationForce.y += worker.transform.position.z - transform.position.z;
//                neighborCount++;
//            }
//        }
//        if (neighborCount == 0)
//            return seperationForce;
//        //get the average point to apply the seperation
//        seperationForce /= neighborCount;
//        //move in the opposite direction from the average direction from the workers
//        seperationForce *= -1;
//        seperationForce = seperationForce.normalized * wc.maxSepForce;
//        return seperationForce;
//    }

//    //chase leader while maintaining a distance behind him
//    Vector2 FollowLeader(Vector3 followedPos, float aheadFollowPoint, bool slowDown = true)
//    {
//        Vector2 aheadDis = Vector2.zero;
//        aheadDis.x = followedPos.x;
//        aheadDis.y = followedPos.z + aheadFollowPoint;
//        // Calculate the desired velocity
//        Vector2 desiredVelocity = Vector2.zero;
//        desiredVelocity.x = aheadDis.x - transform.position.x;
//        desiredVelocity.y = aheadDis.y - transform.position.z;
//        float distance = desiredVelocity.magnitude;

//        // Check the distance to detect whether the character
//        // is inside the slowing area
//        if (slowDown && distance < wc.arrivalSlowingRad)
//        {
//            // Inside the slowing area
//            desiredVelocity *= distance / wc.arrivalSlowingRad;
//        }

//        // Set the steering based on this
//        Vector2 folForce = Vector2.zero;
//        folForce.x = desiredVelocity.x - rb.velocity.x;
//        folForce.y = desiredVelocity.y - rb.velocity.z;
//        //folForce = Vector2.ClampMagnitude(folForce, wc.maxFolForce);
//        return folForce.normalized * wc.maxFolForce;
//    }

//    float CalculateDisFrom(GameObject entity)
//    {
//        return (entity.transform.position - transform.position).magnitude;
//    }
//}

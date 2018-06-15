
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionWorker : SeekPosition
{
    public PositionWorker(WorkerConfig wc, Rigidbody rb, Transform transform, int id) : base(wc, rb, transform, id)
    {
    }


    //seperating worker force
    Vector2 StayAway()
    {
        //the point and magnitude at which we give to the worker
        //to the avoid the crowd
        Vector2 seperationForce = Vector2.zero;
        int neighborCount = 0;

        foreach (GameObject worker in wc.workers)
        {
            if (worker.GetInstanceID() != id && CalculateDisFrom(worker) < wc.workersSepDis)
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
        seperationForce = seperationForce.normalized * wc.maxSepForce;
        return seperationForce;
    }

    public override Vector2 SteeringForce()
    {
        // Creates a force to arrive at the behind point
        Vector2 steeringForce = Vector2.zero;
        steeringForce = SeekTarget(wc.leader.transform.position, wc.aheadFollowPoint);
        //Seperate workers
        steeringForce += StayAway();
        return steeringForce;
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        steeringForce = Vector2.ClampMagnitude(SteeringForce(), wc.maxSpeed);
        rb.AddForce(new Vector3(steeringForce.x, 0, steeringForce.y));
    }

    public override void ScriptReset()
    {

    }
}

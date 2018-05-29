using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PositionWorker : MonoBehaviour, iHalt
{
    Rigidbody rb;
    Vector2 steeringForce;
    public WorkerConfig wc;

    bool isHalt;

    public GameData gameData;

    private void OnEnable()
    {
        RegisterListeners();
    }

    void Start()
    {
        wc.workers.Add(gameObject);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        steeringForce = Vector2.ClampMagnitude(SteeringForce(), wc.maxSpeed);
        rb.AddForce(new Vector3(steeringForce.x, 0, steeringForce.y));
    }

    Vector2 SteeringForce()
    {
        if (isHalt) { return Vector2.zero; }
        // Creates a force to arrive at the behind point
        Vector2 steeringForce = FollowLeader();

        //Seperate workers
        steeringForce += StayAway();

        //Keep workers in a formation
        steeringForce += KeepFormation();

        return steeringForce;
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
            if (worker.GetInstanceID() != GetInstanceID() && CalculateDisFrom(worker) < wc.workersSepDis)
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

    //chase leader while maintaining a distance behind him
    Vector2 FollowLeader()
    {
        Vector2 traverseVec = Vector2.zero;
        traverseVec.x = wc.leader.transform.position.x;
        traverseVec.y = wc.leader.transform.position.z;
        traverseVec = traverseVec.normalized + new Vector2(0, wc.aheadFollowPoint);
        Vector2 aheadDis = Vector2.zero;
        aheadDis.x = wc.leader.transform.position.x + traverseVec.x;
        aheadDis.y = wc.leader.transform.position.z + traverseVec.y;
        // Calculate the desired velocity
        Vector2 desiredVelocity = Vector2.zero;
        desiredVelocity.x = aheadDis.x - transform.position.x;
        desiredVelocity.y = aheadDis.y - transform.position.z;
        float distance = desiredVelocity.magnitude;

        // Check the distance to detect whether the character
        // is inside the slowing area
        if (distance < wc.arrivalSlowingRad)
        {
            // Inside the slowing area
            desiredVelocity *= distance / wc.arrivalSlowingRad;
        }

        // Set the steering based on this
        Vector2 folForce = Vector2.zero;
        folForce.x = desiredVelocity.x - rb.velocity.x;
        folForce.y = desiredVelocity.y - rb.velocity.z;
        //folForce = Vector2.ClampMagnitude(folForce, wc.maxFolForce);
        return folForce.normalized * wc.maxFolForce;
    }

    Vector2 KeepFormation()
    {
        Vector2 formationForce = Vector2.zero;
        formationForce.x = wc.leader.transform.position.x + wc.leftWingShape.Evaluate(transform.position.z) +
            wc.rightWingShape.Evaluate(transform.position.z);
        formationForce.x -= transform.position.x;
        formationForce.y = wc.centerShape.Evaluate(transform.position.x);
        formationForce.y -= transform.position.z;
        return formationForce.normalized * wc.maxFormForce;
    }

    float CalculateDisFrom(GameObject entity)
    {
        return (entity.transform.position - transform.position).magnitude;
    }

    public void Halt()
    {
        isHalt = true;
    }

    public void Resume()
    {
        isHalt = false;
    }

    public void RegisterListeners()
    {
        gameData.OnStart.AddListener(Halt);
        gameData.onPause.AddListener(Halt);
        gameData.OnResume.AddListener(Resume);
    }
}

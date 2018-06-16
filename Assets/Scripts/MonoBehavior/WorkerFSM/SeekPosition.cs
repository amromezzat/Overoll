using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SeekPosition : IWorkerScript
{

    protected WorkerConfig wc;
    protected Rigidbody rb;
    protected Transform transform;
    protected int id;

    public Vector2 steeringForce;

    protected SeekPosition(WorkerConfig wc, Rigidbody rb, Transform transform)
    {
        this.wc = wc;
        this.rb = rb;
        this.transform = transform;
    }

    public SeekPosition(WorkerConfig wc, Rigidbody rb, Transform transform, int id)
    {
        this.wc = wc;
        this.rb = rb;
        this.transform = transform;
        this.id = id;
    }

    //chase leader while maintaining a distance behind him
    public Vector2 SeekTarget(Vector3 followedPos, float aheadFollowPoint, bool slowDown = true)
    {
        Vector2 aheadDis = Vector2.zero;
        aheadDis.x = followedPos.x;
        aheadDis.y = followedPos.z + aheadFollowPoint;
        // Calculate the desired velocity
        Vector2 desiredVelocity = Vector2.zero;
        desiredVelocity.x = aheadDis.x - transform.position.x;
        desiredVelocity.y = aheadDis.y - transform.position.z;
        float distance = desiredVelocity.magnitude;

        // Check the distance to detect whether the character
        // is inside the slowing area
        if (slowDown && distance < wc.arrivalSlowingRad)
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

    public float CalculateDisFrom(GameObject entity)
    {
        Vector2 entityPos = new Vector2(entity.transform.position.x, entity.transform.position.z);
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        return (entityPos - pos).magnitude;
    }

    public abstract void FixedUpdate(float fixedDeltaTime);

    public abstract void ScriptReset();

    public abstract Vector2 SteeringForce();
}

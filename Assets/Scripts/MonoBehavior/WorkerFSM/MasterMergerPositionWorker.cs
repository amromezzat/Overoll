using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMasterMerger : SeekPosition {

    public PositionMasterMerger(WorkerConfig wc, Rigidbody rb, Transform transform) : base(wc, rb, transform)
    {
    }

    public PositionMasterMerger(WorkerConfig wc, Rigidbody rb, Transform transform, int id) : base(wc, rb, transform, id)
    {
    }

    protected override Vector2 SteeringForce()
    {
        // Creates a force to arrive at the point
        return SeekTarget(wc.leader.transform.position, wc.aheadFollowPoint); ;
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

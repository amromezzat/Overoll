using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekMasterMerger : SeekPosition
{
    public Transform seekedMerger;

    public SeekMasterMerger(WorkerConfig wc, Rigidbody rb, Transform transform) : base(wc, rb, transform)
    {
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        steeringForce = SteeringForce();
        rb.AddForce(new Vector3(steeringForce.x, 0, steeringForce.y));
    }

    public override void ScriptReset()
    {
    }

    protected override Vector2 SteeringForce()
    {
        return SeekTarget(seekedMerger.position, 0, false);
    }
}

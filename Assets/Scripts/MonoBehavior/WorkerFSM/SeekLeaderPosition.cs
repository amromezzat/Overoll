using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekLeaderPosition : IWorkerScript, IChangeState
{
    LanesDatabase lanes;
    Transform transform;
    WorkerConfig wc;
    float seekTimer = 0;

    public SeekLeaderPosition(Transform transform, WorkerConfig wc, LanesDatabase lanes)
    {
        this.transform = transform;
        this.wc = wc;
        this.lanes = lanes;
    }

    public void ScriptReset()
    {
        seekTimer = 0;
    }

    public void SetClosestLane()
    {
        float closestLaneDis = 100;
        int closestLane = 0;
        for (int i = 0; i < lanes.OnGridLanes.Count; i++)
        {
            if (CalculateXDisFrom(closestLaneDis) > CalculateXDisFrom(lanes.OnGridLanes[i].laneCenter))
            {
                closestLaneDis = lanes.OnGridLanes[i].laneCenter;
                closestLane = i;
            }
        }
        lanes.CurrentLane = lanes.OnGridLanes[closestLane];

    }

    public void FixedUpdate(float fixedDeltaTime)
    {
        seekTimer += fixedDeltaTime;
        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, 0, seekTimer / wc.takeLeadDuration);
        transform.position = newPos;
    }

    public float CalculateXDisFrom(float entityXPos)
    {
        return Mathf.Abs(entityXPos - transform.position.x);
    }

    public WorkerStateTrigger InputTrigger()
    {
        if (seekTimer >= 2)
        {
            seekTimer = 0;
            return WorkerStateTrigger.StateEnd;
        }
        return WorkerStateTrigger.Null;
    }
}

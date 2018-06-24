using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeLeaderSeeker : SeekLeaderPosition, IWCollide
{
    int mergedCount = 0;

    public MergeLeaderSeeker(Transform transform, WorkerConfig wc, LanesDatabase lanes) : base(transform, wc, lanes)
    {
    }

    public WorkerStateTrigger Collide(Collider collider, ref int health)
    {
        if (collider.CompareTag("SlaveMerger"))
        {
            wc.workers.Remove(collider.GetComponent<WorkerFSM>());
            ICollidable slaveMerger = collider.GetComponent<ICollidable>();
            health += slaveMerger.Gethealth();
            slaveMerger.ReactToCollision(0);
            mergedCount++;
            if (mergedCount >= wc.workersPerLevel - 1 
                && seekTimer >= wc.takeLeadDuration + 1)
            {
                seekTimer = 0;
                mergedCount = 0;
                return WorkerStateTrigger.StateEnd;
            }
        }
        return WorkerStateTrigger.Null;
    }

    public override WorkerStateTrigger InputTrigger()
    {
        if (mergedCount >= wc.workersPerLevel - 1
            && seekTimer >= wc.takeLeadDuration + 1)
        {
            seekTimer = 0;
            mergedCount = 0;
            return WorkerStateTrigger.StateEnd;
        }
        return WorkerStateTrigger.Null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergerCollide : IWCollide
{
    WorkerConfig wc;
    int mergedCount = 0;

    public MergerCollide(WorkerConfig wc)
    {
        this.wc = wc;
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
            if(mergedCount >= wc.workersPerLevel - 1)
            {
                mergedCount = 0;
                return WorkerStateTrigger.StateEnd;
            }
        }
        return WorkerStateTrigger.Null;
    }

    public void FixedUpdate(float fixedDeltaTime)
    {

    }

    public void ScriptReset()
    {
        mergedCount = 0;
    }
}

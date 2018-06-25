using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerList : List<WorkerFSM>
{
    public bool Merging = false;
    List<int> oldWorkersHealth;

    int workersPerLevel; //workers in each level
    int levels; //levels for merging

    WorkerFSM ascender;
    List<List<WorkerFSM>> workers = new List<List<WorkerFSM>>();

    public WorkerList(int workersPerLevel, int levels)
    {
        oldWorkersHealth = new List<int>();

        this.workersPerLevel = workersPerLevel;
        this.levels = levels;

        for (int i = 0; i < levels; i++)
        {
            workers.Add(new List<WorkerFSM>());
        }
    }

    public new void Add(WorkerFSM worker)
    {
        oldWorkersHealth.Add(worker.health);
        base.Add(worker);

        if (worker.currentState == WorkerState.Leader)
            workers[worker.level].Insert(0, worker);
        else
            workers[worker.level].Add(worker);

        if (Merging || worker.level == levels - 1)
        {
            return;
        }

        if (workers[worker.level].Count >= workersPerLevel)
        {
            Merging = true;

            workers[worker.level][0].ChangeState(WorkerStateTrigger.Merge);
            ascender = workers[worker.level][0];

            for (int i = 1; i < workersPerLevel; i++)
            {
                workers[worker.level][i].SetSeekedMaster(ascender.transform);
                workers[worker.level][i].ChangeState(WorkerStateTrigger.SlaveMerge);
            }
        }
    }

    public new void Remove(WorkerFSM worker)
    {
        oldWorkersHealth.Remove(IndexOf(worker));
        base.Remove(worker);

        workers[worker.level].Remove(worker);
    }

    //called after merging is finished
    public void Ascend()
    {
        Merging = false;

        //if merged while shield is active
        //correct old health to new merge health
        if(ascender.health > 1000)
            oldWorkersHealth[IndexOf(ascender)] = ascender.health / 1000;

        if (ascender.level < levels)
        {
            Remove(ascender);
            ascender.level++;
            Add(ascender);
        }
    }

    public WorkerFSM GetNewLeader()
    {
        for (int i = 0; i < workers.Count; i++)
        {
            if (workers[i].Count > 0)
            {
                workers[i][0].ChangeState(WorkerStateTrigger.Succeed);
                return workers[i][0];
            }
        }
        return null;
    }

    public void ResetWorkersHealth()
    {
        for(int i = 0; i < Count; i++)
        {
            this[i].health = oldWorkersHealth[i];
            this[i].helmetMaterial.SetFloat("_ExtAmount", 0);
        }
    }
}

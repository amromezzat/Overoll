/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerList : List<WorkerFSM>
{
    public bool Merging = false;
    //List<int> normWorkersHealth;

    int minWorkersToMerge; //workers in each level
    int levels; //levels for merging

    bool shieldOn = false;
    bool magnetOn = false;
    bool teacupOn = false;
    public bool doubleCoinOn = false;

    GameObject WorkerVest;

    WorkerFSM ascender;
    List<List<WorkerFSM>> workers = new List<List<WorkerFSM>>();

    public WorkerList(int workersPerLevel, int levels)
    {
        this.minWorkersToMerge = workersPerLevel;
        this.levels = levels;

        for (int i = 0; i < levels; i++)
        {
            workers.Add(new List<WorkerFSM>());
        }
    }

    public new void Add(WorkerFSM worker)
    {
        //add worker normal health
        //normWorkersHealth.Add(worker.health);
        base.Add(worker);

        //if there is a power up apply it to worker
        if (shieldOn)
            WorkerStartShield(worker);
        if (magnetOn)
            WorkerStartMagnet(worker);
        if (teacupOn)
            WorkerStartTeacup(worker);
        if (doubleCoinOn)
            WorkerStartDoubleCoin(worker);

        //if the leader is added after succeding
        //add him at the top of the level
        if (worker.currentState == WorkerState.Leader)
            workers[worker.level].Insert(0, worker);
        else
            workers[worker.level].Add(worker);

        if (!worker.gameObject.activeSelf)
        {
            WorkerStartShield(worker);
            WorkersManager.Instance.StartCoroutine(DisableStartProtection(worker));
            worker.gameObject.SetActive(true);
        }


        //if worker is at last level don't merge
        if (Merging || worker.level == levels - 1)
        {
            return;
        }

        //if the workers in current level match minimum workers to merge
        //start merging
        if (workers[worker.level].Count >= minWorkersToMerge)
        {
            Merging = true;

            int newMasterHealth = 0;

            WorkerStartShield(workers[worker.level][0]);
            workers[worker.level][0].ChangeState(WorkerStateTrigger.Merge);
            ascender = workers[worker.level][0];

            //set normal new master merger health
            for (int i = 1; i < minWorkersToMerge; i++)
            {
                WorkerStartShield(workers[worker.level][i]);
                workers[worker.level][i].SetSeekedMaster(ascender.transform);
                workers[worker.level][i].ChangeState(WorkerStateTrigger.SlaveMerge);
                newMasterHealth += workers[worker.level][i].health;
            }
        }
    }

    IEnumerator DisableStartProtection(WorkerFSM worker)
    {
        yield return new WaitForSeconds(3);

        if (!shieldOn)
            WorkerEndShield(worker);
    }

    public new void Remove(WorkerFSM worker)
    {
        base.Remove(worker);

        workers[worker.level].Remove(worker);

        if (shieldOn)
            WorkerEndShield(worker);
        if (magnetOn)
            WorkerEndMagnet(worker);
        if (teacupOn)
            WorkerEndTeacup(worker);
        if (doubleCoinOn)
            WorkerEndDoubleCoin(worker);
    }

    //called after merging is finished
    public void Ascend()
    {
        Merging = false;

        if (ascender.level < levels)
        {
            Remove(ascender);
            ascender.level++;
            Add(ascender);

            if (!shieldOn)
                WorkerEndShield(ascender);
        }
    }

    public void Descend(WorkerFSM worker)
    {
        Remove(worker);
        Add(worker);
    }

    public WorkerFSM GetNewLeader()
    {
        for (int i = 0; i < workers.Count; i++)
        {
            if (workers[i].Count > 0)
            {
                workers[i][0].ChangeState(WorkerStateTrigger.Succeed);
                WorkerStartShield(workers[i][0]);
                WorkersManager.Instance.StartCoroutine(DisableStartProtection(workers[i][0]));
                return workers[i][0];
            }
        }
        return null;
    }

    void WorkerStartShield(WorkerFSM worker)
    {
        worker.SetWorkerCollision(VestState.WithVest);
        worker.ParticlePowerUp.SetActive(true);
        worker.ParticleShield.SetActive(true);
        WorkerVest = worker.transform.GetChild(0).gameObject;
        WorkerVest.SetActive(true);
    }

    public void StartShieldPowerup()
    {
        shieldOn = true;
        for (int i = 0; i < Count; i++)
        {
            WorkerStartShield(this[i]);
        }
    }

    void WorkerEndShield(WorkerFSM worker)
    {
        worker.ParticleShield.SetActive(false);
        worker.SetWorkerCollision(VestState.WithoutVest);
        WorkerVest = worker.transform.GetChild(0).gameObject;
        WorkerVest.SetActive(false);
    }

    public void EndShieldPowerup()
    {
        shieldOn = false;
        for (int i = 0; i < Count; i++)
        {
            WorkerEndShield(this[i]);
        }
    }

    void WorkerStartTeacup(WorkerFSM worker)
    {
        worker.TeaOnHisHand.SetActive(true);
        worker.ParticlePowerUp.SetActive(true);
        worker.ParticleSpeed.SetActive(true);
        worker.GetComponent<Animator>().SetTrigger("Drink");
    }

    public void StartTeacupPowerUp()
    {
        teacupOn = true;
        SpeedManager.Instance.speed.Value += 1;
        for (int i = 0; i < Count; i++)
        {
            WorkerStartTeacup(this[i]);
        }
    }

    void WorkerEndTeacup(WorkerFSM worker)
    {
        worker.ParticleSpeed.SetActive(false);
    }

    public void EndTeacupPowerUp()
    {
        teacupOn = false;
        if (GameManager.Instance.gameState == GameState.Gameplay)
            SpeedManager.Instance.ResetSpeed();
        for (int i = 0; i < Count; i++)
        {
            WorkerEndTeacup(this[i]);
        }
    }

    void WorkerStartMagnet(WorkerFSM worker)
    {
        worker.ParticlePowerUp.SetActive(true);
        worker.ParticleMagnet.SetActive(true);
        worker.MagneOnHisHand.SetActive(true);
        worker.magnetColliderObject.SetActive(true);
        worker.GetComponent<Animator>().SetBool("HoldingMagnet", true);
    }

    public void StartMagnetPowerup()
    {
        magnetOn = true;
        for (int i = 0; i < Count; i++)
        {
            WorkerStartMagnet(this[i]);
        }
    }

    void WorkerEndMagnet(WorkerFSM worker)
    {
        worker.ParticleMagnet.SetActive(false);
        worker.MagneOnHisHand.SetActive(false);
        worker.magnetColliderObject.SetActive(false);
        worker.GetComponent<Animator>().SetBool("HoldingMagnet", false);
    }

    public void EndMagnetPowerup()
    {
        magnetOn = false;
        for (int i = 0; i < Count; i++)
        {
            WorkerEndMagnet(this[i]);
        }
    }

    void WorkerStartDoubleCoin(WorkerFSM worker)
    {
        worker.ParticlePowerUp.SetActive(true);
        worker.ParticleDoubleCoin.SetActive(true);
    }

    public void StartDoubleCoin()
    {
        doubleCoinOn = true;
        for (int i = 0; i < Count; i++)
        {
            WorkerStartDoubleCoin(this[i]);
        }
    }

    void WorkerEndDoubleCoin(WorkerFSM worker)
    {
        worker.ParticleDoubleCoin.SetActive(false);
    }

    public void EndDoubleCoin()
    {
        doubleCoinOn = false;
        for (int i = 0; i < Count; i++)
        {
            WorkerEndDoubleCoin(this[i]);
        }
    }
}

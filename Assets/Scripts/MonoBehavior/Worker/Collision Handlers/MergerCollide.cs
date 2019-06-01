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
/// <summary>
/// This Script is resposible for the merge between workers and change 
/// the skinned mesh renderer when the merge count to 5
/// </summary>
public class MergerCollide : IWCollide
{
    WorkerConfig wc;

    int mergedCount = 0;

    WorkerFSM wfsm;

    MeshChange m_meshChange;

    //SkinnedMeshRenderer hel;
    //SkinnedMeshRenderer ov;
    
    //List<Mesh> helMesh = new List<Mesh>();
    //List<Mesh> ovMesh = new List<Mesh>();

    public MergerCollide(WorkerConfig wc, MeshChange mc, WorkerFSM w)
    {
        this.wc = wc;
        wfsm = w;
        m_meshChange = mc;
    }

    public WorkerStateTrigger Collide(Collider collider, ref int health)
    {
        // When the master merger recieves a slave merger it returns it to pool,
        // until the number of leveling up is reached then it outputs
        // the trigger for the next state
        if (collider.CompareTag("SlaveMerger"))
        {
            WorkersManager.Instance.RemoveWorker(collider.GetComponent<WorkerFSM>());
            ICollidable slaveMerger = collider.GetComponent<ICollidable>();
            health += slaveMerger.Gethealth();
            slaveMerger.ReactToCollision(0);
            mergedCount++;
            if (mergedCount == wc.workersPerLevel - 1)
            {
                m_meshChange.ChangeHelmet(wfsm.level + 1);
                m_meshChange.ChangeOveroll(wfsm.level + 1);
                //hel.sharedMesh = helMesh[wfsm.level+1];
                //ov.sharedMesh = ovMesh[wfsm.level+1];
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

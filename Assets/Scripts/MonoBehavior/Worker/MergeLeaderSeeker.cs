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

public class MergeLeaderSeeker : SeekLeaderPosition, IWCollide
{
    int mergedCount = 0;

    public MergeLeaderSeeker(Transform transform, WorkerConfig wc, LanesDatabase lanes) : base(transform, wc, lanes)
    {
    }

    public WorkerStateTrigger Collide(Collider collider, ref int health)
    {
        // If merging worker hits the main merger his health is added up
        // then he is sent to pool
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

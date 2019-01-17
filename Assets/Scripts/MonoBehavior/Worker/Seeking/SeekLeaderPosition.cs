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

public class SeekLeaderPosition : IWChangeState
{
    protected LanesDatabase lanes;
    protected Transform transform;
    protected WorkerConfig wc;
    protected float seekTimer = 0;

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

    private float CalculateXDisFrom(float entityXPos)
    {
        return Mathf.Abs(entityXPos - transform.position.x);
    }

    public virtual WorkerStateTrigger InputTrigger()
    {
        if (seekTimer >= wc.takeLeadDuration + 1)
        {
            seekTimer = 0;
            return WorkerStateTrigger.StateEnd;
        }
        return WorkerStateTrigger.Null;
    }
}

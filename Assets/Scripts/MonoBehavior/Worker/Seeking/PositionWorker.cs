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

public class PositionWorker : PositionMasterMerger
{
    public PositionWorker(WorkerConfig wc, Rigidbody rb, Transform transform, int id) : base(wc, rb, transform, id)
    {
    }


    //seperating worker force
    Vector2 StayAway()
    {
        //the point and magnitude at which we give to the worker
        //to the avoid the crowd
        Vector2 seperationForce = Vector2.zero;
        int neighborCount = 0;

        foreach (WorkerFSM worker in wc.workers)
        {
            if (worker.GetInstanceID() != id && CalculateDisFrom(worker.gameObject) < wc.workersSepDis)
            {
                seperationForce.x += worker.transform.position.x - transform.position.x;
                seperationForce.y += worker.transform.position.z - transform.position.z;
                neighborCount++;
            }
        }
        if (neighborCount == 0)
            return seperationForce;
        //get the average point to apply the seperation
        seperationForce /= neighborCount;
        //move in the opposite direction from the average direction from the workers
        seperationForce *= -1;
        seperationForce = seperationForce.normalized * wc.maxSepForce;
        return seperationForce;
    }

    protected override Vector2 SteeringForce()
    {
        Vector2 steeringForce = base.SteeringForce();
        //Seperate workers
        steeringForce += StayAway();
        return steeringForce;
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

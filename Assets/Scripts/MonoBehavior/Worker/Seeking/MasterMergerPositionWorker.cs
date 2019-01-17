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
/// Master merger keeps following leader
/// </summary>
public class PositionMasterMerger : SeekPosition {

    public PositionMasterMerger(WorkerConfig wc, Rigidbody rb, Transform transform) : base(wc, rb, transform)
    {
    }

    public PositionMasterMerger(WorkerConfig wc, Rigidbody rb, Transform transform, int id) : base(wc, rb, transform, id)
    {
    }

    protected override Vector2 SteeringForce()
    {
        // Creates a force to arrive at the point
        return SeekTarget(wc.leader.transform.position, wc.aheadFollowPoint); ;
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

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

public abstract class SeekPosition : IWorkerScript
{

    protected WorkerConfig wc;
    protected Rigidbody rb;
    protected Transform transform;
    protected int id;

    public Vector2 steeringForce;

    protected SeekPosition(WorkerConfig wc, Rigidbody rb, Transform transform)
    {
        this.wc = wc;
        this.rb = rb;
        this.transform = transform;
    }

    protected SeekPosition(WorkerConfig wc, Rigidbody rb, Transform transform, int id)
    {
        this.wc = wc;
        this.rb = rb;
        this.transform = transform;
        this.id = id;
    }

    //chase leader while maintaining a distance behind him
    protected Vector2 SeekTarget(Vector3 followedPos, float aheadFollowPoint, bool slowDown = true)
    {
        Vector2 aheadDis = Vector2.zero;
        aheadDis.x = followedPos.x;
        aheadDis.y = followedPos.z + aheadFollowPoint;
        // Calculate the desired velocity
        Vector2 desiredVelocity = Vector2.zero;
        desiredVelocity.x = aheadDis.x - transform.position.x;
        desiredVelocity.y = aheadDis.y - transform.position.z;
        float distance = desiredVelocity.magnitude;

        // Check the distance to detect whether the character
        // is inside the slowing area
        if (slowDown && distance < wc.arrivalSlowingRad)
        {
            // Inside the slowing area
            desiredVelocity *= distance / wc.arrivalSlowingRad;
        }

        // Set the steering based on this
        Vector2 folForce = Vector2.zero;
        folForce.x = desiredVelocity.x - rb.velocity.x;
        folForce.y = desiredVelocity.y - rb.velocity.z;
        //folForce = Vector2.ClampMagnitude(folForce, wc.maxFolForce);
        return folForce.normalized * wc.maxFolForce;
    }

    protected float CalculateDisFrom(GameObject entity)
    {
        Vector2 entityPos = new Vector2(entity.transform.position.x, entity.transform.position.z);
        Vector2 pos = new Vector2(transform.position.x, transform.position.z);
        return (entityPos - pos).magnitude;
    }

    public abstract void FixedUpdate(float fixedDeltaTime);

    public abstract void ScriptReset();

    protected abstract Vector2 SteeringForce();
}

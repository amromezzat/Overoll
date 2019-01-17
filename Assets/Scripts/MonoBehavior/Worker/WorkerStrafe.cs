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

public class WorkerStrafe : IWStrafe
{
    //external references
    LanesDatabase lanes;
    Transform transform;
    Animator animator;
    float strafeDuration;

    bool strafing = false;
    float strafeTimer = 0;

    public WorkerStrafe(LanesDatabase lanes, Animator animator, Transform transform,float strafeDuration){
        this.lanes = lanes;
        this.animator = animator;
        this.transform = transform;
        this.strafeDuration = strafeDuration;
    }

    public virtual void StrafeRight()
    {
        if (!strafing)
        {
            strafeTimer = 0;
            animator.SetBool("StrafeRightAnim", true);
            lanes.GoRight();
            strafing = true;
        }
    }

    public virtual void StrafeLeft()
    {
        if (!strafing)
        {
            strafeTimer = 0;
            animator.SetBool("StrafeLeftAnim", true);
            lanes.GoLeft();
            strafing = true;
        }
    }


    public void FixedUpdate(float fixedDeltaTime)
    {
        if (strafing)
        {
            float completedPortion = strafeTimer / strafeDuration;
            float squarePortion = completedPortion * completedPortion;
            float xPos = Mathf.Lerp(transform.position.x, lanes.CurrentLane.laneCenter, squarePortion);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            strafeTimer += fixedDeltaTime;
            if (squarePortion >= 1)
            {
                strafing = false;
                animator.SetBool("StrafeRightAnim", false);
                animator.SetBool("StrafeLeftAnim", false);
            }
        }
    }

    public virtual void ScriptReset()
    {
        strafing = false;
    }
}

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

public class Jump : IDoAction
{
    public float jumpDuration;
    public float jumpHeight;

    float jumpTimer;

    Animator animator;

    public Jump(Animator animator)
    {
        this.animator = animator;
    }

    public void OnStateEnter(Animator animator)
    {
        this.animator = animator;
        animator.SetBool("Jumping", true);
        AudioManager.instance.PlaySound("WorkerJump");

        jumpTimer = 0;
    }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        Vector3 newPos = transform.position;
        jumpTimer += deltaTime;
        float completedPortion = jumpTimer / jumpDuration;
        animator.SetFloat("JumpingRatio", completedPortion);
        newPos.y = Mathf.Lerp(0.25f, jumpHeight, Mathf.Sin(Mathf.PI * completedPortion));
        transform.position = newPos;
        if (transform.position.y <= 0.25)
        {
            return false;
        }
        return true;
    }

    public void OnStateExit(Animator animator)
    {
        animator.SetBool("Jumping", false);
    }
}

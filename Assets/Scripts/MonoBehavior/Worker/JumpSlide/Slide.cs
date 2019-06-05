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

public class Slide : IDoAction
{
    public float slideDuration;

    BoxCollider collider;
    float slideTimer = 0;

    Animator animator;

    GameObject shadow;

    public Slide(BoxCollider _collider, Animator animator, GameObject shadow)
    {
        collider = _collider;
        this.animator = animator;
        this.shadow = shadow;
    }

    public void OnStateEnter(Animator animator)
    {
        slideTimer = 0;
        animator.SetTrigger("Duck");

        AudioManager.Instance.PlaySound("Roll");

        Vector3 newColliderSize = collider.size;
        newColliderSize.y *= 0.25f;
        collider.size = newColliderSize;
        Vector3 colliderNewPos = collider.center;
        colliderNewPos.y *= 0.25f;
        collider.center = colliderNewPos;
        shadow.SetActive(false);
    }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        slideTimer += deltaTime;
        if (slideTimer >= slideDuration)
        {
            return false;
        }
        return true;
    }

    public void OnStateExit(Animator animator)
    {
        Vector3 newColliderSize = collider.size;
        newColliderSize.y *= 4;
        collider.size = newColliderSize;
        Vector3 colliderNewPos = collider.center;
        colliderNewPos.y *= 4;
        collider.center = colliderNewPos;
        shadow.SetActive(true);
    }
}

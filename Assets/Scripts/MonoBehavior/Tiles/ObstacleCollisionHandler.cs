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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileReturner))]
public class ObstacleCollisionHandler : MonoBehaviour, IObstacle
{
    public int obsHealth;
    [HideInInspector]
    public int runtimeObsHealth;
    int currentHealth;
    TileReturner objReturner;
    HealthState obstacleState = HealthState.Healthy;
    public WorkerCollidingEffect collidingEffect;

    private void Awake()
    {
        objReturner = GetComponent<TileReturner>();
        runtimeObsHealth = obsHealth;
    }

    public virtual void ReactToCollision(int collidedHealth)
    {
        currentHealth -= collidedHealth;
        if (obsHealth <= 0)
        {
            obstacleState = HealthState.Wrecked;
            currentHealth = obsHealth;
            StartCoroutine(objReturner.ReturnToPool(0));
        }
        else
        {
            obstacleState = HealthState.Fractured;
        }
    }

    public int Gethealth()
    {
        return obsHealth;
    }

    public void PlayEffect(Animator animator, Rigidbody rb)
    {
        if (collidingEffect != null)
            collidingEffect.PlayEffect(animator, rb);
    }
}

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

public class CoinMagnet : MonoBehaviour
{
    public WorkerConfig wc;

    TileReturner tileReturner;

    Vector3 coinPos;
    Transform playerTrans;
    float lerpFac;
    float totalTime = 0.5f;
    float currentTimer = 0;
    bool collided = false;
    float timerCoolDown;

    const float cdBeforeCollision = 0.3f;

    void Awake()
    {
        tileReturner = GetComponent<TileReturner>();
        timerCoolDown = cdBeforeCollision;
    }

    void OnEnable()
    {
        collided = false;
        currentTimer = 0;
        timerCoolDown = cdBeforeCollision;
        Vector3 fixedYPos = transform.position;
        fixedYPos.y = wc.groundLevel;
        transform.position = fixedYPos;
    }

    void Update()
    {
        if (collided)
        {
            currentTimer += Time.deltaTime;
            lerpFac = currentTimer / totalTime;
            // Move coin to worker position
            transform.position = Vector3.Lerp(coinPos, playerTrans.position, lerpFac);
            if(lerpFac > 1)
            {
                StartCoroutine(tileReturner.ReturnToPool(0));
            }
        }
        else
        {
            timerCoolDown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (timerCoolDown > 0)
        {
            return;
        }
        if (other.gameObject.CompareTag("PowerUpCollider"))
        {
            collided = true;
            playerTrans = other.transform.parent;
            coinPos = transform.position;
        }
    }
}

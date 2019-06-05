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

[RequireComponent(typeof(TileMover))]
public abstract class TileExtraAction : MonoBehaviour
{
    // Activation position infront of the player
    [SerializeField, Tooltip("Activation position infront of the player")]
    protected float relActivPos;

    protected TileMover tileMover;

    protected bool actionInitiated;

    Coroutine takeAction;

    protected virtual void Awake()
    {
        tileMover = GetComponent<TileMover>();

        SpeedManager.Instance.speed.onValueChanged.AddListener((speed) =>
        {
            if(isActiveAndEnabled)
                StartAction();
        });
    }

    protected virtual void OnEnable()
    {
        actionInitiated = false;
        StartAction();
    }

    void StartAction()
    {
        if (actionInitiated)
            return;

        if (takeAction != null)
            StopCoroutine(takeAction);

        takeAction = StartCoroutine(TakeActionCoroutine());
    }

    protected virtual IEnumerator TakeActionCoroutine()
    {
        yield return new WaitWhile(() => SpeedManager.Instance.speed < Mathf.Epsilon);
        yield return new WaitWhile(() => TutorialManager.Instance.Active);

        float waitingTime = (transform.position.z - relActivPos) / SpeedManager.Instance.speed.Value;
        while (waitingTime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitWhile(() => SpeedManager.Instance.speed < Mathf.Epsilon);
            waitingTime -= 0.1f;
        }
    }
}

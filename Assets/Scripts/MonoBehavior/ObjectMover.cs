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
/// This class resposiple for moving the tile
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class ObjectMover : MonoBehaviour, IHalt, IChangeSpeed
{
    public TileConfig tc;

    [HideInInspector]
    public Rigidbody rb;

    bool isKillingSpeed = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        RegisterListeners();
    }

    protected virtual void OnEnable()
    {
        if (GameManager.Instance.gameState == GameState.Gameplay)
        {
            MoveObj();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        if (TutorialManager.Instance.tutorialActive && TutorialManager.Instance.TutorialState != TutorialState.Null)
        {
            isKillingSpeed = true;
        }
    }

    protected virtual void Update()
    {
        if (isKillingSpeed)
        {
            if (Mathf.Abs(rb.velocity.z) > 0.01f)
            {
                rb.velocity = Vector3.back * SpeedManager.Instance.speed.Value;
                SetAnimatorsSpeed(SpeedManager.Instance.speed.Value / SpeedManager.Instance.speed.OldValue);
            }
            else
            {
                rb.velocity = Vector3.zero;
                SetAnimatorsSpeed(0);
                isKillingSpeed = false;
            }
        }
    }

    protected virtual void MoveObj()
    {
        rb.velocity = Vector3.back * SpeedManager.Instance.speed.Value;
        SetAnimatorsSpeed(1);
    }

    public virtual void Halt()
    {
        rb.velocity = Vector3.zero;
        SetAnimatorsSpeed(0);
    }

    public void Resume()
    {
        MoveObj();
    }

    public virtual void RegisterListeners()
    {
        GameManager.Instance.OnStart.AddListener(Begin);
        GameManager.Instance.onPause.AddListener(Halt);
        GameManager.Instance.OnResume.AddListener(Resume);
        //GameManager.Instance.onEnd.AddListener(End);
        TutorialManager.Instance.onSlowDown.AddListener(SlowDown);
        TutorialManager.Instance.onSpeedUp.AddListener(SpeedUp);
    }

    public void Begin()
    {
        MoveObj();
    }

    protected abstract void SetAnimatorsSpeed(float speed);

    public void SpeedUp()
    {
        MoveObj();
        isKillingSpeed = false;
    }

    public void SlowDown()
    {
        isKillingSpeed = true;
    }

    public void End()
    {
        throw new System.NotImplementedException();
    }
}

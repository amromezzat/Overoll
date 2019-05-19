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

[RequireComponent(typeof(Rigidbody))]
public class TileMover : ObjectMover
{
    public TileExtraAction tileExtraAction;

    public Animator Anim;

    [HideInInspector]
    public float extraSpeed;

    public float Velocity
    {
        get
        {
            return -rb.velocity.z;
        }
        set
        {
            rb.velocity = Vector3.back * value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        TakeExtraAction();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected virtual void OnDisable()
    {
        if (Anim != null)
            Anim.SetTrigger("Reset");
    }

    protected override void SetVelocity(float speed)
    {
        Velocity = speed > Mathf.Epsilon ? speed + extraSpeed : 0;
    }

    protected virtual void TakeExtraAction()
    {
        if (isActiveAndEnabled && tileExtraAction != null)
        {
            tileExtraAction.Begin(this);
        }
    }

    protected override void SetAnimatorsSpeed(float speed)
    {
        if (Anim != null)
            Anim.speed = speed;
    }
}

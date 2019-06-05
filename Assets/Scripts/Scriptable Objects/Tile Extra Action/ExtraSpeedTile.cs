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

public class ExtraSpeedTile : TileExtraAction
{
    [SerializeField]
    float ExtraVelocity;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void OnDisable()
    {
        tileMover.extraSpeed = 0;
        tileMover.Velocity -= ExtraVelocity;
    }

    /// <summary>
    /// Take extra speed when reaching workers;
    /// object stays static relative to other objects
    /// until it is close enough to workers
    /// </summary>
    protected override IEnumerator TakeActionCoroutine()
    {
        yield return base.TakeActionCoroutine();
        tileMover.Anim.SetTrigger("Rotate Spool");
        tileMover.extraSpeed = ExtraVelocity;
        tileMover.Velocity += ExtraVelocity;
        tileMover.Anim.speed = 1;

        actionInitiated = true;
    }
}

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
public abstract class ObjectMover : MonoBehaviour
{
    protected virtual void Update()
    {
        transform.position += Vector3.back * SpeedManager.Instance.speed;
        SetAnimatorsSpeed(SpeedManager.Instance.speed.Value / SpeedManager.Instance.speed.OldValue);
    }

    protected abstract void SetAnimatorsSpeed(float speed);
}

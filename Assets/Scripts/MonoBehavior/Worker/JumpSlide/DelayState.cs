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

public class DelayState : IDoAction
{
    float delay;

    public float Delay
    {
        get
        {
            return delay;
        }

        set
        {
            delay = value;
        }
    }

    public void OnStateEnter(Animator animator)
    { }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        delay -= deltaTime;
        if(delay <= 0)
        {
            return false;
        }
        return true;
    }

    public void OnStateExit(Animator animator)
    { }
}

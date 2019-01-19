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
/// Strafe left or right when the tutorial state is only equal to the input
/// </summary>
public class TutWorkerStrafe : WorkerStrafe, IWChangeState
{
    GameData gd;
    bool tutRightAct = false;

    public TutWorkerStrafe(LanesDatabase lanes, Animator animator, Transform transform,
        float strafeDuration, GameData gd) : base(lanes, animator, transform, strafeDuration)
    {
        this.gd = gd;
    }

    public override void ScriptReset()
    {
        base.ScriptReset();
        tutRightAct = false;
    }

    public override void StrafeRight()
    {
        if (TutorialManager.Instance.TutorialState == TutorialState.RightStrafe)
        {
            tutRightAct = true;
            base.StrafeRight();
        }
    }

    public override void StrafeLeft()
    {
        if (TutorialManager.Instance.TutorialState == TutorialState.LeftStrafe)
        {
            tutRightAct = true;
            base.StrafeLeft();
        }
    }

    public WorkerStateTrigger InputTrigger()
    {
        if (tutRightAct)
        {
            tutRightAct = false;
            return WorkerStateTrigger.StateEnd;
        }
        return WorkerStateTrigger.Null;
    }
}

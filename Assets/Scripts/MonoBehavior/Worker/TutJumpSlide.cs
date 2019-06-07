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

using UnityEngine;

/// <summary>
/// Jump and slide when only the tutorial of the state is equal to the input
/// </summary>
public class TutJumpSlide : JumpSlideFSM, IWChangeState
{
    bool tutRightAct = false;

    public TutJumpSlide(WorkerConfig wc, BoxCollider mCollider, Animator mAnimator, Transform transform, 
        GameObject shadow, WorkerFSM workerController) : base(wc, mCollider, mAnimator, transform, shadow, workerController)
    {
    }

    public override void ScriptReset()
    {
        base.ScriptReset();
        tutRightAct = false;
    }

    public override void Jump()
    {
        if (TutorialManager.Instance.TutorialState == TutorialState.Jump)
        {
            tutRightAct = true;
            base.Jump();
        }
    }

    public override void Slide()
    {
        if (TutorialManager.Instance.TutorialState == TutorialState.Slide)
        {
            tutRightAct = true;
            base.Slide();
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

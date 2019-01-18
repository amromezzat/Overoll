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

public class JumpSlideFSM : IWJumpSlide
{
    //external references
    WorkerConfig wc;
    protected GameData gd;
    BoxCollider mCollider;
    Animator mAnimator;
    Transform transform;

    Slide slideState;
    Jump jumpState;
    Run runState = new Run();
    InterruptJump interruptJumpState = new InterruptJump();
    DelayState delayState = new DelayState();
    // Dictionary for allowed transitions from a certain state
    Dictionary<IDoAction, List<IDoAction>> actionsDic = new Dictionary<IDoAction, List<IDoAction>>();
    Stack<IDoAction> actionStack = new Stack<IDoAction>();

    IDoAction currentState;

    public JumpSlideFSM(WorkerConfig wc, GameData gd, BoxCollider mCollider, Animator mAnimator, Transform transform)
    {
        this.wc = wc;
        this.gd = gd;
        this.mCollider = mCollider;
        this.mAnimator = mAnimator;
        this.transform = transform;
        InitializeFSM();
    }

    void InitializeFSM()
    {
        slideState = new Slide(mCollider)
        {
            slideDuration = wc.slideDuration
        };
        jumpState = new Jump
        {
            jumpDuration = wc.jumpDuration,
            jumpHeight = wc.jumpHeight
        };

        //allowed transition states
        actionsDic[slideState] = new List<IDoAction>() { runState, jumpState };
        actionsDic[jumpState] = new List<IDoAction>() { runState, interruptJumpState };
        actionsDic[runState] = new List<IDoAction>() { runState, jumpState, slideState, delayState };
        actionsDic[interruptJumpState] = new List<IDoAction>() { runState, slideState, delayState };
        actionsDic[delayState] = new List<IDoAction>() { jumpState, slideState };
    }

    public virtual void ScriptReset()
    {
        actionStack = new Stack<IDoAction>();
        actionStack.Push(runState);
        currentState = runState;
    }

    //change to a new state after concluding the current one
    void ChangeState(IDoAction nextState)
    {
        if (actionsDic[currentState].Contains(nextState))
        {
            currentState.OnStateExit(mAnimator);
            nextState.OnStateEnter(mAnimator);
            currentState = nextState;
        }
    }

    public virtual void Jump()
    {
        //float delayTime = (wc.leader.transform.position.z - transform.position.z) / gd.Speed;
        float delayTime = (wc.leader.transform.position.z - transform.position.z) / SpeedManager.Instance.speed.Value;
        actionStack.Push(jumpState);
        if (delayTime > 0)
        {
            delayState.Delay = delayTime;
            actionStack.Push(delayState);
        }
        ChangeState(actionStack.Pop());
    }

    public virtual void Slide()
    {
        //if jumping interrupt jump to return worker to ground and then slide
        //float delayTime = (wc.leader.transform.position.z - transform.position.z) / gd.Speed;
        float delayTime = (wc.leader.transform.position.z - transform.position.z) / SpeedManager.Instance.speed.Value;

        if (currentState == jumpState)
        {
            actionStack.Push(slideState);
            actionStack.Push(interruptJumpState);
        }
        else
        {
            actionStack.Push(slideState);
        }

        if (delayTime > 0)
        {
            delayState.Delay = delayTime;
            actionStack.Push(delayState);
        }
        ChangeState(actionStack.Pop());
    }

    public void FixedUpdate(float fixedDeltaTime)
    {
        bool executing = currentState.OnStateExecution(transform, fixedDeltaTime);
        if (!executing)
        {
            IDoAction nextState = actionStack.Pop();
            if (nextState == runState)
            {
                actionStack.Push(runState);
            }
            ChangeState(nextState);
        }
    }
}

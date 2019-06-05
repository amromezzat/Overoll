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
    //protected GameData gd;
    BoxCollider mCollider;
    Animator mAnimator;
    Transform transform;

    GameObject shadow;

    Slide slideState;
    Jump jumpState;
    Run runState;
    InterruptJump interruptJumpState = new InterruptJump();
    DelayState delayState = new DelayState();

    // Dictionary for allowed transitions from a certain state
    Dictionary<IDoAction, List<IDoAction>> actionsDic = new Dictionary<IDoAction, List<IDoAction>>();

    Queue<IDoAction> actionQueue = new Queue<IDoAction>();
    IDoAction currentState;

    public JumpSlideFSM(WorkerConfig wc, BoxCollider mCollider, Animator mAnimator, Transform transform, GameObject mShadow)
    {
        this.wc = wc;
        //this.gd = gd;
        this.mCollider = mCollider;
        this.mAnimator = mAnimator;
        this.transform = transform;
        this.shadow = mShadow;
        InitializeFSM();
    }

    void InitializeFSM()
    {
        runState = new Run()
        {
            jsFSM = this
        };

        slideState = new Slide(mCollider, mAnimator, shadow)
        {
            slideDuration = wc.slideDuration
        };

        jumpState = new Jump(mAnimator)
        {
            jumpDuration = wc.jumpDuration,
            jumpHeight = wc.jumpHeight
        };

        //allowed transition states
        actionsDic[slideState] = new List<IDoAction>() { runState, jumpState, delayState };
        actionsDic[jumpState] = new List<IDoAction>() { interruptJumpState, runState };
        actionsDic[runState] = new List<IDoAction>() { runState, jumpState, slideState, delayState, interruptJumpState };
        actionsDic[interruptJumpState] = new List<IDoAction>() { runState, slideState };
        actionsDic[delayState] = new List<IDoAction>() { jumpState, slideState, runState };
    }

    public void Enqueue(IDoAction state)
    {
        actionQueue.Enqueue(state);
    }

    public virtual void ScriptReset()
    {
        actionQueue = new Queue<IDoAction>();
        actionQueue.Enqueue(runState);
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

    void ChangeState(Stack<IDoAction> nextStates)
    {
        if (actionsDic[currentState].Contains(nextStates.Peek()))
        {
            currentState.OnStateExit(mAnimator);
            currentState = nextStates.Pop();
            currentState.OnStateEnter(mAnimator);
        }

        while (nextStates.Count > 0)
            actionQueue.Enqueue(nextStates.Pop());
    }

    public virtual void Jump()
    {
        float delayTime = (WorkersManager.Instance.leader.transform.position.z - transform.position.z) / SpeedManager.Instance.speed.Value;

        Stack<IDoAction> nextActions = new Stack<IDoAction>();

        nextActions.Push(interruptJumpState);
        nextActions.Push(jumpState);

        if (delayTime > Mathf.Epsilon)
        {
            delayState.Delay = delayTime;
            nextActions.Push(delayState);
        }

        ChangeState(nextActions);
    }

    public virtual void Slide()
    {
        // Delay time before reaching leader position
        float delayTime = (WorkersManager.Instance.leader.transform.position.z - transform.position.z) / SpeedManager.Instance.speed.Value;

        Stack<IDoAction> nextActions = new Stack<IDoAction>();

        nextActions.Push(slideState);

        if (currentState == jumpState)
            nextActions.Push(interruptJumpState);

        if (delayTime > Mathf.Epsilon)
        {
            delayState.Delay = delayTime;
            nextActions.Push(delayState);
        }

        ChangeState(nextActions);
    }

    public void FixedUpdate(float fixedDeltaTime)
    {
        //Debug.Log("List: " + string.Join(", ", actionQueue));

        bool executing = currentState.OnStateExecution(transform, fixedDeltaTime);
        if (!executing)
        {
            IDoAction nextState = actionQueue.Dequeue();
            ChangeState(nextState);
        }
    }
}

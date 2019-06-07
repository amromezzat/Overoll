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
    // External references
    WorkerConfig wc;
    //protected GameData gd;
    BoxCollider mCollider;
    Animator mAnimator;
    Transform transform;
    WorkerFSM workerController;

    GameObject shadow;

    Slide slideState;
    Jump jumpState;
    Run runState;
    InterruptJump interruptJumpState = new InterruptJump();

    Vector3 colliderSize;
    Vector3 colliderPosition;
    float yPos;

    // Dictionary for allowed transitions from a certain state
    Dictionary<IDoAction, List<IDoAction>> actionsDic = new Dictionary<IDoAction, List<IDoAction>>();

    Queue<IDoAction> actionQueue = new Queue<IDoAction>();
    IDoAction currentState;

    public JumpSlideFSM(WorkerConfig wc, BoxCollider mCollider, Animator mAnimator,
        Transform transform, GameObject mShadow, WorkerFSM workerController)
    {
        this.wc = wc;
        //this.gd = gd;
        this.mCollider = mCollider;
        this.mAnimator = mAnimator;
        this.transform = transform;
        this.shadow = mShadow;
        this.workerController = workerController;
        InitializeFSM();
    }

    void InitializeFSM()
    {
        colliderPosition = mCollider.center;
        colliderSize = mCollider.size;
        yPos = transform.position.y;

        runState = new Run();

        slideState = new Slide(mCollider, mAnimator, shadow)
        {
            slideDuration = wc.slideDuration
        };

        jumpState = new Jump(mAnimator)
        {
            jumpDuration = wc.jumpDuration,
            jumpHeight = wc.jumpHeight
        };

        // Allowed transition states
        actionsDic[slideState] = new List<IDoAction>() { runState, jumpState };
        actionsDic[jumpState] = new List<IDoAction>() { interruptJumpState };
        actionsDic[runState] = new List<IDoAction>() { jumpState, slideState, runState };
        actionsDic[interruptJumpState] = new List<IDoAction>() { slideState, runState };
    }

    public virtual void ScriptReset()
    {
        mCollider.center = colliderPosition;
        mCollider.size = colliderSize;
        Vector3 pos = transform.position;
        pos.y = yPos;
        transform.position = pos;

        actionQueue = new Queue<IDoAction>();
        currentState = runState;
    }

    // Change to a new state after concluding the current one
    bool ChangeState(IDoAction nextState)
    {
        if (actionsDic[currentState].Contains(nextState))
        {
            currentState.OnStateExit(mAnimator);
            nextState.OnStateEnter(mAnimator);
            currentState = nextState;
            return true;
        }
        return false;
    }

    void ChangeState(Queue<IDoAction> nextStates)
    {
        bool changeAllowed = ChangeState(nextStates.Dequeue());

        if (changeAllowed)
            actionQueue = nextStates;
    }

    public virtual void Jump()
    {
        Queue<IDoAction> nextActions = new Queue<IDoAction>();

        nextActions.Enqueue(jumpState);
        nextActions.Enqueue(interruptJumpState);
        nextActions.Enqueue(runState);

        workerController.StartCoroutine(DelayedAction(() => ChangeState(nextActions)));
    }

    public virtual void Slide()
    {
        Queue<IDoAction> nextActions = new Queue<IDoAction>();

        if (currentState == jumpState)
            nextActions.Enqueue(interruptJumpState);

        nextActions.Enqueue(slideState);
        nextActions.Enqueue(runState);

        workerController.StartCoroutine(DelayedAction(() => ChangeState(nextActions)));
    }

    IEnumerator DelayedAction(System.Action action)
    {
        // Delay time before reaching leader position
        float delayTime = WorkersManager.Instance.leader.transform.position.z - transform.position.z;
        delayTime /= SpeedManager.Instance.speed.Value > Mathf.Epsilon ? SpeedManager.Instance.speed.Value : 1;

        yield return new WaitForSeconds(delayTime);
        action();
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

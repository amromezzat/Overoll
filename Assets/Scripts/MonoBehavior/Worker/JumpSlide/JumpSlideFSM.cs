using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSlideFSM : IWJumpSlide
{
    //external references
    WorkerConfig wc;
    TileConfig tc;
    BoxCollider mCollider;
    Animator mAnimator;
    Transform transform;

    Slide slideState;
    Jump jumpState;
    Run runState = new Run();
    InterruptJump interruptJumpState = new InterruptJump();
    DelayState delayState = new DelayState();
    Dictionary<IDoAction, List<IDoAction>> actionsDic = new Dictionary<IDoAction, List<IDoAction>>();
    Stack<IDoAction> actionStack = new Stack<IDoAction>();

    IDoAction currentState;
    public string currentStateStr;


    public JumpSlideFSM(WorkerConfig wc, TileConfig tc, BoxCollider mCollider, Animator mAnimator, Transform transform)
    {
        this.wc = wc;
        this.tc = tc;
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

    public void ScriptReset()
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

    public void Jump()
    {
        float delayTime = (wc.leader.transform.position.z - transform.position.z) / tc.tileSpeed;
        actionStack.Push(jumpState);
        if (delayTime > 0)
        {
            delayState.Delay = delayTime;
            actionStack.Push(delayState);
        }
        ChangeState(actionStack.Pop());
        //FindObjectOfType<AudioManager>().PlaySound("WorkerJump");
    }

    public void Slide()
    {
        float delayTime = (wc.leader.transform.position.z - transform.position.z) / tc.tileSpeed;
        //if jumping interrupt jump and slide
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
        currentStateStr = currentState.ToString();
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

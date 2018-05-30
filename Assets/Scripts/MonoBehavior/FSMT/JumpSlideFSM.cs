using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSlideFSM : MonoBehaviour, iHalt
{

    public WorkerConfig wc;
    public TileConfig tc;
    public GameData gd;

    Slide slideState;
    Jump jumpState = new Jump();
    Run runState = new Run();
    InterruptJump InterruptJumpState = new InterruptJump();
    HaltState haltState = new HaltState();
    DelayState delayState = new DelayState();
    Dictionary<IDoAction, List<IDoAction>> actionsDic = new Dictionary<IDoAction, List<IDoAction>>();
    Stack<IDoAction> actionStack = new Stack<IDoAction>();

    IDoAction currentState;
    public string currentStateStr;

    BoxCollider mCollider;
    Animator mAnimator;

    // Use this for initialization
    void Start()
    {

        mCollider = GetComponent<BoxCollider>();
        mAnimator = GetComponent<Animator>();

        slideState = new Slide(mCollider);
        actionsDic[slideState] = new List<IDoAction>() { runState, jumpState };
        actionsDic[jumpState] = new List<IDoAction>() { runState, InterruptJumpState };
        actionsDic[runState] = new List<IDoAction>() { runState, jumpState, slideState };
        actionsDic[InterruptJumpState] = new List<IDoAction>() { runState, slideState };
        actionsDic[haltState] = new List<IDoAction>() { runState, jumpState, slideState };
        actionsDic[delayState] = new List<IDoAction>() { runState, jumpState, slideState };
        actionStack.Push(runState);

        currentState = runState;
        RegisterListeners();
    }

    // Update is called once per frame
    void Update()
    {
        currentStateStr = currentState.ToString();
        bool stateEnded = currentState.OnStateExecution(transform, Time.deltaTime);
        if (stateEnded)
        {
            IDoAction nextState = actionStack.Pop();
            if (nextState == runState)
            {
                actionStack.Push(runState);
            }
            ChangeState(nextState);
        }
    }

    void ChangeState(IDoAction nextState)
    {
        if (actionsDic[currentState].Contains(nextState))
        {
            currentState.OnStateExit(mAnimator);
            nextState.OnStateEnter(mAnimator);
            currentState = nextState;
        }
    }

    void Jump()
    {
        ChangeState(jumpState);
    }

    void Slide()
    {
        float delayTime = (wc.leader.transform.position.z - transform.position.z) / tc.tileSpeed;
        if(delayTime > 0)
        {
            delayState.Delay = delayTime;
            actionStack.Push(delayState);
        }
        if (currentState == jumpState)
        {
            actionStack.Push(InterruptJumpState);
            actionStack.Push(slideState);
        }
        else
        {
            actionStack.Push(slideState);
        }
        ChangeState(actionStack.Pop());
    }

    public void Halt()
    {
        wc.onJump.RemoveListener(Jump);
        wc.onSlide.RemoveListener(Slide);
    }

    public void Resume()
    {
        wc.onJump.AddListener(Jump);
        wc.onSlide.AddListener(Slide);
    }

    public void RegisterListeners()
    {
        gd.OnStart.AddListener(Halt);
        gd.onPause.AddListener(Halt);
        gd.OnResume.AddListener(Resume);
    }
}

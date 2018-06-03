using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionState
{
    RUNNING,
    FINISHED,
    INTERRUPTED
}

public class JumpSlideFSM : MonoBehaviour, iHalt
{

    public WorkerConfig wc;
    public TileConfig tc;
    public GameData gd;

    Slide slideState;
    Jump jumpState = new Jump();
    Run runState = new Run();
    InterruptJump interruptJumpState = new InterruptJump();
    HaltState haltState = new HaltState();
    DelayState delayState = new DelayState();
    Dictionary<IDoAction, List<IDoAction>> actionsDic = new Dictionary<IDoAction, List<IDoAction>>();
    Stack<IDoAction> actionStack = new Stack<IDoAction>();

    IDoAction currentState;
    public string currentStateStr;

    BoxCollider mCollider;
    Animator mAnimator;

    void Awake()
    {
        mCollider = GetComponent<BoxCollider>();
        mAnimator = GetComponent<Animator>();

        slideState = new Slide(mCollider);
        actionsDic[slideState] = new List<IDoAction>() { runState, jumpState, haltState };
        actionsDic[jumpState] = new List<IDoAction>() { runState, interruptJumpState, haltState };
        actionsDic[runState] = new List<IDoAction>() { runState, jumpState, slideState, haltState, delayState };
        actionsDic[interruptJumpState] = new List<IDoAction>() { runState, slideState, haltState, delayState };
        actionsDic[haltState] = new List<IDoAction>() { runState, jumpState, slideState };
        actionsDic[delayState] = new List<IDoAction>() { jumpState, slideState, haltState };
        actionStack.Push(runState);

        currentState = runState;

        RegisterListeners();
    }

    private void OnEnable()
    {
        if(gd.gameState == GameState.Gameplay)
        {
            mAnimator.SetBool("RunAnim", true);
        }
    }

    void Update()
    {
        currentStateStr = currentState.ToString();
        ActionState currentStateState = currentState.OnStateExecution(transform, Time.deltaTime);
        if (currentStateState == ActionState.FINISHED)
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
        if (!isActiveAndEnabled)
        {
            return;
        }
        float delayTime = (wc.leader.transform.position.z - transform.position.z) / tc.tileSpeed;
        actionStack.Push(jumpState);
        if (delayTime > 0)
        {
            if (currentState == delayState && actionStack.Peek() == slideState)
            {
                ((Slide)actionStack.Peek()).interruptTime = delayTime;
            }
            else
            {
                delayState.Delay = delayTime;
                actionStack.Push(delayState);
            }
        }
        ChangeState(actionStack.Pop());
    }

    void Slide()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        float delayTime = (wc.leader.transform.position.z - transform.position.z) / tc.tileSpeed;
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
            if (currentState == delayState && actionStack.Peek() == jumpState)
            {
                ((Jump)actionStack.Peek()).interruptTime = delayTime;
            }
            else
            {
                delayState.Delay = delayTime;
                actionStack.Push(delayState);
            }
        }
        ChangeState(actionStack.Pop());
    }

    public void RegisterListeners()
    {
        gd.OnStart.AddListener(Begin);
        gd.onPause.AddListener(Halt);
        gd.OnResume.AddListener(Resume);
        gd.onEnd.AddListener(End);
    }

    public void Begin()
    {
        if(isActiveAndEnabled)
            StartCoroutine(StartRunning());
        wc.onJump.AddListener(Jump);
        wc.onSlide.AddListener(Slide);
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
    
    public void End()
    {
        wc.onJump.RemoveListener(Jump);
        wc.onSlide.RemoveListener(Slide);
    }

    IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(4);
        mAnimator.SetBool("RunAnim", true);
    }
}

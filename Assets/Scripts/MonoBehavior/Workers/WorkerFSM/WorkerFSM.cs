using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectReturner))]
public class WorkerFSM : MonoBehaviour, iHalt
{
    public WorkerConfig wc;
    public TileConfig tc;
    public LanesDatabase lanes;
    public GameData gd;

    Animator mAnimator;
    BoxCollider mCollider;
    ObjectReturner workerReturner;
    Rigidbody rb;

    WorkerStrafe workerStrafe;
    JumpSlideFSM jumpSlideFsm;
    WorkerCollide workerCollide;

    IWorkerScript[] workerScripts;
    Dictionary<WorkerState, StateScriptsWrapper> workerStateScripts = new Dictionary<WorkerState, StateScriptsWrapper>();
    WorkerStateTransition workerStateTransition = new WorkerStateTransition();

    [SerializeField]
    WorkerState currentState;
    [SerializeField]
    WorkerState haltedState;
    public int health = 1;

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
        mCollider = GetComponent<BoxCollider>();
        workerReturner = GetComponent<ObjectReturner>();
        rb = GetComponent<Rigidbody>();

        wc.onLeft.AddListener(StrafeLeft);
        wc.onRight.AddListener(StrafeRight);
        wc.onJump.AddListener(Jump);
        wc.onSlide.AddListener(Slide);

        RegisterListeners();
        SetStatesScripts();
    }

    private void OnEnable()
    {
        for (int i = 0; i < workerScripts.Length; i++)
        {
            workerScripts[i].ScriptReset();
        }
        if (gd.gameState == GameState.Gameplay)
        {
            currentState = WorkerState.Worker;
            mAnimator.SetBool("RunAnim", true);
        }
        else if (gd.gameState == GameState.MainMenu)
        {
            haltedState = WorkerState.Leader;
            currentState = WorkerState.Halted;
        }
    }

    private void OnDisable()
    {
        currentState = WorkerState.Dead;
        haltedState = WorkerState.Dead;
    }

    void SetStatesScripts()
    {
        workerStrafe = new WorkerStrafe(lanes, mAnimator, transform, wc.strafeDuration);
        jumpSlideFsm = new JumpSlideFSM(wc, tc, mCollider, mAnimator, transform);
        workerCollide = new WorkerCollide(mAnimator, rb, tc);

        workerScripts = new IWorkerScript[] { workerStrafe, jumpSlideFsm, workerCollide };

        workerStateScripts[WorkerState.Leader] = new StateScriptsWrapper(new List<IWorkerScript>() { workerStrafe, jumpSlideFsm, workerCollide },
            workerStrafe, jumpSlideFsm, workerCollide);

        workerStateScripts[WorkerState.Worker] = new StateScriptsWrapper(new List<IWorkerScript>());
        workerStateScripts[WorkerState.Dead] = new StateScriptsWrapper(new List<IWorkerScript>());
        workerStateScripts[WorkerState.Halted] = new StateScriptsWrapper(new List<IWorkerScript>());
    }

    void StrafeLeft()
    {
        workerStateScripts[currentState].StrafeLeft();
    }

    void StrafeRight()
    {
        workerStateScripts[currentState].StrafeRight();
    }

    void Jump()
    {
        workerStateScripts[currentState].Jump();
    }

    void Slide()
    {
        workerStateScripts[currentState].Slide();
    }

    private void OnTriggerEnter(Collider other)
    {
        WorkerStateTrigger trigger = workerStateScripts[currentState].Collide(other, ref health);
        if (trigger != WorkerStateTrigger.Null)
        {
            TransitionBundle transition = workerStateTransition.ChangeState(trigger, currentState);
            currentState = transition.Destination;
            Output(transition.Output);
        }
    }

    void Output(WorkerFSMOutput outputKey)
    {
        switch (outputKey)
        {
            case WorkerFSMOutput.LeaderDied:
                wc.workers.Remove(gameObject);
                wc.onLeaderDeath.Invoke();
                StartCoroutine(workerReturner.ReturnToPool(2));
                break;
            case WorkerFSMOutput.WorkerDied:
                wc.workers.Remove(gameObject);
                StartCoroutine(workerReturner.ReturnToPool(2));
                break;
            case WorkerFSMOutput.WorkerRevived:
                wc.workers.Add(gameObject);
                break;
            case WorkerFSMOutput.SlaveMerged:
                break;
            case WorkerFSMOutput.LeaderElected:
                break;
        }
    }

    private void Update()
    {
        WorkerStateTrigger trigger = workerStateScripts[currentState].InputTrigger();
        if (trigger != WorkerStateTrigger.Null)
        {
            TransitionBundle transition = workerStateTransition.ChangeState(trigger, currentState);
            currentState = transition.Destination;
            Output(transition.Output);
        }
    }

    private void FixedUpdate()
    {
        foreach (IWorkerScript script in workerStateScripts[currentState])
        {
            script.FixedUpdate(Time.fixedDeltaTime);
        }
    }

    public void Begin()
    {
        if (gameObject.activeSelf)
            mAnimator.SetBool("RunAnim", true);
        currentState = haltedState;
    }

    public void Halt()
    {
        if (gameObject.activeSelf)
        {
            if (currentState == WorkerState.Dead)
                StartCoroutine(workerReturner.ReturnToPool(0));

            mAnimator.speed = 0;
            haltedState = currentState;
            currentState = WorkerState.Halted;
            rb.velocity = Vector3.zero;
        }
    }

    public void Resume()
    {
        if (gameObject.activeSelf)
        {
            mAnimator.speed = 1;
            currentState = haltedState;
        }
    }

    public void End()
    {
        rb.velocity = Vector3.zero;
    }

    public void RegisterListeners()
    {
        gd.OnStart.AddListener(Begin);
        gd.onPause.AddListener(Halt);
        gd.OnResume.AddListener(Resume);
        gd.onEnd.AddListener(End);
    }
}

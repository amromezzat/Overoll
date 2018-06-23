﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectReturner))]
public class WorkerFSM : MonoBehaviour, IHalt, ICollidable, IChangeSpeed
{
    public WorkerConfig wc;
    public TileConfig tc;
    public LanesDatabase lanes;
    public GameData gd;
    public TextMesh healthText;

    Animator mAnimator;
    BoxCollider mCollider;
    ObjectReturner workerReturner;
    Rigidbody rb;

    WorkerStrafe workerStrafe;
    JumpSlideFSM jumpSlideFsm;
    WorkerCollide workerCollide;
    PositionWorker positionWorker;
    SeekLeaderPosition seekLeaderPosition;
    MergerCollide mergerCollide;
    SeekMasterMerger seekMasterMerger;
    PositionMasterMerger positionMasterMerger;

    //for tutorial
    TutWorkerStrafe tutWorkerStrafe;
    TutJumpSlide tutJumpSlide;

    IWorkerScript[] scriptsToResetState;
    Dictionary<WorkerState, StateScriptsWrapper> workerStateScripts = new Dictionary<WorkerState, StateScriptsWrapper>();
    WorkerStateTransition workerStateTransition = new WorkerStateTransition();

    public WorkerState currentState;
    [SerializeField]
    WorkerState haltedState;
    public int health = 1;
    public int level = 0;

    bool isKillingSpeed = false;

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
        for (int i = 0; i < scriptsToResetState.Length; i++)
        {
            scriptsToResetState[i].ScriptReset();
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
        health = 1;
        level = 0;
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, wc.groundLevel, 0);
        tag = "Worker";
    }

    void SetStatesScripts()
    {
        workerStrafe = new WorkerStrafe(lanes, mAnimator, transform, wc.strafeDuration);
        jumpSlideFsm = new JumpSlideFSM(wc, gd, mCollider, mAnimator, transform);
        workerCollide = new WorkerCollide(mAnimator, rb, gd);

        positionWorker = new PositionWorker(wc, rb, transform, GetInstanceID());
        seekLeaderPosition = new SeekLeaderPosition(transform, wc, lanes);
        mergerCollide = new MergerCollide(wc);
        seekMasterMerger = new SeekMasterMerger(wc, rb, transform);
        positionMasterMerger = new PositionMasterMerger(wc, rb, transform, GetInstanceID());

        //for tutorial
        tutWorkerStrafe = new TutWorkerStrafe(lanes, mAnimator, transform, wc.strafeDuration, gd);
        tutJumpSlide = new TutJumpSlide(wc, gd, mCollider, mAnimator, transform);

        scriptsToResetState = new IWorkerScript[] {
            workerStrafe, jumpSlideFsm, mergerCollide, tutWorkerStrafe, tutJumpSlide
        };

        workerStateScripts[WorkerState.Leader] = new StateScriptsWrapper(new List<IWorkerScript>() {
            workerStrafe, jumpSlideFsm }, workerStrafe, jumpSlideFsm, workerCollide);

        workerStateScripts[WorkerState.LeaderSeeker] = new StateScriptsWrapper(new List<IWorkerScript>() {
        workerStrafe, jumpSlideFsm, seekLeaderPosition}, workerStrafe, jumpSlideFsm,
        new List<IWChangeState>() { seekLeaderPosition });

        workerStateScripts[WorkerState.LeaderMerger] = new StateScriptsWrapper(new List<IWorkerScript>()
        {workerStrafe, jumpSlideFsm}, workerStrafe, jumpSlideFsm, mergerCollide);

        workerStateScripts[WorkerState.Worker] = new StateScriptsWrapper(new List<IWorkerScript>() {
        positionWorker, jumpSlideFsm}, jumpSlideFsm, workerCollide);

        workerStateScripts[WorkerState.MasterMerger] = new StateScriptsWrapper(new List<IWorkerScript>()
        {positionMasterMerger, jumpSlideFsm}, jumpSlideFsm, mergerCollide);

        workerStateScripts[WorkerState.SlaveMerger] = new StateScriptsWrapper(new List<IWorkerScript>()
        {seekMasterMerger, jumpSlideFsm}, jumpSlideFsm);

        workerStateScripts[WorkerState.Tutoring] = new StateScriptsWrapper(new List<IWorkerScript>()
        {tutWorkerStrafe, tutJumpSlide}, tutWorkerStrafe, tutJumpSlide, 
        new List<IWChangeState>() { tutWorkerStrafe, tutJumpSlide });

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

    public void ChangeState(WorkerStateTrigger trigger)
    {
        TransitionBundle transition = workerStateTransition.ChangeState(trigger, currentState);
        currentState = transition.Destination;
        Output(transition.Output);
    }

    void Output(WorkerFSMOutput outputKey)
    {
        switch (outputKey)
        {
            case WorkerFSMOutput.LeaderDied:
                wc.workers.Remove(this);
                wc.onLeaderDeath.Invoke();
                StartCoroutine(workerReturner.ReturnToPool(2));
                break;
            case WorkerFSMOutput.WorkerDied:
                wc.workers.Remove(this);
                StartCoroutine(workerReturner.ReturnToPool(2));
                break;
            case WorkerFSMOutput.WorkerRevived:
                wc.workers.Add(this);
                break;
            case WorkerFSMOutput.LeaderElected:
                seekLeaderPosition.SetClosestLane();
                rb.velocity = Vector3.zero;
                break;
            case WorkerFSMOutput.SeekingMasterMerger:
                tag = "SlaveMerger";
                break;
            case WorkerFSMOutput.LeaderMerged:
                wc.onMergeOver.Invoke();
                break;
            case WorkerFSMOutput.MasterMerged:
                wc.onMergeOver.Invoke();
                break;
            case WorkerFSMOutput.TutRightInput:
                gd.onSpeedUp.Invoke();
                break;
        }
    }

    public void SetSeekedMaster(Transform masterTransform)
    {
        seekMasterMerger.seekedMerger = masterTransform;
    }

    private void Update()
    {
        healthText.text = health.ToString();
        WorkerStateTrigger trigger = workerStateScripts[currentState].InputTrigger();
        if (trigger != WorkerStateTrigger.Null)
        {
            TransitionBundle transition = workerStateTransition.ChangeState(trigger, currentState);
            currentState = transition.Destination;
            Output(transition.Output);
        }
        if (isKillingSpeed)
        {
            mAnimator.speed *= gd.Speed / gd.oldSpeed;
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
            {
                StartCoroutine(workerReturner.ReturnToPool(0));
                return;
            }

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
        gd.onSlowDown.AddListener(SlowDown);
        gd.onSpeedUp.AddListener(SpeedUp);
    }

    public void ReactToCollision(int collidedHealth)
    {
        StartCoroutine(workerReturner.ReturnToPool(0));
    }

    public int Gethealth()
    {
        return health;
    }

    public void SpeedUp()
    {
        if (!isActiveAndEnabled)
            return;
        mAnimator.speed = 1;
        isKillingSpeed = false;
    }

    public void SlowDown()
    {
        if (!isActiveAndEnabled)
            return;
        mAnimator.speed = gd.Speed / gd.oldSpeed;
        isKillingSpeed = true;
    }
}

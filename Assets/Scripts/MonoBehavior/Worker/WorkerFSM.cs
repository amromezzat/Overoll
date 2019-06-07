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

public enum VestState
{
    WithoutVest,
    WithVest
}

[RequireComponent(typeof(WorkerReturner)), SelectionBase]
public class WorkerFSM : MonoBehaviour, IHalt, ICollidable
{
#if UNITY_EDITOR
    public DestructableObstacleCollision testSample;
#endif

    public WorkerConfig wc;
    public TileConfig tc;
    public LanesDatabase lanes;
    public GameObject MagneOnHisHand;
    public GameObject TeaOnHisHand;
    public GameObject magnetColliderObject;
    public GameObject shadow;

    public GameObject ParticlePowerUp;
    public GameObject ParticleMagnet;
    public GameObject ParticleShield;
    public GameObject ParticleSpeed;
    public GameObject ParticleDoubleCoin;

    Animator mAnimator;
    BoxCollider mCollider;
    WorkerReturner workerReturner;
    Rigidbody rb;

    WorkerStrafe workerStrafe;
    JumpSlideFSM jumpSlideFsm;
    CollideRefUpdate colliderRefUpdate = new CollideRefUpdate();

    WorkerWithoutVestCollide workerWithoutVestCollide;
    WorkerWithVestCollide workerWithVestCollide;

    PositionWorker positionWorker;
    SeekLeaderPosition seekLeaderPosition;
    MergerCollide mergerCollide;
    SeekMasterMerger seekMasterMerger;
    PositionMasterMerger positionMasterMerger;
    MergeLeaderSeeker mergeLeaderSeeker;

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

    [HideInInspector]
    public List<Material> helmetsMaterial = new List<Material>();

    [SerializeField]
    public FloatField Speed;

    //[SerializeField]
    // List<SkinnedMeshRenderer> Helmets;

    [SerializeField]
    Transform powerUpPosition;

    MeshChange mMeshChange;

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
        mCollider = GetComponent<BoxCollider>();
        workerReturner = GetComponent<WorkerReturner>();
        rb = GetComponent<Rigidbody>();

        mMeshChange = gameObject.GetComponent<MeshChange>();

        RegisterListeners();
        SetStatesScripts();

        magnetColliderObject = transform.GetChild(transform.childCount - 1).gameObject;
    }

    private void Start()
    {
        wc.onLeft.AddListener(StrafeLeft);
        wc.onRight.AddListener(StrafeRight);
        wc.onJump.AddListener(Jump);
        wc.onSlide.AddListener(Slide);
    }

    private void OnEnable()
    {
        magnetColliderObject.SetActive(false);
        for (int i = 0; i < scriptsToResetState.Length; i++)
        {
            scriptsToResetState[i].ScriptReset();
        }
        if (GameManager.Instance.gameState == GameState.Gameplay)
        {
            currentState = WorkerState.Worker;
        }
        else if (GameManager.Instance.gameState == GameState.MainMenu)
        {
            mAnimator.SetBool("Idle", true);
            haltedState = WorkerState.Leader;
            currentState = WorkerState.Halted;
        }

        Speed.onValueChanged.AddListener(ChangeAnimationSpeed);
    }

    private void OnDisable()
    {
        magnetColliderObject.SetActive(false);
        currentState = WorkerState.Dead;
        haltedState = WorkerState.Dead;
        health = 1;
        level = 0;
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, wc.groundLevel, 0);
        tag = "Worker";

        Speed.onValueChanged.RemoveListener(ChangeAnimationSpeed);
    }

    public void DisableTeaCup()
    {
        TeaOnHisHand.SetActive(false);
    }

    public void SetWorkerCollision(VestState vestState)
    {
        switch (vestState)
        {
            case VestState.WithoutVest:
                colliderRefUpdate.m_ICollide = workerWithoutVestCollide;
                break;
            case VestState.WithVest:
                colliderRefUpdate.m_ICollide = workerWithVestCollide;
                break;
        }
    }

    void ChangeAnimationSpeed(float speed)
    {
        if (currentState != WorkerState.Dead)
            mAnimator.speed = speed / SpeedManager.Instance.gameSpeed;
    }

    void SetStatesScripts()
    {
        workerStrafe = new WorkerStrafe(lanes, mAnimator, transform, wc.strafeDuration);
        jumpSlideFsm = new JumpSlideFSM(wc, mCollider, mAnimator, transform, shadow, this);

        workerWithoutVestCollide = new WorkerWithoutVestCollide(mAnimator, rb, this, mMeshChange);
        workerWithVestCollide = new WorkerWithVestCollide(mAnimator, rb);
        SetWorkerCollision(VestState.WithoutVest);

        positionWorker = new PositionWorker(wc, rb, transform, GetInstanceID());
        seekLeaderPosition = new SeekLeaderPosition(transform, wc, lanes);
        seekMasterMerger = new SeekMasterMerger(wc, rb, transform);
        positionMasterMerger = new PositionMasterMerger(wc, rb, transform, GetInstanceID());

        mergeLeaderSeeker = new MergeLeaderSeeker(transform, wc, lanes);

        mergerCollide = new MergerCollide(wc, mMeshChange, this);

        //for tutorial
        tutWorkerStrafe = new TutWorkerStrafe(lanes, mAnimator, transform, wc.strafeDuration);
        tutJumpSlide = new TutJumpSlide(wc, mCollider, mAnimator, transform, shadow, this);

        scriptsToResetState = new IWorkerScript[] {
            workerStrafe, jumpSlideFsm, workerWithoutVestCollide, workerWithVestCollide,
            tutWorkerStrafe, tutJumpSlide, mergeLeaderSeeker
        };

        workerStateScripts[WorkerState.Leader] = new StateScriptsWrapper(new List<IWorkerScript>() {
            workerStrafe, jumpSlideFsm }, workerStrafe, jumpSlideFsm, colliderRefUpdate);

        workerStateScripts[WorkerState.LeaderSeeker] = new StateScriptsWrapper(new List<IWorkerScript>() {
        workerStrafe, jumpSlideFsm, seekLeaderPosition}, workerStrafe, jumpSlideFsm,
        new List<IWChangeState>() { seekLeaderPosition });

        workerStateScripts[WorkerState.LeaderMerger] = new StateScriptsWrapper(new List<IWorkerScript>()
        {workerStrafe, jumpSlideFsm}, workerStrafe, jumpSlideFsm, mergerCollide);

        workerStateScripts[WorkerState.SeekerMerger] = new StateScriptsWrapper(new List<IWorkerScript>()
        { workerStrafe, jumpSlideFsm, mergeLeaderSeeker},
        workerStrafe, jumpSlideFsm, mergerCollide, new List<IWChangeState>() { mergeLeaderSeeker });

        workerStateScripts[WorkerState.Worker] = new StateScriptsWrapper(new List<IWorkerScript>() {
        positionWorker, jumpSlideFsm}, jumpSlideFsm, colliderRefUpdate);

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
            Output(transition.Output, other);
        }
    }

    public void ChangeState(WorkerStateTrigger trigger)
    {
        TransitionBundle transition = workerStateTransition.ChangeState(trigger, currentState);
        currentState = transition.Destination;
        Output(transition.Output);
    }

    void WorkerDied(Collider collider)
    {
        WorkersManager.Instance.RemoveWorker(this);
        StartCoroutine(workerReturner.ReturnToPool(2));

        Vector3 throwDirection = transform.position - collider.transform.position;
        throwDirection.y = 0;
        throwDirection = throwDirection.normalized * 0.25f;
        rb.AddForce(throwDirection, ForceMode.Impulse);
    }

    void Output(WorkerFSMOutput outputKey, Collider collider = null)
    {
        switch (outputKey)
        {
            case WorkerFSMOutput.LeaderDied:
                WorkerDied(collider);
                wc.onLeaderDeath.Invoke();
                AudioManager.Instance.PlaySound("WorkerDeath");
                break;
            case WorkerFSMOutput.WorkerDied:
                WorkerDied(collider);
                AudioManager.Instance.PlaySound("WorkerDeath");
                break;
            case WorkerFSMOutput.LeaderElected:
                seekLeaderPosition.SetClosestLane();
                rb.velocity = Vector3.zero;
                break;
            case WorkerFSMOutput.SeekingMasterMerger:
                tag = "SlaveMerger";
                break;
            case WorkerFSMOutput.MergingDone:
                wc.onMergeOver.Invoke();
                break;
            case WorkerFSMOutput.TutRightInput:
                TutorialManager.Instance.ExitState();
                break;
        }
    }

    public void SetSeekedMaster(Transform masterTransform)
    {
        seekMasterMerger.seekedMerger = masterTransform;
    }

    private void Update()
    {
        //healthText.text = health.ToString();
        // Check if there is an output trigger from current state
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

    public void SetHelmetMaterial(string floatName, float value)
    {
        foreach (Material helmetMaterial in helmetsMaterial)
        {
            helmetMaterial.SetFloat(floatName, value);
        }
    }
    public void Begin()
    {
        if (gameObject.activeSelf)
            mAnimator.SetBool("Idle", false);
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

#if UNITY_EDITOR
    [ContextMenu("Leader Die")]
    void LeaderDie()
    {
        Output(WorkerFSMOutput.LeaderDied);
    }
    [ContextMenu("Worker Die")]
    void WorkerDie()
    {
        Output(WorkerFSMOutput.LeaderDied);
    }
    [ContextMenu("Lose Health")]
    void LoseHealth()
    {
        DestructableObstacleCollision other = Instantiate(testSample);
        WorkerStateTrigger trigger = workerStateScripts[currentState].Collide(other.GetComponent<Collider>(), ref health);
        if (trigger != WorkerStateTrigger.Null)
        {
            TransitionBundle transition = workerStateTransition.ChangeState(trigger, currentState);
            currentState = transition.Destination;
            Output(transition.Output);
        }
    }
#endif

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
        GameManager.Instance.OnStart.AddListener(Begin);
        GameManager.Instance.onPause.AddListener(Halt);
        GameManager.Instance.OnResume.AddListener(Resume);
        GameManager.Instance.onEnd.AddListener(End);
    }

    public void ReactToCollision(int collidedHealth)
    {
        WorkersManager.Instance.RemoveWorker(this);
        StartCoroutine(workerReturner.ReturnToPool(0));
    }

    public int Gethealth()
    {
        return health;
    }
}

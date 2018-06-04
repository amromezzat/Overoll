using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WorkerReturner))]
public class WorkerLifeCycle : MonoBehaviour
{
    public GameData gData;
    public WorkerConfig wConfig;
    public TileConfig tc;

    public HealthState healthState = HealthState.Healthy;
    public int workerHealth;
    Rigidbody rb;

    public bool isLeader = false;
    WorkerReturner workerReturner;
    Animator animator;
    PositionWorker positionWorker;
    WorkerStrafe workerStrafe;
    JumpSlideFSM jumpSlideFSM;
    SeekLeaderPosition seekLeaderPosition;
    RandomBehaviour randomBehaviour;
    //------------------------------------------------

    void Awake()
    {
        workerHealth = 1;
        workerReturner = GetComponent<WorkerReturner>();
        animator = GetComponent<Animator>();
        positionWorker = GetComponent<PositionWorker>();
        workerStrafe = GetComponent<WorkerStrafe>();
        jumpSlideFSM = GetComponent<JumpSlideFSM>();
        seekLeaderPosition = GetComponent<SeekLeaderPosition>();
        randomBehaviour = GetComponent<RandomBehaviour>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        workerHealth = 1;
        healthState = HealthState.Healthy;
        positionWorker.enabled = true;
        jumpSlideFSM.enabled = true;
        randomBehaviour.enabled = true;
        rb.velocity = Vector3.zero;
    }

    IEnumerator DeathCoroutine()
    {
        animator.SetTrigger("DeathAnim");
        positionWorker.enabled = false;
        workerStrafe.enabled = false;
        jumpSlideFSM.enabled = false;
        seekLeaderPosition.enabled = false;
        rb.velocity = Vector3.back * tc.tileSpeed;
        wConfig.workers.Remove(gameObject);
        if (isLeader)
        {
            wConfig.onLeaderDeath.Invoke();
            isLeader = false;
        }
        yield return new WaitForSeconds(2.0f);
        workerReturner.ReturnToObjectPool();
    }

    private void Update()
    {
        if(gData.gameState == GameState.GameOver)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            ICollidable collidableOther = other.GetComponent<ICollidable>();

            int obsHealth = collidableOther.Gethealth();
            int preCollisionWH = workerHealth;
            workerHealth = workerHealth - obsHealth;
            collidableOther.ReactToCollision(preCollisionWH);

            healthState = HealthState.Fractured;

            if (workerHealth <= 0)
            {
                healthState = HealthState.Wrecked;

                StartCoroutine(DeathCoroutine());
            }

        }

        if (other.gameObject.tag == "collider")
        {
            healthState = HealthState.Wrecked;
            StartCoroutine(DeathCoroutine());
        }
    }
}

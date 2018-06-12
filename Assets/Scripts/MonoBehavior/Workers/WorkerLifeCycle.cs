using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerLifeCycle : MonoBehaviour
{
    public GameData gData;
    public WorkerConfig wConfig;
    public TileConfig tc;

    [HideInInspector]
    public HealthState healthState = HealthState.Healthy;
    [HideInInspector]
    public int workerHealth;
    [HideInInspector]
    Rigidbody rb;

    public bool isLeader = false;
    ObjectReturner workerReturner;
    Animator animator;
    PositionWorker positionWorker;
    WorkerStrafe workerStrafe;
    JumpSlideFSM jumpSlideFSM;
    SeekLeaderPosition seekLeaderPosition;
    RandomBehaviour randomBehaviour;
    bool fallingToDeath = false;

    //------------------------------------------------

    void Awake()
    {
        workerHealth = 1;
        workerReturner = GetComponent<ObjectReturner>();
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
        fallingToDeath = false;
        transform.position = new Vector3(0, wConfig.groundLevel, 0);
    }

    IEnumerator DeathCoroutine()
    {
        healthState = HealthState.Wrecked;
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
        if (gData.gameState == GameState.GameOver)
        {
            rb.velocity = Vector3.zero;
        }
        if (fallingToDeath)
        {
            transform.position -= new Vector3(0, 0.05f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        if (CompareTag("MagnetCollider"))
        {
            return;
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().PlaySound("WorkerDeath");
            ICollidable collidableOther = other.GetComponent<ICollidable>();

            int obsHealth = collidableOther.Gethealth();
            int preCollisionWH = workerHealth;
            workerHealth = workerHealth - obsHealth;
            collidableOther.ReactToCollision(preCollisionWH);

            healthState = HealthState.Fractured;

            if (workerHealth <= 0)
            {
                animator.SetTrigger("DeathAnim");
                StartCoroutine(DeathCoroutine());
            }

        }

        if (other.gameObject.CompareTag("FallCollider"))
        {
            animator.SetTrigger("FallToDeathAnim");
            fallingToDeath = true;
            StartCoroutine(DeathCoroutine());
        }
    }
}


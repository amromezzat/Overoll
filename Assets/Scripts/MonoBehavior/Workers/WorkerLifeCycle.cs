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
    public int workerHealth;
    Rigidbody rb;

    ObjectReturner objReturner;
    Animator animator;
    PositionWorker positionWorker;
    WorkerStrafe workerStrafe;
    JumpSlideFSM jumpSlideFSM;
    //------------------------------------------------

    void Awake()
    {
        workerHealth = 1;
        objReturner = GetComponent<ObjectReturner>();
        animator = GetComponent<Animator>();
        positionWorker = GetComponent<PositionWorker>();
        workerStrafe = GetComponent<WorkerStrafe>();
        jumpSlideFSM = GetComponent<JumpSlideFSM>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        wConfig.workers.Add(gameObject);
    }

    private void OnDisable()
    {
        workerHealth = 1;
        healthState = HealthState.Healthy;
        wConfig.workers.Remove(gameObject);
        positionWorker.enabled = true;
        jumpSlideFSM.enabled = true;
        rb.velocity = Vector3.zero;
    }

    IEnumerator DeathCoroutine()
    {
        animator.SetTrigger("DeathAnim");
        positionWorker.enabled = false;
        workerStrafe.enabled = false;
        jumpSlideFSM.enabled = false;
        rb.velocity = Vector3.back * tc.tileSpeed;
        gData.workersNum -= 1;
        yield return new WaitForSeconds(2.0f);
        objReturner.ReturnToObjectPool();
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
            ICollidable Other = other.GetComponent<ICollidable>();

            int obsHealth = Other.Gethealth();
            int preCollisionWH = workerHealth;
            workerHealth = workerHealth - obsHealth;

            Other.ReactToCollision(preCollisionWH);

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

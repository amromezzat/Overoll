using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorkerLifeCycle : MonoBehaviour, ICollidable
{

    [HideInInspector]
    public HealthState healthState = HealthState.Healthy;
    [HideInInspector]
    public int workerHealth;

    WorkerReturner wr;
    WorkerFollowState wfs;
    Animator animator;
    bool fallingToDeath = false;

    //------------------------------------------------

    void Awake()
    {
        workerHealth = 1;
        wr = GetComponent<WorkerReturner>();
        animator = GetComponent<Animator>();
        wfs = GetComponent<WorkerFollowState>();
    }

    private void OnDisable()
    {
        workerHealth = 1;
        healthState = HealthState.Healthy;
        fallingToDeath = false;
    }

    private void Update()
    {
        if (fallingToDeath)
        {
            transform.position -= new Vector3(0, 0.05f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Worker"))
        {
            if (!wfs.CanMerge(other.GetComponent<WorkerFollowState>()))
            {
                return;
            }
            ICollidable otherWorker = other.GetComponent<ICollidable>();

            workerHealth += otherWorker.Gethealth();

            otherWorker.ReactToCollision(workerHealth);
        }

        else if (CompareTag("MagnetCollider"))
        {
            return;
        }

        else if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().PlaySound("WorkerDeath");
            ICollidable collidableObstacle = other.GetComponent<ICollidable>();

            int obsHealth = collidableObstacle.Gethealth();
            int preCollisionWH = workerHealth;
            workerHealth = workerHealth - obsHealth;
            collidableObstacle.ReactToCollision(preCollisionWH);

            healthState = HealthState.Fractured;

            if (workerHealth <= 0)
            {
                animator.SetTrigger("DeathAnim");
                StartCoroutine(wr.PoolReturnCoroutine(2));
            }

        }


        else if (other.gameObject.CompareTag("FallCollider"))
        {
            animator.SetTrigger("FallToDeathAnim");
            fallingToDeath = true;
            StartCoroutine(wr.PoolReturnCoroutine(2));
        }
    }

    public void ReactToCollision(int collidedHealth)
    {
        StartCoroutine(wr.PoolReturnCoroutine(0));
    }

    public int Gethealth()
    {
        return workerHealth;
    }
}


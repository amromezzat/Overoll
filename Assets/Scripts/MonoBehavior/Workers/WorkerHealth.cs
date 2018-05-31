using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum workerState
{
    Idle = 1,
    Dead = 2,
    Hit = 3
}
public class WorkerHealth : MonoBehaviour
{

    public workerState state = workerState.Idle;
    public int workerHealth;
    ObjectReturner objReturner;
    public GameData gData;
    ObjectMover ObjectMover;
    Animator animator;
    //------------------------------------------------

    void OnEnable()
    {
        workerHealth = 1;
        objReturner = GetComponent<ObjectReturner>();
        animator = GetComponent<Animator>();
        ObjectMover = GetComponent<ObjectMover>();
    }

    private void OnDisable()
    {
        ObjectMover.enabled = false;
    }

    IEnumerator WaitToAnimate()
    {
        animator.SetBool("DeathAnim", true);
        ObjectMover.enabled = true;
        gData.workersNum -= 1;
        yield return new WaitForSeconds(2.0f);

        objReturner.ReturnToObjectPool();
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
                state = workerState.Dead;

                StartCoroutine(WaitToAnimate());
            }

        }

        if (other.gameObject.tag == "collider")
        {
            state = workerState.Dead;
            StartCoroutine(WaitToAnimate());
        }
    }
}

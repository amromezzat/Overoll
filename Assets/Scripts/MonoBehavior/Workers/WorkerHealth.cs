using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerHealth : MonoBehaviour
{
    enum workerState
    {
        Idle = 1,
        Dead = 2,
        Hit = 3
    }

    workerState state = workerState.Idle;
    public int workerHealth;

    //------------------------------------------------

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

            }
        }




    }



}

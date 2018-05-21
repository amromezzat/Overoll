using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerHealth : MonoBehaviour { 
    enum workerState
    {
        Idle=1,
        Dead=2,
        Hit=3
    }

    workerState state = workerState.Idle;
    public int workerHealth;
    public int obsH;
    public BaseObstacle baseObs;
    public int CoinCount;

    //------------------------------------------------

    public void onTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "obstacle")
        {
            Collidable Other = other.GetComponent<Collidable>();

            obsH = Other.Gethealth();
            int wh = workerHealth;
            workerHealth = workerHealth - obsH;

           Other.reactToCollision(wh);
        }

        if (other.gameObject.tag == "coin")
        {
            Destroy(other);
            //return to the coin pool
            CoinCount += 1;
            Debug.Log(CoinCount);
        }


    }



}

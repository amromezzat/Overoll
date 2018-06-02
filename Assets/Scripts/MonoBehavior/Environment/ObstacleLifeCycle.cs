using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectReturner))]
public class ObstacleLifeCycle : MonoBehaviour, ICollidable
{

    public int obsHealth;
    ObjectReturner objReturner;
    HealthState obstacleState = HealthState.Healthy;

    private void Awake()
    {
        objReturner = GetComponent<ObjectReturner>();
    }

    public void ReactToCollision(int collidedHealth)
    {
        obsHealth = obsHealth - collidedHealth;
        if (obsHealth <= 0)
        {
            obstacleState = HealthState.Wrecked;
            obsHealth = 1;
            objReturner.ReturnToObjectPool();
        }
        else
        {
            obstacleState = HealthState.Fractured;
        }
        //-----------------------------------------      
    }

    public int Gethealth()
    {
        return obsHealth;
    }
    //-----------------------------------------------------------------------------------------

}

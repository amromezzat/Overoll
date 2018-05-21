using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Collidable
{

    void reactToCollision(int collidedHealth);
    int Gethealth();
}


public class BaseObstacle :MonoBehaviour, Collidable {

    public int obsHealth;
    public Vector3 obsVelocity;
    public WorkerHealth WH;
    
    enum obstacleState
    {
        Idle=1, // No one collide with it yet
        Destroied=2, //the obstacle is destroid 
        Broken=3  // lesa feh shewaia health metb2e
    }

    obstacleState state= obstacleState.Idle;


    public void reactToCollision(int collidedHealth)
    {
        obsHealth = obsHealth - collidedHealth;



        if (obsHealth <= 0)
        {
            state = obstacleState.Destroied;
        }
        else
        {
            state = obstacleState.Broken;
        }
        //-----------------------------------------

       
    }

    public int Gethealth()
    {
      
        return obsHealth;
       
    }
    //-----------------------------------------------------------------------------------------
  
}

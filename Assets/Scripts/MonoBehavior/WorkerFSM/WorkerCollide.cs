using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCollide : IWCollide
{
    Animator animator;
    Rigidbody rb;
    TileConfig tc;

    public WorkerCollide(Animator animator, Rigidbody rb, TileConfig tc)
    {
        this.animator = animator;
        this.rb = rb;
        this.tc = tc;
    }

    public void ScriptReset()
    {

    }

    public WorkerStateTrigger Collide(Collider collider, ref int health)
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            //FindObjectOfType<AudioManager>().PlaySound("WorkerDeath");
            ICollidable collidableObstacle = collider.GetComponent<ICollidable>();

            int obsHealth = collidableObstacle.Gethealth();
            int preCollisionWH = health;
            health = health - obsHealth;
            collidableObstacle.ReactToCollision(preCollisionWH);

            if (health <= 0)
            {
                animator.SetTrigger("DeathAnim");
                rb.velocity += Vector3.back * tc.tileSpeed;
                return WorkerStateTrigger.Die;
            }

        }


        else if (collider.gameObject.CompareTag("FallCollider"))
        {
            animator.SetTrigger("FallToDeathAnim");
            rb.velocity += Vector3.down * 0.5f;
            return WorkerStateTrigger.Die;
        }
        return WorkerStateTrigger.Null;
    }

    public void FixedUpdate(float fixedDeltaTime)
    {
    
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerStrafe : IWStrafe
{
    //external references
    LanesDatabase lanes;
    Transform transform;
    Animator animator;
    float strafeDuration;

    bool strafing = false;
    float strafeTimer = 0;

    public WorkerStrafe(LanesDatabase lanes, Animator animator, Transform transform,float strafeDuration){
        this.lanes = lanes;
        this.animator = animator;
        this.transform = transform;
        this.strafeDuration = strafeDuration;
    }

    public virtual void StrafeRight()
    {
        if (!strafing)
        {
            strafeTimer = 0;
            animator.SetBool("StrafeRightAnim", true);
            lanes.GoRight();
            strafing = true;
        }
    }

    public virtual void StrafeLeft()
    {
        if (!strafing)
        {
            strafeTimer = 0;
            animator.SetBool("StrafeLeftAnim", true);
            lanes.GoLeft();
            strafing = true;
        }
    }


    public void FixedUpdate(float fixedDeltaTime)
    {
        if (strafing)
        {
            float completedPortion = strafeTimer / strafeDuration;
            float squarePortion = completedPortion * completedPortion;
            float xPos = Mathf.Lerp(transform.position.x, lanes.CurrentLane.laneCenter, squarePortion);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            strafeTimer += fixedDeltaTime;
            if (squarePortion >= 1)
            {
                strafing = false;
                animator.SetBool("StrafeRightAnim", false);
                animator.SetBool("StrafeLeftAnim", false);
            }
        }
    }

    public virtual void ScriptReset()
    {
        strafing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : IDoAction
{
    public float interruptTime = 10;
    public float slideDuration;

    BoxCollider collider;
    float slideTimer = 0;

    public Slide(BoxCollider _collider)
    {
        collider = _collider;
    }

    public void OnStateEnter(Animator animator)
    {
        slideTimer = 0;
        animator.SetBool("DuckAnim", true);
        Vector3 newColliderSize = collider.size;
        newColliderSize.y *= 0.25f;
        collider.size = newColliderSize;
        Vector3 colliderNewPos = collider.transform.position;
        colliderNewPos.y *= 0.25f;
        collider.transform.position = colliderNewPos;
    }

    public ActionState OnStateExecution(Transform transform, float deltaTime)
    {
        interruptTime -= deltaTime;
        slideTimer += deltaTime;
        if (slideTimer >= slideDuration)
        {
            return ActionState.FINISHED;
        }

        return ActionState.RUNNING;
    }

    public void OnStateExit(Animator animator)
    {
        interruptTime = 10;
        animator.SetBool("DuckAnim", false);
        Vector3 newColliderSize = collider.size;
        newColliderSize.y *= 4;
        collider.size = newColliderSize;
        Vector3 colliderNewPos = collider.transform.position;
        colliderNewPos.y *= 4;
        collider.transform.position = colliderNewPos;
    }
}

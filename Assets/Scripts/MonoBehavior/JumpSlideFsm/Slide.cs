using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : IDoAction
{
    public float interruptTime = 10;

    float slideTime;
    BoxCollider collider;

    public Slide(BoxCollider _collider)
    {
        collider = _collider;
    }

    public void OnStateEnter(Animator animator)
    {
        slideTime = 0.5f;
        animator.SetBool("DuckAnim", true);
        Vector3 newColliderSize = collider.size;
        newColliderSize.y *= 0.5f;
        collider.size = newColliderSize;
        Vector3 colliderNewPos = collider.transform.position;
        colliderNewPos.y *= 0.5f;
        collider.transform.position = colliderNewPos;
    }

    public ActionState OnStateExecution(Transform transform, float deltaTime)
    {
        interruptTime -= deltaTime;
        slideTime -= deltaTime;
        if (slideTime <= 0)
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
        newColliderSize.y *= 2;
        collider.size = newColliderSize;
        Vector3 colliderNewPos = collider.transform.position;
        colliderNewPos.y *= 2;
        collider.transform.position = colliderNewPos;
    }
}

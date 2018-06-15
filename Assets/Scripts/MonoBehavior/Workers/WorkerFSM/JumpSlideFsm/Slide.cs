using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : IDoAction
{
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

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        slideTimer += deltaTime;
        if (slideTimer >= slideDuration)
        {
            return false;
        }
        return true;
    }

    public void OnStateExit(Animator animator)
    {
        animator.SetBool("DuckAnim", false);
        Vector3 newColliderSize = collider.size;
        newColliderSize.y *= 4;
        collider.size = newColliderSize;
        Vector3 colliderNewPos = collider.transform.position;
        colliderNewPos.y *= 4;
        collider.transform.position = colliderNewPos;
    }
}

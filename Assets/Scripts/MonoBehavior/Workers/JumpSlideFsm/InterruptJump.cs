using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptJump : IDoAction
{
    float landTimer;
    float landDuration = 0.2f;

    public void OnStateEnter(Animator animator)
    {
        
    }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        Vector3 newPos = transform.position;
        landTimer += deltaTime;
        float completedPortion = landTimer / landDuration;
        newPos.y = Mathf.Lerp(transform.position.y, 0.25f, Mathf.PI * completedPortion);
        transform.position = newPos;
        if (transform.position.y <= 0.25)
        {
            return false;
        }
        return true;
    }

    public void OnStateExit(Animator animator)
    {
        animator.SetBool("JumpAnim", false);
    }
}

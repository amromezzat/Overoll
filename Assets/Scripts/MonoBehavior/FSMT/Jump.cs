using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IDoAction
{
    float jumpTimer;
    float jumpDuration = 1;
    float jumpHeight = 2;

    public void OnStateEnter(Animator animator)
    {
        animator.SetBool("JumpAnim", true);
        jumpTimer = 0;
    }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        Vector3 newPos = transform.position;
        jumpTimer += deltaTime;
        float completedPortion = jumpTimer / jumpDuration;
        newPos.y = Mathf.Lerp(0.25f, jumpHeight, Mathf.Sin(Mathf.PI * completedPortion));
        transform.position = newPos;
        if (transform.position.y <= 0.25)
        {
            return true;
        }
        return false;
    }

    public void OnStateExit(Animator animator)
    {
        animator.SetBool("JumpAnim", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IDoAction
{
    public float jumpDuration;
    public float jumpHeight;

    float jumpTimer;

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
            return false;
        }
        return true;
    }

    public void OnStateExit(Animator animator)
    {
        animator.SetBool("JumpAnim", false);
    }
}

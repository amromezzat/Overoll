using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaltState : IDoAction
{
    public void OnStateEnter(Animator animator)
    {
        animator.speed = 0;
    }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        return true;
    }

    public void OnStateExit(Animator animator)
    {
        animator.speed = 1;
    }
}

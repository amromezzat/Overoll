using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaltState : IDoAction
{
    public void OnStateEnter(Animator animator)
    {
        animator.speed = 0;
    }

    public ActionState OnStateExecution(Transform transform, float deltaTime)
    {
        return ActionState.RUNNING;
    }

    public void OnStateExit(Animator animator)
    {
        animator.speed = 1;
    }
}

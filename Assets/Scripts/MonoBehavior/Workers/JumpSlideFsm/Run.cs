using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : IDoAction {
    public void OnStateEnter(Animator animator)
    {
    }

    public ActionState OnStateExecution(Transform transform, float deltaTime)
    {
        return ActionState.RUNNING;
    }

    public void OnStateExit(Animator animator)
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : IDoAction {
    public void OnStateEnter(Animator animator)
    {
    }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        return false;
    }

    public void OnStateExit(Animator animator)
    {
    }
}

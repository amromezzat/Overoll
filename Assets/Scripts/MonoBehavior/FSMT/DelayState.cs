using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayState : IDoAction
{
    float delay;

    public float Delay
    {
        get
        {
            return delay;
        }

        set
        {
            delay = value;
        }
    }

    public void OnStateEnter(Animator animator)
    { }

    public bool OnStateExecution(Transform transform, float deltaTime)
    {
        delay -= deltaTime;
        if(delay <= 0)
        {
            return true;
        }
        return false;
    }

    public void OnStateExit(Animator animator)
    { }
}

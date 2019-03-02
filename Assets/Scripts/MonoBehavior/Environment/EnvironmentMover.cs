using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnvironmentMover : ObjectMover
{
    Animator[] animList;

    protected virtual void Awake()
    {
        animList = GetComponentsInChildren<Animator>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void SetAnimatorsSpeed(float speed)
    {
        foreach (Animator anim in animList)
        {
            anim.speed = speed;
        }
    }
}

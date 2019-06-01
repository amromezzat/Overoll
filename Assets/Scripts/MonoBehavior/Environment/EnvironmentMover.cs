using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnvironmentMover : ObjectMover
{
    Animator[] animList;

    protected override void Awake()
    {
        base.Awake();

        animList = GetComponentsInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetAnimatorsSpeed(float speed)
    {
        foreach (Animator anim in animList)
        {
            anim.speed = speed;
        }
    }
}

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

    protected override void OnEnable()
    {
        base.OnEnable();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TileMover : ObjectMover
{
    public TileExtraAction tileExtraAction;

    public Animator Anim;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected virtual void OnDisable()
    {
        if (Anim != null)
            Anim.SetTrigger("Reset");
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void MoveObj()
    {
        base.MoveObj();

        if (isActiveAndEnabled && tileExtraAction != null)
        {
            tileExtraAction.Begin(this);
        }
    }

    protected override void SetAnimatorsSpeed(float speed)
    {
        if (Anim != null)
            Anim.speed = speed;
    }
}

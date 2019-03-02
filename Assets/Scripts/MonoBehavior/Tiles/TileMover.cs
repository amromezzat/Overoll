using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TileMover : ObjectMover
{
    public TileExtraAction tileExtraAction;

    public Animator Anim;

    [HideInInspector]
    public float extraSpeed;

    protected virtual void OnDisable()
    {
        if (Anim != null)
            Anim.SetTrigger("Reset");
    }

    private void OnEnable()
    {
        TakeExtraAction();
    }

    protected override void Update()
    {
        float speed = SpeedManager.Instance.speed + extraSpeed;
        transform.position += Vector3.back * speed;
        SetAnimatorsSpeed(speed / SpeedManager.Instance.speed.OldValue);
    }

    protected virtual void TakeExtraAction()
    {
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

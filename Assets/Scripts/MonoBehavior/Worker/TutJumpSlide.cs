using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutJumpSlide : JumpSlideFSM, IWChangeState
{
    bool tutRightAct = false;

    public TutJumpSlide(WorkerConfig wc, GameData gd, BoxCollider mCollider, Animator mAnimator, Transform transform) : base(wc, gd, mCollider, mAnimator, transform)
    {
    }

    public override void ScriptReset()
    {
        base.ScriptReset();
        tutRightAct = false;
    }

    public override void Jump()
    {
        if (gd.TutorialState == TutorialState.Jump)
        {
            tutRightAct = true;
            base.Jump();
        }
    }

    public override void Slide()
    {
        if (gd.TutorialState == TutorialState.Slide)
        {
            tutRightAct = true;
            base.Slide();
        }
    }

    public WorkerStateTrigger InputTrigger()
    {
        if (tutRightAct)
        {
            tutRightAct = false;
            return WorkerStateTrigger.StateEnd;
        }
        return WorkerStateTrigger.Null;
    }
}

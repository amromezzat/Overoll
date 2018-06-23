using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutWorkerStrafe : WorkerStrafe, IWChangeState
{
    GameData gd;
    bool tutRightAct = false;

    public TutWorkerStrafe(LanesDatabase lanes, Animator animator, Transform transform,
        float strafeDuration, GameData gd) : base(lanes, animator, transform, strafeDuration)
    {
        this.gd = gd;
    }

    public override void ScriptReset()
    {
        base.ScriptReset();
        tutRightAct = false;
    }

    public override void StrafeRight()
    {
        if (gd.TutorialState == TutorialState.RightStrafe)
        {
            tutRightAct = true;
            base.StrafeRight();
        }
    }

    public override void StrafeLeft()
    {
        if (gd.TutorialState == TutorialState.LeftStrafe)
        {
            tutRightAct = true;
            base.StrafeLeft();
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

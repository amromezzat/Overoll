using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScriptsWrapper
{
    public List<IWorkerScript> attachedScripts = new List<IWorkerScript>();
    public IWStrafe strafeScript = null;
    public IWJumpSlide jumpSlideScript = null;
    public List<IWChangeState> changeStateScripts = new List<IWChangeState>();
    public IWCollide collideScript = null;


    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript,
    IWCollide collideScript) : this(attachedScripts, strafeScript, jumpSlideScript)
    {
        this.collideScript = collideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript,
        List<IWChangeState> changeStateScripts) : this(attachedScripts, strafeScript, jumpSlideScript)
    {
        this.changeStateScripts = changeStateScripts;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript) :
        this(attachedScripts)
    {
        this.strafeScript = strafeScript;
        this.jumpSlideScript = jumpSlideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, List<IWChangeState> changeStateScripts) : this(attachedScripts)
    {
        this.changeStateScripts = changeStateScripts;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWJumpSlide jumpSlideScript, IWCollide collideScript) :
        this(attachedScripts)
    {
        this.jumpSlideScript = jumpSlideScript;
        this.collideScript = collideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWJumpSlide jumpSlideScript)
        : this(attachedScripts)
    {
        this.jumpSlideScript = jumpSlideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts)
    {
        this.attachedScripts = attachedScripts;
    }

    public void StrafeLeft()
    {
        if (strafeScript != null)
            strafeScript.StrafeLeft();
    }

    public void StrafeRight()
    {
        if (strafeScript != null)
            strafeScript.StrafeRight();
    }

    public void Jump()
    {
        if (jumpSlideScript != null)
            jumpSlideScript.Jump();
    }

    public void Slide()
    {
        if (jumpSlideScript != null)
            jumpSlideScript.Slide();
    }

    public WorkerStateTrigger Collide(Collider collider, ref int health)
    {
        if (collideScript != null)
            return collideScript.Collide(collider, ref health);
        return WorkerStateTrigger.Null;
    }

    public WorkerStateTrigger InputTrigger()
    {
        foreach (IWChangeState stateScript in changeStateScripts)
        {
            WorkerStateTrigger inputTrigger = stateScript.InputTrigger();
            if (inputTrigger != WorkerStateTrigger.Null)
            {
                return inputTrigger;
            }
        }
        return WorkerStateTrigger.Null;
    }

    public IEnumerator GetEnumerator()
    {
        return attachedScripts.GetEnumerator();
    }

}

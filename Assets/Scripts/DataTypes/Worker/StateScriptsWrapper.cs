using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScriptsWrapper
{
    public List<IWorkerScript> attachedScripts = new List<IWorkerScript>();
    public IWStrafe strafeScript = null;
    public IWJumpSlide jumpSlideScript = null;
    public IWChangeState changeStateScript = null;
    public IWCollide collideScript = null;

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript,
    IWCollide collideScript) : this(attachedScripts, strafeScript, jumpSlideScript)
    {
        this.collideScript = collideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript, 
        IWChangeState changeStateScript) : this(attachedScripts, strafeScript, jumpSlideScript)
    {
        this.changeStateScript = changeStateScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript) : 
        this(attachedScripts)
    {
        this.strafeScript = strafeScript;
        this.jumpSlideScript = jumpSlideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWChangeState changeStateScript) : this(attachedScripts)
    {
        this.changeStateScript = changeStateScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWJumpSlide jumpSlideScript, IWCollide collideScript) : 
        this(attachedScripts)
    {
        this.jumpSlideScript = jumpSlideScript;
        this.collideScript = collideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWJumpSlide jumpSlideScript)
        :this(attachedScripts)
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
        if (changeStateScript != null)
            return changeStateScript.InputTrigger();
        return WorkerStateTrigger.Null;
    }

    public IEnumerator GetEnumerator()
    {
        return attachedScripts.GetEnumerator();
    }

}

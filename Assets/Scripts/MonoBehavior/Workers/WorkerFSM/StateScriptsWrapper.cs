using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScriptsWrapper
{
    public List<IWorkerScript> attachedScripts = new List<IWorkerScript>();
    public IStrafe strafeScript = null;
    public IJumpSlide jumpSlideScript = null;
    public IChangeState changeStateScript = null;
    public ICollide collideScript = null;


    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IStrafe strafeScript, IJumpSlide jumpSlideScript,
    ICollide collideScript) : this(attachedScripts, strafeScript, jumpSlideScript)
    {
        this.collideScript = collideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IStrafe strafeScript, IJumpSlide jumpSlideScript, 
        IChangeState changeStateScript) : this(attachedScripts, strafeScript, jumpSlideScript)
    {
        this.changeStateScript = changeStateScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IStrafe strafeScript, IJumpSlide jumpSlideScript) : this(attachedScripts)
    {
        this.strafeScript = strafeScript;
        this.jumpSlideScript = jumpSlideScript;
    }


    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IChangeState changeStateScript)
    {
        this.attachedScripts = attachedScripts;
        this.changeStateScript = changeStateScript;
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

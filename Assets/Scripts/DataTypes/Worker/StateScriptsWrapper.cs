/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScriptsWrapper
{
    // Scripts that use update
    public List<IWorkerScript> attachedScripts = new List<IWorkerScript>();
    // Scripts that use left and right input
    public IWStrafe strafeScript = null;
    // Scripts that use jump and slide input
    public IWJumpSlide jumpSlideScript = null;
    // Scripts that can call FSM to change state
    public List<IWChangeState> changeStateScripts = new List<IWChangeState>();
    // Scripts that take action on collision
    public CollideRefUpdate collideRefScript = null;
    public IWCollide collideScript;

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript,
    IWCollide collideScript) : this(attachedScripts, strafeScript, jumpSlideScript)
    {
        this.collideScript = collideScript;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript,
CollideRefUpdate collideScript) : this(attachedScripts, strafeScript, jumpSlideScript)
    {
        this.collideRefScript = collideScript;
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

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript, IWCollide collideScript, List<IWChangeState> changeStateScripts) : this(attachedScripts, strafeScript, jumpSlideScript, collideScript)
    {
        this.changeStateScripts = changeStateScripts;
    }

    public StateScriptsWrapper(List<IWorkerScript> attachedScripts, IWStrafe strafeScript, IWJumpSlide jumpSlideScript, CollideRefUpdate collideRefUpdate, List<IWChangeState> changeStateScripts) : this(attachedScripts, strafeScript, jumpSlideScript, collideRefUpdate)
    {
        this.changeStateScripts = changeStateScripts;
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
        if (collideRefScript != null && collideRefScript.m_ICollide != null)
            return collideRefScript.m_ICollide.Collide(collider, ref health);
        return WorkerStateTrigger.Null;
    }

    /// <summary>
    /// Gets the output of the state, from states that can transfer to another
    /// without external input to the FSM
    /// </summary>
    /// <returns></returns>
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

using UnityEngine;

public interface IWorkerScript
{
    void FixedUpdate(float fixedDeltaTime);
    //restore script initial state
    void ScriptReset();
}

public interface IWStrafe : IWorkerScript
{
    void StrafeLeft();
    void StrafeRight();
}

public interface IWJumpSlide : IWorkerScript
{
    void Jump();
    void Slide();
}


public interface IWChangeState: IWorkerScript
{
    //state sets an input trigger when it is done
    WorkerStateTrigger InputTrigger();
}

public interface IWCollide : IWorkerScript
{
    WorkerStateTrigger Collide(Collider collider, ref int health);
}

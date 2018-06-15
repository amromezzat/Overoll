using UnityEngine;

public interface IWorkerScript
{
    void FixedUpdate(float fixedDeltaTime);
    //restore script initial state
    void ScriptReset();
}

public interface IStrafe : IWorkerScript
{
    void StrafeLeft();
    void StrafeRight();
}

public interface IJumpSlide : IWorkerScript
{
    void Jump();
    void Slide();
}


public interface IChangeState: IWorkerScript
{
    //state sets an input trigger when it is done
    WorkerStateTrigger InputTrigger();
}

public interface ICollide : IWorkerScript
{
    WorkerStateTrigger Collide(Collider collider, ref int health);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoAction  {
    void OnStateEnter(Animator animator);
    bool OnStateExecution(Transform transform, float deltaTime);
    void OnStateExit(Animator animator);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomState : StateMachineBehaviour
{
    [SerializeField]
    float numberOfStates;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        animator.SetTrigger(Mathf.CeilToInt(Random.value * numberOfStates).ToString());
    }
}

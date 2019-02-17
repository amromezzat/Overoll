using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRollBehavior : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetBool("DuckAnim", false);
    }
}

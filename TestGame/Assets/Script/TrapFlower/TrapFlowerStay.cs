using UnityEngine;
using System.Collections;

public class TrapFlowerStay : TrapFlowerState
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isStay", false);
    }
}

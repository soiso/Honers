using UnityEngine;
using System.Collections;

public class EventRavenAnimation : StateMachineBehaviour
{

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDrop", false);
    }
}

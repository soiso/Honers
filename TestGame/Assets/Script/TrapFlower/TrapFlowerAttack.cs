using UnityEngine;
using System.Collections;

public class TrapFlowerAttack : TrapFlowerState
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("TrapFlower").GetComponentInChildren<DamageTrigger>().OnCollisionBegin();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isAttack", false);
        GameObject.Find("TrapFlower").GetComponent<TrapFlower>().SetTimer();
        GameObject.Find("TrapFlower").GetComponentInChildren<DamageTrigger>().onCollisionEnd();
    }
}

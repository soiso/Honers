using UnityEngine;
using System.Collections;

public class P_Over_Obstacle : PlayerStateInterFace 
{
    [SerializeField]
    private  Transform m_target;
    [SerializeField,Range(0.00001f,1f)]
    private float m_interpolation_Speed = 0f;
    private float m_current_interpolation =0f;

    public override void Enter(ref Player player) 
    {
        m_current_interpolation = 0f;
        player.Get_Animator.SetTrigger("NearObstacle");
    }

    private void Move(ref Player player)
    {
        m_current_interpolation += m_interpolation_Speed;
        Vector3 pos = Vector3.Lerp(player.transform.position, m_target.position, m_current_interpolation);
        player.Get_RigidBody.MovePosition(pos);
    }

    public override bool Execute(ref Player player)
    {
        Move(ref player);
       // string name = player.Get_Animator.GetAnimatorTransitionInfo(0).ToString();
        AnimatorStateInfo current_State = player.Get_Animator.GetCurrentAnimatorStateInfo(0);
        bool is_Run = current_State.IsName("Base Layer.HANDUP");
 
        if(player.Get_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && is_Run)
        {
            player.Get_Animator.SetTrigger("return_Default");
            return true;
        }
        return false;
    }  	

    public override void Exit_State(ref Player player)
    {

    }

}

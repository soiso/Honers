using UnityEngine;
using System.Collections;

public class P_Move : PlayerStateInterFace
{
    private float m_current_Speed = 0f;
    
    void    Calculate_Current_Speed(ref Player player)
    {
        if (player.Get_FrameInfo.MoveDirection != PlayerFrameInformation.movedirection.STAY)
        {
            m_current_Speed += player.Get_Param.Get_Acceleration;
            if (m_current_Speed > player.Get_Param.Get_MaxSpeed)
                m_current_Speed = player.Get_Param.Get_MaxSpeed;
        }
        else
        {
            m_current_Speed -= player.Get_Param.Get_BrakeSpeed;
            if (m_current_Speed < 0)
                m_current_Speed = 0;
        }
    }

    private void Move(ref Player player)
    {
        Vector3 right = Camera.main.transform.right.normalized;
        right.z = 0f;
        Vector3 move = right * m_current_Speed * (float)player.Get_FrameInfo.MoveDirection * Time.deltaTime;
        move.y = Physics.gravity.y * Time.deltaTime;
        //player.Get_RigidBody.MovePosition(this.transform.position + move);
        player.m_controller.Move(move);
    }

    void Animation(ref Player player)
    {
        if (player.Get_FrameInfo.MoveDirection == PlayerFrameInformation.movedirection.STAY)
        {
            player.Get_Animator.SetBool("is_Running", false);
        }
        else
            player.Get_Animator.SetBool("is_Running", true);
    }

    void Rotate(ref Player player)
    {
      //  if (!player.Get_FrameInfo.Is_Move)
         //   return;
        //Vector3 rotate_Angle = new Vector3(0, 0, 0);

        //rotate_Angle.x = (player.Get_FrameInfo.m_lastMoveDirection == PlayerFrameInformation.movedirection.LEFT) 
        //                                                                                    ? -1f : 1f;

        //Quaternion rotate_Q = Quaternion.LookRotation(rotate_Angle);
        Vector3 axis = new Vector3(0, 1, 0);
        float angle = (player.Get_FrameInfo.m_lastMoveDirection == PlayerFrameInformation.movedirection.LEFT) ? 0 : 180;
        Quaternion rotate_Q = Quaternion.AngleAxis(angle,axis);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate_Q, 0.1f);
    }

    public override void Enter(ref Player player)
    {

    }
    public override bool Execute(ref Player player) 
    {
        Calculate_Current_Speed(ref player);
        Move(ref player);
        Rotate(ref player);
        Animation(ref player);
        return false;
    }

    public override void Exit_State(ref Player player)
    {

    }
}

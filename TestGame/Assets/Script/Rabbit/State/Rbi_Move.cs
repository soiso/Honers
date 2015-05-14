using UnityEngine;
using System.Collections;

public class Rbi_Move : FieldObjectStateInterface
{

    void Rotate(NewRabbit owner)
    {
      
        Vector3 rotate_Angle = new Vector3(0, 0, 0);
        Vector3 target_vec = owner.m_current_MoveTarget.transform.position - owner.transform.position;
        rotate_Angle.z = (target_vec.x <= 0) ? -1f : 1f;

        Quaternion rotate_Q = Quaternion.LookRotation(rotate_Angle);
        owner.transform.rotation = Quaternion.Slerp(transform.rotation, rotate_Q, 0.1f);
    }

    public override void Enter(GameObject owner)
    {
        NewRabbit rab = owner.GetComponent<NewRabbit>();
        rab.m_animator.SetBool("isMove", true);
      
    }

    private bool    Goal_Check(NewRabbit  owner)
    {
        var target_Collider = owner.m_current_MoveTarget.GetComponent<ObjectTrigger>();
        return (target_Collider.m_is_Active) ? true : false;
    }


    void Wait(NewRabbit rab)
    {

    }

    void Move(NewRabbit rab)
    {
        Vector3 move_direc = rab.m_current_MoveTarget.transform.position - rab.transform.position;
        move_direc.y = 0;
        move_direc.z = 0;
        move_direc.Normalize();

        move_direc = move_direc.normalized * Time.deltaTime;
        
        //this.transform.position += move_direc.normalized * 2f * Time.deltaTime;
        rab.m_controller.Move(move_direc);
    }

    public override bool Execute(GameObject owner)
    {
        NewRabbit rab = owner.GetComponent<NewRabbit>();

        if (!rab.m_CanMove)
        {
            Wait(rab);
        }
        else
        {
            Move(rab);
            Rotate(rab);
        }
        if(Goal_Check(rab))
        {
            rab.m_state_Machine.SetBook_State
                (rab.m_current_MoveTarget.GetComponent<Container_FieldObjectState>().m_state);
            return true;
        }

        return false;
    }

    public override void Exit_State(GameObject owner)
    {
        NewRabbit rab = owner.GetComponent<NewRabbit>();
        rab.m_animator.SetBool("isMove", false);
    }
}

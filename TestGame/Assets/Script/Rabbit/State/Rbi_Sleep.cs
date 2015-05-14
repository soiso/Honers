using UnityEngine;
using System.Collections;

public class Rbi_Sleep : FieldObjectStateInterface
{
    public override void Enter(GameObject owner)
    {
        var rab = owner.GetComponent<NewRabbit>();
        rab.m_animator.SetBool("isSleep", true);
    }

    public override bool Execute(GameObject owner)
    {
        var rab = owner.GetComponent<NewRabbit>();

        if(rab.Is_ActiveTimeZone(rab.m_CurrentTimeZone))
        {
            rab.Set_MoveTarget(rab.m_carrot);
            rab.m_state_Machine.SetBook_State(rab.m_move_State);
            return true;
        }
        return false;
        
    }

    public override void Exit_State(GameObject owner)
    {
        var rab = owner.GetComponent<NewRabbit>();
        rab.m_animator.SetBool("isSleep", false);
    }


}

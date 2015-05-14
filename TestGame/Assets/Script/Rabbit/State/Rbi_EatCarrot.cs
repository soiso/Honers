using UnityEngine;
using System.Collections;

public class Rbi_EatCarrot : FieldObjectStateInterface {

    public override void Enter(GameObject owner)
    {
        var rab = owner.GetComponent<NewRabbit>();
    }

    public override bool Execute(GameObject owner)
    {
        NewRabbit rab = owner.GetComponent<NewRabbit>();

        if (rab.Is_SleepTimeZone(rab.m_CurrentTimeZone))
        {
            rab.Set_MoveTarget(rab.m_sleepSpot);
            rab.m_state_Machine.SetBook_State(rab.m_move_State);
            return true;
        }
        return false;
    }

    public override void Exit_State(GameObject owner)
    {
        NewRabbit rab = owner.GetComponent<NewRabbit>();
    }

}

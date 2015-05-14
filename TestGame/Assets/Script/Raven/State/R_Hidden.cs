using UnityEngine;
using System.Collections;

public class R_Hidden : RavenStateBase 
{

    public override void Enter( RavenStateMachine owner_Machine)
    {
        //モーションせんいここで
        var state_param =  owner_Machine.gameObject.GetComponent<RavenStateParametor>();
        state_param.m_current_WaitTime_Tomarigi = Time.time + state_param.m_wait_Time_Tomarigi;
    }

    public override bool Execute (RavenStateMachine owner_Machine)
    {
        var state_param =  owner_Machine.gameObject.GetComponent<RavenStateParametor>();
        if (Time.time > state_param.m_current_WaitTime_Tomarigi)
            return true;
        
        return false;
    }

    public override void Exit_State(RavenStateMachine owner_Machine)
    {
        var state_param = owner_Machine.gameObject.GetComponent<RavenStateParametor>();
        state_param.m_current_WaitTime_Tomarigi = 0f;
    }

}

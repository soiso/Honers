using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System;
using System.Linq;

public class R_Patrol : RavenStateBase 
{

    void Rotate(ref RavenStateMachine machine)
    {
        Vector3 rotate_Angle = new Vector3(0, 0, 0);

        var Current_MoveTarget = machine.m_owner.m_patrol.Get_MoveTarget();
        if (!Current_MoveTarget)
            return;

        Vector3 target_vec = Current_MoveTarget.transform.position - this.transform.position;
        rotate_Angle.z = (target_vec.x <= 0) ? -1f : 1f;

        Quaternion rotate_Q = Quaternion.LookRotation(rotate_Angle);
        machine.m_owner.transform.rotation = Quaternion.Slerp(transform.rotation, rotate_Q, 0.1f);
    }

    public override void Enter( RavenStateMachine owner_Machine)
    {
        //ここの初期フレームnullReferenceはStartの実行順によるもの
        //とりあえず動いているので放置

        if (!owner_Machine.m_owner.m_patrol)
            return;
        owner_Machine.m_owner.m_patrol.Direction_MoveProgram();
    }

    public override bool Execute (RavenStateMachine owner_Machine)
    {
        owner_Machine.m_owner.m_patrol.Exexute();
        Rotate(ref owner_Machine);
        return false;
    }

    public override void Exit_State(RavenStateMachine owner_Machine)
    {
        owner_Machine.m_owner.m_patrol.Clear_CurrentGoal(); 
    }
}

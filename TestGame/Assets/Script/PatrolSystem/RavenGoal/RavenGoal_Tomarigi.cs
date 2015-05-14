using UnityEngine;
using System.Collections;

public class RavenGoal_Tomarigi : InterFace_WayPointGoal
{
    RavenStateBase m_state = null;

    void Start()
    {
        m_state = gameObject.AddComponent<R_Hidden>();
    }

    public override bool Arrival_Goal(GameObject owner)
    {
        var raven = owner.GetComponent<Raven>();
        if (!raven)
            return false;
        var param = owner.GetComponent<RavenStateParametor>();
       if(param.m_probability_Tomarigi  >= Random.Range(0,101))
       {
           raven.m_state_Machine.SetBookState(raven.m_state_Machine.m_default_state);
           return true;
       }
       return false;
    }
}

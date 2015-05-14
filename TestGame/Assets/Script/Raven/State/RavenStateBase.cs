using UnityEngine;
using System.Collections;

public class RavenStateBase : MonoBehaviour {

    public virtual void Enter( RavenStateMachine owner_Machine) { }
    public virtual bool Execute( RavenStateMachine owner_Machine) { return false; }
    public virtual void Exit_State( RavenStateMachine owner_Machine) { }
}

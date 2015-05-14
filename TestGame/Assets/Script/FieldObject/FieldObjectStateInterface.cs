using UnityEngine;
using System.Collections;

public class FieldObjectStateInterface : MonoBehaviour {

    public virtual void Enter(GameObject owner) { }
    public virtual bool Execute(GameObject owner) { return false; }
    public virtual void Exit_State(GameObject owner) { }
}

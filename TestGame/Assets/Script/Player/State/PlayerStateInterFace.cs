using UnityEngine;
using System.Collections;

public class PlayerStateInterFace : MonoBehaviour
{
    public virtual void Enter(ref Player player) { }
    public virtual bool Execute(ref Player player) { return false; }
    public virtual void Exit_State(ref Player player) { }
}


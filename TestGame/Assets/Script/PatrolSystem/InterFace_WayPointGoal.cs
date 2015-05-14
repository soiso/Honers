using UnityEngine;
using System.Collections;

public class InterFace_WayPointGoal : MonoBehaviour {

    //ゴールとしてもよいかどうか
    public bool m_enabled{get; protected set;}
   
    public void Lock() { m_enabled = false; }
    public void Open() { m_enabled = true; }

    public virtual bool Arrival_Goal(GameObject owner) { return false; }
}

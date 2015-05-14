using UnityEngine;
using System.Collections;

public class Boolean_BoxCollider : MonoBehaviour {

	// Use this for initialization
    public bool m_is_Active { get; private set; }




    void OnTriggerEnter(Collider col_object)
    {

        m_is_Active = true;
    }

    void OnTriggerStay(Collider col_object)
    {
        m_is_Active = true;
    }

    void OnTriggerExit(Collider col_object)
    {
        m_is_Active = false;
    }
}

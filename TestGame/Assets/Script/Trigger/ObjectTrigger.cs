using UnityEngine;
using System.Collections;

public class ObjectTrigger : MonoBehaviour {

    // Use this for initialization
    public bool m_is_Active { get; private set; }

    [SerializeField]
    GameObject owner;


    void OnTriggerEnter(Collider col_object)
    {
        if(col_object.gameObject ==owner)
            m_is_Active = true;
    }

    void OnTriggerStay(Collider col_object)
    {
        if (col_object.gameObject == owner)
            m_is_Active = true;
    }

    void OnTriggerExit(Collider col_object)
    {
        if (col_object.gameObject == owner)
            m_is_Active = false;
    }
}

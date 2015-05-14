using UnityEngine;
using System.Collections;

public class Boolean_EnterCollider : MonoBehaviour {

    public bool m_is_Active { get; private set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col_object)
    {

        m_is_Active = true;
    }

    void OnTriggerStay(Collider col_object)
    {
        m_is_Active = false;
    }

    void OnTriggerExit(Collider col_object)
    {
        m_is_Active = false;
    }
}

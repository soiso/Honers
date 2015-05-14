using UnityEngine;
using System.Collections;

public class SearchCollider : MonoBehaviour {

    public bool m_FindTarget = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col_obj)
    {
        m_FindTarget = true;
    }

    void OnTriggerExit(Collider col_obj)
    {
        m_FindTarget = false;
    }
}

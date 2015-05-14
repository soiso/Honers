using UnityEngine;
using System.Collections;

public class Weight_Collider : MonoBehaviour {

    public bool m_is_Active { get; private set; }
    public int m_currentMaxWeight { get; private set; }

    GameObject m_current_Ride = null;

	// Use this for initialization
	void Start () {
        m_currentMaxWeight = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider col_object)
    {
        m_is_Active = true;
        if(m_current_Ride != col_object.gameObject)
        {
            var weight = col_object.GetComponent<Weight>();
            if(weight)
            {
                 if(m_currentMaxWeight <= weight.m_weight)
                 {
                     m_currentMaxWeight = weight.m_weight;
                     m_current_Ride = col_object.gameObject;
                 }
            }
        }
    }

    void OnTriggerStay(Collider col_object)
    {
        m_is_Active = true;
        if (m_current_Ride != col_object.gameObject)
        {
            var weight = col_object.GetComponent<Weight>();
            if (weight)
            {
                if (m_currentMaxWeight <= weight.m_weight)
                {
                    m_currentMaxWeight = weight.m_weight;
                    m_current_Ride = col_object.gameObject;
                }
            }
        }

    }

    void OnTriggerExit(Collider col_object)
    {
        m_is_Active = false;
        if (m_current_Ride == col_object.gameObject)
        {
                   m_currentMaxWeight =0;
                   m_current_Ride = null;      
            
        }
    }
}

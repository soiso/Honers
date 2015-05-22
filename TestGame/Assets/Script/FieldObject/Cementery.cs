using UnityEngine;
using System.Collections;

public class Cementery : FieldObjectInterface {


    public bool m_is_active { get;  set; }
    private bool m_isonce;
    private TimeZone_BoxCollider m_timezoneCollider;
	// Use this for initialization
	void Start () 
    {
        m_is_active = false;
        m_isonce = false;
        m_timezoneCollider = GetComponentInChildren<TimeZone_BoxCollider>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        Material m = GetComponentInChildren<MeshRenderer>().material;
        m_CurrentTimeZone = m_timezoneCollider.m_myColliderTimeZone;
	    if(Is_ActiveTimeZone(m_CurrentTimeZone) && !m_isonce)
        {
            m_isonce = true;
            m_is_active = true;
            m.color = Color.red;
        }
        if( Is_SleepTimeZone(m_CurrentTimeZone))
        {
            m_isonce = false;
            m_is_active = false;
            m.color = Color.black;
        }

	}
}

using UnityEngine;
using System.Collections;

public class Mole : FieldObjectInterface 
{
    private TimeZone_BoxCollider m_TimeTrigger;

	// Use this for initialization
	void Start ()
    {
        m_TimeTrigger = GetComponentInChildren<TimeZone_BoxCollider>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        m_CurrentTimeZone = m_TimeTrigger.m_myColliderTimeZone;
        //GetComponent<ShaderChanger>().Change(m_CurrentTimeZone);

        if( Is_ActiveTimeZone( m_CurrentTimeZone ) )
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
        }
	}
}

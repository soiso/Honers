using UnityEngine;
using System.Collections;

public class Mole : FieldObjectInterface 
{
    private TimeZone_BoxCollider m_TimeTrigger;
    private Animator m_Animator;

	// Use this for initialization
	void Start ()
    {
        m_TimeTrigger = GetComponentInChildren<TimeZone_BoxCollider>();
        m_Animator = GetComponent<Animator>();
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
            m_Animator.SetBool("isSleep", false);
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            m_Animator.SetBool("isSleep", true);
            GetComponent<BoxCollider>().enabled = false;
        }
	}
}

using UnityEngine;
using System.Collections;

public class Mole : FieldObjectInterface 
{
    private TimeZone_BoxCollider m_TimeTrigger;
    private Animator m_Animator;

    private Transform m_obj;

	// Use this for initialization
	void Start ()
    {
        m_TimeTrigger = GetComponentInChildren<TimeZone_BoxCollider>();
        m_Animator = GetComponent<Animator>();
        m_obj = transform.GetChild(3);
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
            AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime % 1.0f > 0.9f) m_obj.localScale = transform.localScale;
        }
        else
        {
            m_Animator.SetBool("isSleep", true);
            GetComponent<BoxCollider>().enabled = false;
            AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime%1.0f > 0.993f) m_obj.localScale = new Vector3(transform.localScale.x, .0f, transform.localScale.z);          
        }
	}
}

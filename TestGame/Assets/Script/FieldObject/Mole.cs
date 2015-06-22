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
            if (info.normalizedTime % 1.0f > 0.05f)
            {
                m_obj.localScale = new Vector3(.0f, .0f, .0f);
                m_obj.localScale = transform.localScale;
            }
        }
        else
        {
            m_Animator.SetBool("isSleep", true);
            GetComponent<BoxCollider>().enabled = false;
            m_Animator.Update(Time.deltaTime);
            AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);

            float time = info.normalizedTime % 1.0f;
            if (time > 0.95f)
            {
                m_obj.GetComponent<BoxCollider>().enabled = false;
                m_obj.localScale = new Vector3(.0f, .0f, .0f);
            }
        }
	}
}

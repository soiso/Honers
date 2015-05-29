using UnityEngine;
using System.Collections;

public class Hamster : MonoBehaviour {


    private TimeZone_BoxCollider m_collider;

    public PanelParametor.TIMEZONE m_time_Zone { get; private set; }

    private Animator m_animator;

    [SerializeField]
    private GameObject m_kassha;

	void Start () 
    {
        m_collider = GetComponentInChildren<TimeZone_BoxCollider>();
        m_animator = GetComponentInChildren<Animator>();
        //m_kassha = this.gameObject.transform.Find("kassha").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_time_Zone = m_collider.m_myColliderTimeZone;
        switch(m_time_Zone)
        {
            case PanelParametor.TIMEZONE.morning :
                m_animator.SetBool("isDush", false);
                m_animator.SetBool("isSleep", true);
                break;

            case PanelParametor.TIMEZONE.noon :
                m_animator.SetBool("isDush", false);
                m_animator.SetBool("isSleep", true);
                break;

            case PanelParametor.TIMEZONE.night :
                m_animator.SetBool("isDush", true);
                m_animator.SetBool("isSleep", false);
                m_kassha.transform.Rotate(new Vector3(0, 0, -1), 2.5f);
                break;
        }
	}
}

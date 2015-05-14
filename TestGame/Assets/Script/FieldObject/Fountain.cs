using UnityEngine;
using System.Collections;

public class Fountain : FieldObjectInterface
{
    private TimeZone_BoxCollider m_TriggerCollider;

    [SerializeField, HeaderAttribute("目的地")]
    Transform[] m_TargetTransForm;
    private Vector3 m_Default_Position;

    [SerializeField, Range(0, 1), HeaderAttribute("噴き出すスピード")]
    private float m_SpoutSpeed;

    private float m_CurrentLeap = .0f;

    private DamageTrigger m_DamageTrigger;

	// Use this for initialization
	void Start () 
    {
        m_TriggerCollider = transform.parent.GetComponentInChildren<TimeZone_BoxCollider>();
        m_Default_Position = transform.position;
        m_DamageTrigger = transform.GetComponentInChildren<DamageTrigger>();
        m_DamageTrigger.OnCollisionBegin();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        if( m_CurrentTimeZone != m_TriggerCollider.m_myColliderTimeZone )
        {
            m_CurrentLeap = .0f;
        }
        m_CurrentTimeZone = m_TriggerCollider.m_myColliderTimeZone;

        Spout();
	}

    //噴き出す
    void Spout()
    {
        m_CurrentLeap += m_SpoutSpeed;
        m_CurrentLeap = Mathf.Clamp(m_CurrentLeap, 0, 1);
        int index = (int)m_CurrentTimeZone;
        this.transform.position = Vector3.Lerp(this.transform.position, m_TargetTransForm[index].transform.position, m_CurrentLeap);

    }

    //静まる
    void Calm()
    {

    }
}

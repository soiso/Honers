using UnityEngine;
using System.Collections;

public class Asagao : FieldObjectInterface {

    TimeZone_BoxCollider m_trigger_Collider;

    [SerializeField]
    Transform m_target_TransForm;
    private Vector3 m_default_Position;

    [SerializeField,Range(0,1)]
    private float m_glowSpeed;

    private Animator m_Animator;
    private BoxCollider m_Collider;
    
    //とりあえず
    private float m_current_Leap = 0f;

    [SerializeField, HeaderAttribute("伸びる速度")]
    private float m_animation_Speed = .8f;

    [SerializeField, HeaderAttribute("伸縮SE")]
    public AudioClip m_SE;
    private AudioSource m_Souce;

	// Use this for initialization
	void Start () 
    {
        m_trigger_Collider = this.transform.GetComponentInChildren<TimeZone_BoxCollider>();
        m_default_Position = this.transform.position;
        m_Animator = gameObject.GetComponent<Animator>();
        //m_Animator.speed = 0;
        m_Animator.speed = m_animation_Speed;
        m_Souce = GetComponent<AudioSource>();
        m_Souce.clip = m_SE;
	}

    void Glow()
    {
        //m_current_Leap += m_glowSpeed;
        //m_current_Leap = Mathf.Clamp(m_current_Leap, 0, 1);
        //this.transform.position = Vector3.Lerp(this.transform.position, m_target_TransForm.position, m_current_Leap);
        //m_Animator.speed = m_glowSpeed;
        m_Animator.SetBool("isSleep", false);
        m_Animator.SetBool("isGlow", true);
    }

    void    Sleep()
    {
        //m_current_Leap += m_glowSpeed;
        //m_current_Leap = Mathf.Clamp(m_current_Leap, 0, 1);
        //this.transform.position = Vector3.Lerp(this.transform.position, m_default_Position, m_current_Leap);
        //m_Animator.speed = -m_glowSpeed;
        m_Animator.SetBool("isGlow", false);
        m_Animator.SetBool("isSleep", true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        if (m_CurrentTimeZone != m_trigger_Collider.m_myColliderTimeZone)
        {
            m_current_Leap = .0f;
        }
        if( m_CurrentTimeZone != m_trigger_Collider.m_myColliderTimeZone )
        {
            m_Souce.Play();
        }
        m_CurrentTimeZone = m_trigger_Collider.m_myColliderTimeZone;
        GetComponent<ShaderChanger>().Change(m_CurrentTimeZone);
        if (Is_ActiveTimeZone(m_CurrentTimeZone))
        {
            Glow();
        }
        else
        {
            Sleep();
        }

	}
}

using UnityEngine;
using System.Collections;

public class PicturePaper : MonoBehaviour {

	// Use this for initialization
    [SerializeField]
    private bool m_delete_Child = true;
    public bool m_move { get; private set; }
    [SerializeField,Range(0.1f,500)]
    private float m_rotate_Speed = 10.0f;
    [SerializeField,Range(0.1f, 50)]
    private float m_move_Speed = 10.0f;
    [SerializeField]
    private Transform m_MoveTarget;

    private float m_default_position_z = 5.0f;
    [SerializeField,Range(0,0.1f)]
    private float m_leap_leapspeed = 0.01f;

    private float m_current_Leep = 0f;

    public AudioClip clip;
    private AudioSource audio;


	void Start ()
    {
        m_move = false;
        audio = GetComponent<AudioSource>();
        audio.clip = clip;
	}

    void Leap_PositionZ()
    {
        float pos_z = Mathf.Lerp(m_default_position_z, 0f, m_current_Leep);
        Vector3 new_pos = this.transform.position;
        new_pos.z = pos_z;
        this.transform.position = new_pos;
        m_current_Leep += m_leap_leapspeed;
        m_current_Leep = Mathf.Clamp(m_current_Leep, 0f, 1f);
    }
	
    void    Move()
    {
        if(!m_MoveTarget)
        {
            m_MoveTarget = GameObject.Find("MoveTarget").transform;
        }
        Vector3 axis = new Vector3(0,0,1);
        //float rotate_Angle = m_rotate_Speed * Time.deltaTime;
        float rotate_Angle = m_rotate_Speed * 0.03f;
        this.transform.Rotate(axis, rotate_Angle);

        Vector3 move_Vec = m_MoveTarget.position - this.transform.position;
        move_Vec.Normalize();
        //this.transform.position += move_Vec * m_move_Speed * Time.deltaTime
        this.transform.position += move_Vec * m_move_Speed * 0.03f;
    }

	// Update is called once per frame
	void Update () 
    {
        
        if(m_move)
        {
            Move();
        }
        else
        {
            Leap_PositionZ();
        }
	}

    public void Move_Begin()
    {
        m_move = true;
        //GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        Objectmanager.m_instance.m_BGM.Stop();
    }
    public float GetTargetRange()
    {
        Vector3 target_range = m_MoveTarget.position - this.transform.position;
        float range_X = target_range.x * target_range.x;
        float range_Y = target_range.y * target_range.y;
        float range_Z = target_range.z * target_range.z;
        return Mathf.Sqrt(range_X + range_Y + range_Z);
    }

    public void SetMove_Target(string target_name)
    {
        GameObject target = GameObject.Find(target_name);
        m_MoveTarget = target.transform;
    }
    public void SoundPlay()
    {
        audio.Play();

    }
}

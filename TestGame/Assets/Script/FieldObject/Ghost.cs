using UnityEngine;
using System.Collections;

public class Ghost : FieldObjectInterface {

    [SerializeField,HeaderAttribute("所属している墓(要Cementery.cs)")]
    private GameObject m_owner_GameObject;
    private Cementery m_ownerCementery;

    [SerializeField,HeaderAttribute("出現位置")]
    Transform m_dafault_Position;

    private float  m_current_Alpha = 0f;

    [SerializeField, HeaderAttribute("消えるまでのスピード")]
    private float m_erase_speed =0.05f;

    enum MOVE_DIRECTION
    {
        right,
        left,
        stay,
    }

    public TimeZone_BoxCollider m_rightTimetrigger { get; private set; }  //時間帯の判別用
    public TimeZone_BoxCollider m_leftTimetrigger { get; private set; }

    [SerializeField]
    private MOVE_DIRECTION m_default_MoveDirection = MOVE_DIRECTION.right;

    [SerializeField, Range(2f, 5f)]
    private float m_max_MoveSpeed;
    [SerializeField, Range(0.1f, 3f)]
    private float m_acceleration = 0.1f;   //加速度
    private float m_current_MoveSpeed = 0;
    private MOVE_DIRECTION m_current_MoveDirection;

    private DamageTrigger m_damage_Trigger;

	// Use this for initialization
	void Start () 
    {
        if(!m_owner_GameObject)
        {
            Debug.Log(gameObject.name +"OwnerGameobj is Null !!");
        }
        m_ownerCementery = m_owner_GameObject.GetComponent<Cementery>();
        //this.transform.position = m_dafault_Position.position;

        m_rightTimetrigger = this.transform.GetChild(2).GetComponent<TimeZone_BoxCollider>();
        m_leftTimetrigger = this.transform.GetChild(3).GetComponent<TimeZone_BoxCollider>();

        m_damage_Trigger = GetComponentInChildren<DamageTrigger>();
	}

    private void Sub_Alpha()
    {
        m_current_Alpha = Mathf.Clamp(m_current_Alpha - m_erase_speed, 0, 1);
        if(m_current_Alpha <=0)
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            m_damage_Trigger.onCollisionEnd();
        }
    }

    private void Add_Alpha()
    {
        if (m_current_Alpha <= 0)
        {
            GetComponentInChildren<MeshRenderer>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            this.transform.position = m_dafault_Position.position;
            m_damage_Trigger.OnCollisionBegin();
        }

        m_current_Alpha = Mathf.Clamp(m_current_Alpha + m_erase_speed, 0, 1);
    }

    void Move()
    {
        Vector3 move_Vec = new Vector3(0, 0, 0);
        move_Vec.x = (m_current_MoveDirection == MOVE_DIRECTION.right) ? 1 : -1;
        m_current_MoveSpeed += m_acceleration;
        m_current_MoveSpeed = Mathf.Clamp(m_current_MoveSpeed, 0, m_max_MoveSpeed);

        move_Vec = move_Vec * m_current_MoveSpeed * Time.deltaTime;

        this.transform.Translate(move_Vec);

        //自分の進行方向先がSleepmまたはフィールド外の場合は進路を変える
        if (m_current_MoveDirection == MOVE_DIRECTION.right)
        {
            if (Is_SleepTimeZone(m_rightTimetrigger.m_myColliderTimeZone) ||
                m_rightTimetrigger.m_is_field_Out ||
                m_rightTimetrigger.m_touch_MainStageObject ||
                m_rightTimetrigger.m_touch_FieldObject)
            {
                m_current_MoveDirection = MOVE_DIRECTION.left;

            }
        }
        else
        {
            if (Is_SleepTimeZone(m_leftTimetrigger.m_myColliderTimeZone) ||
                m_leftTimetrigger.m_is_field_Out ||
                m_leftTimetrigger.m_touch_MainStageObject ||
                m_leftTimetrigger.m_touch_FieldObject)
            {
                m_current_MoveDirection = MOVE_DIRECTION.right;
            }
        }
    }

    void Threaten()
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
        if (m_ownerCementery.m_is_active)
        {
            Add_Alpha();
            Move();
        }
        else
        {
            Sub_Alpha();
        }

        Material m = GetComponentInChildren<MeshRenderer>().material;
        var col = new Color(m.color.r,m.color.g,m.color.b,m_current_Alpha);
        m.color = col;

	}
}

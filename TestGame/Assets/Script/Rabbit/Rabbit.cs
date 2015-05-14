using UnityEngine;
using System.Collections;

public class Rabbit : FieldObjectInterface
{
    public Animator m_animator { get; set; }
    public CharacterController m_controller { get; set; }
    public TimeZone_BoxCollider m_rightTimetrigger { get; private set; }
    public TimeZone_BoxCollider m_leftTimetrigger { get; private set; }

   

    PanelParametor.TIMEZONE m_ColliderTimezone;

    enum MOVE_DIRECTION
    {
        right,
        left,
        stay,
    }

    [SerializeField]
    private MOVE_DIRECTION m_default_MoveDirection = MOVE_DIRECTION.right;
    [SerializeField, Range(2f, 5f)]
    private float m_max_MoveSpeed;
    [SerializeField, Range(0.1f, 3f)]
    private float m_acceleration = 0.1f;
    private float m_current_MoveSpeed = 0;

    private MOVE_DIRECTION m_current_MoveDirection;

    void Start()
    {        m_controller = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_rightTimetrigger = this.transform.GetChild(0).GetComponent<TimeZone_BoxCollider>();
        m_leftTimetrigger = this.transform.GetChild(2).GetComponent<TimeZone_BoxCollider>();
        m_current_MoveDirection = m_default_MoveDirection;
        m_Particle = GetComponentInChildren<ParticleSystem>();
    }

    void Calculate_CurrentTimeZone()
    {
        int[] counter = new int[3];
        for (int i = 0; i < 3; i++)
            counter[0] = 0;

        counter[(int)m_leftTimetrigger.m_myColliderTimeZone]++;
        counter[(int)m_rightTimetrigger.m_myColliderTimeZone]++;
        counter[(int)m_ColliderTimezone]++;
        //一番大きい数字を現在のタイムゾーンに
        int work = 0;
        int max_index = 0;
        for (int i = 0; i < 3; i++)
        {
            if (counter[i] >= work)
            {
                max_index = i;
                work = counter[i];
            }
        }
        m_CurrentTimeZone = (PanelParametor.TIMEZONE)max_index;
    }
    void Sleep()
    {
        m_current_MoveSpeed = 0;
        Vector3 move_Vec = new Vector3(0, 0, 0);
        move_Vec.y = Physics.gravity.y * Time.deltaTime;
        m_controller.Move(move_Vec);
    }

    void Move()
    {
        Vector3 move_Vec = new Vector3(0, 0, 0);
        move_Vec.x = (m_current_MoveDirection == MOVE_DIRECTION.right) ? 1 : -1;
        m_current_MoveSpeed += m_acceleration;
        m_current_MoveSpeed = Mathf.Clamp(m_current_MoveSpeed, 0, m_max_MoveSpeed);

        move_Vec = move_Vec * m_current_MoveSpeed * Time.deltaTime;
        move_Vec.y = Physics.gravity.y * Time.deltaTime;
        m_controller.Move(move_Vec);

        //自分の進行方向先がSleepmまたはフィールド外の場合は進路を変える
        if (m_current_MoveDirection == MOVE_DIRECTION.right)
        {
            if (Is_SleepTimeZone(m_rightTimetrigger.m_myColliderTimeZone) ||
                m_rightTimetrigger.m_is_field_Out ||
                m_rightTimetrigger.m_touch_MainStageObject ||
                m_rightTimetrigger.m_touch_FieldObject)
            {
                m_current_MoveDirection = MOVE_DIRECTION.left;
                m_current_MoveSpeed = 0;
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
                m_current_MoveSpeed = 0;
            }
        }
    }

    void Update()
    {
        Calculate_CurrentTimeZone();
        if (Is_ActiveTimeZone(m_CurrentTimeZone))
        {
            Move();
            SleepEffectPlay();
        }
        else
        {
            Sleep();
        }
    }

    void OnTriggerEnter(Collider col_object)
    {
        //レイヤー名取得
        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        if (layer_name == "Panel")
        {
            var param = col_object.GetComponent<PanelParametor>();
            m_ColliderTimezone = param.GetTimezone;
        }
    }

    void OnTriggerStay(Collider col_object)
    {
        //レイヤー名取得
        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        if (layer_name == "Panel")
        {
            var param = col_object.GetComponent<PanelParametor>();
            m_ColliderTimezone = param.GetTimezone;
        }
    }

    void OnTriggerExit(Collider col_object)
    {
        //レイヤー名取得
        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        if (layer_name == "Panel")
        {
            var param = col_object.GetComponent<PanelParametor>();
            m_ColliderTimezone = param.GetTimezone;
        }
    }
}
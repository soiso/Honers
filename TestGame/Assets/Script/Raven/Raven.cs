using UnityEngine;
using System.Collections;

public class Raven : FieldObjectInterface
{
    [SerializeField, Range( 10.0f, 50.0f)]
    private float m_DepriveScore;
    [SerializeField, Range(2.0f, 10.0f)]
    private float m_MaxSpeed;
 
    private float m_CurrentSpeed = .0f;

    private SphereCollider m_Search;

    public RavenStateMachine m_state_Machine { get; private set; }

    public Patrol m_patrol { get; private set; }
    [SerializeField, HeaderAttribute("ダメージ食らったときの無敵時間 / 秒")]
    private float m_strong_second = 2;

    private float m_lift_strongTime = 0;
    public bool m_is_strong { get; private set; }

    private FruitDropper m_dropper;
    public Animator m_Animator{get; private set;}

    public AudioClip clip;
    private AudioSource audio;
	void Start ()
    {
        m_Search = this.transform.GetChild(2).GetComponent<SphereCollider>();
        //SphereCollider.m_Search = this.GetComponentInChildren<SphereCollider>();
        m_state_Machine = GetComponent<RavenStateMachine>();
        m_patrol = GetComponent<Patrol>();
        m_is_strong = false;
        m_dropper = GetComponent<FruitDropper>();
        m_Animator = gameObject.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        audio.clip = clip;
	}

    void Update_Strong()
    {
        if (!m_is_strong)
            return;

        if (m_lift_strongTime < Time.time)
        {
            m_is_strong = false;
            m_Animator.SetBool("isDamage", false);
        }
    }

	// Update is called once per frame
	void Update () 
    {
        m_state_Machine.Execute();
        Update_Strong();

        Debug.Log("CurrentState : " + m_state_Machine.m_current_State.ToString());

        //仮でSearch範囲内にプレイヤーがいたら挑発するよ
        if (m_Search.GetComponent<SearchCollider>().m_FindTarget)
            m_Animator.SetBool("isProvocation", true);
        else
            m_Animator.SetBool("isProvocation", false);

        
	}



    void OnTriggerEnter(Collider col_object)
    {
        ////レイヤー名取得
        //string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        //if (layer_name == "Panel")
        //{
        //    var param = col_object.GetComponent<PanelParametor>();
        //    m_ColliderTimezone = param.GetTimezone;
        //}
        //else if( layer_name == "Player")
        //{
        //    col_object.gameObject.GetComponent<Player>().m_score.SubScore(m_DepriveScore);
        //}
    }

    void OnTriggerStay(Collider col_object)
    {
        ////レイヤー名取得
        //string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        //if (layer_name == "Panel")
        //{
        //    var param = col_object.GetComponent<PanelParametor>();
        //    m_ColliderTimezone = param.GetTimezone;
        //}
        //else if (layer_name == "Player")
        //{
        //    //col_object.gameObject.GetComponent<Player>().m_score.SubScore(m_DepriveScore);
        //}
    }

    void OnTriggerExit(Collider col_object)
    {
        ////レイヤー名取得
        //string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        //if (layer_name == "Panel")
        //{
        //    var param = col_object.GetComponent<PanelParametor>();
        //    m_ColliderTimezone = param.GetTimezone;
        //}
        //else if (layer_name == "Player")
        //{

        //}
    }

    //void Move()
    //{
    //    Vector3 move_Vec = new Vector3(0, 0, 0);
    //    move_Vec.x = (m_Current_MoveDirection == MOVE_DIRECTION.right) ? 1 : -1;
    //    //m_CurrentSpeed = Calculate_Acceleration(m_CurrentTimeZone);

    //    move_Vec = move_Vec * m_CurrentSpeed * Time.deltaTime;

    //    //自分の進行方向先がSleepmまたはフィールド外の場合は進路を変える
    //    if (m_Current_MoveDirection == MOVE_DIRECTION.right)
    //    {
    //        if (/*Is_SleepTimeZone(m_RightTimetrigger.m_myColliderTimeZone) ||*/
    //            m_RightTimetrigger.m_is_field_Out ||
    //            m_RightTimetrigger.m_touch_MainStageObject /*||
    //            m_RightTimetrigger.m_touch_FieldObject*/)
    //        {
    //            m_Current_MoveDirection = MOVE_DIRECTION.left;
    //            m_CurrentSpeed = 0;
    //        }
    //        else transform.position += move_Vec;
    //    }
    //    else
    //    {
    //        if (/*Is_SleepTimeZone(m_LeftTimetrigger.m_myColliderTimeZone) ||*/
    //            m_LeftTimetrigger.m_is_field_Out ||
    //            m_LeftTimetrigger.m_touch_MainStageObject/* ||
    //            m_LeftTimetrigger.m_touch_FieldObject*/)
    //        {
    //            m_Current_MoveDirection = MOVE_DIRECTION.right;
    //            m_CurrentSpeed = 0;
    //        }
    //        else
    //        {
    //            transform.position += move_Vec;
    //        }
    //    }
    //}

    //void Attack()
    //{
    //    Vector3 Pos = transform.position;
    //    Vector3 PlayerPos = GameObject.Find("Player").transform.position;
    //    PlayerPos.y += 1.0f;
    //    Vector3 MoveVec = PlayerPos - Pos;
    //    Vector3.Normalize(MoveVec);

    //   // m_CurrentSpeed += Calculate_Acceleration(m_CurrentTimeZone);
    //    m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, .0f, m_MaxSpeed);

    //    Pos += MoveVec * m_CurrentSpeed * Time.deltaTime;

    //    //自分の進行方向先がSleepmまたはフィールド外の場合は進路を変える
    //    if (MoveVec.x > 0)
    //    {
    //        if (/*Is_SleepTimeZone(m_RightTimetrigger.m_myColliderTimeZone) ||*/
    //            m_RightTimetrigger.m_is_field_Out ||
    //            m_RightTimetrigger.m_touch_MainStageObject /*||
    //            m_RightTimetrigger.m_touch_FieldObject*/)
    //        {
    //            m_CurrentSpeed = .0f;
    //        }
    //        else
    //            transform.position = Pos;
    //    }
    //    else
    //    {
    //        if (/*Is_SleepTimeZone(m_LeftTimetrigger.m_myColliderTimeZone) ||*/
    //            m_LeftTimetrigger.m_is_field_Out ||
    //            m_LeftTimetrigger.m_touch_MainStageObject /*||
    //            m_LeftTimetrigger.m_touch_FieldObject*/)
    //        {
    //            m_CurrentSpeed = .0f;
    //        }
    //        else
    //            transform.position = Pos;
    //    }
    //}



    public override void Damage(DamageTrigger.DamageObject damage_info)
    {
        if (m_is_strong)
            return;
        //ここでモーション遷移
        m_Animator.SetBool("isDamage", true);

        audio.Play();
        m_is_strong = true;
        m_lift_strongTime = Time.time + m_strong_second;
        m_dropper.Drop();
    }
}

using UnityEngine;
using System.Collections;

public class TrapFlower : FieldObjectInterface
{

    [SerializeField]
    private PanelParametor.TIMEZONE m_DefalutTimeZone = PanelParametor.TIMEZONE.morning;      //デフォルト行動
    private PanelParametor.TIMEZONE m_ColliderTimeZone;

    private BoxCollider m_MeshCollider;
    public TimeZone_BoxCollider m_LeftCollider { get; private set; }
    public TimeZone_BoxCollider m_RightCollider { get; private set; }

    public Animator m_Animator{ get; set; }

    private DamageTrigger m_Trigger;

    [SerializeField, Range(1, 10), HeaderAttribute("攻撃後の放心状態秒数")]
    private int m_NoneActionSecond;
    private int m_NoneActionTimer = 0;

    [SerializeField, HeaderAttribute("プレイヤーダメージ（減点）")]
    private int m_SnatchPoint;

	// Use this for initialization
	void Start ()
    {
        m_MeshCollider = transform.GetChild(1).GetComponent<BoxCollider>();
        m_LeftCollider = transform.GetChild(2).GetComponent<TimeZone_BoxCollider>();
        m_RightCollider = transform.GetChild(3).GetComponent<TimeZone_BoxCollider>();

        m_Particle = GetComponentInChildren<ParticleSystem>();
        m_Animator = GetComponent<Animator>();

        //  ダメージTrigger
        m_Trigger = gameObject.GetComponentInChildren<DamageTrigger>();
        //m_Trigger.OnCollisionBegin();

	}
	
	// Update is called once per frame
	void Update () 
    {
        if( m_NoneActionTimer > 0 )
        {
            m_NoneActionTimer--;
            return;
        }

        Calculate_CurrentTimeZone();

        if( Is_ActiveTimeZone( m_CurrentTimeZone ) )
        {
            //m_Trigger.OnCollisionBegin();
            Action();
        }
        else if ( Is_SleepTimeZone(m_CurrentTimeZone) )
        {
            Sleep();
        }
	
	}

    private void Action()
    {
        m_MeshCollider.enabled = true;
    }

    private void Sleep()
    {
        m_MeshCollider.enabled = false;
    }

    /*
     * 
     *      トリガー関係
     * 
     */
    void OnTriggerEnter(Collider col_object)
    {
        //レイヤー名取得
        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        if (layer_name == "Panel")
        {
            var param = col_object.GetComponent<PanelParametor>();
            m_ColliderTimeZone = param.GetTimezone;
        }
        else if (layer_name == "Player" || layer_name == "FieldObject")
        {
            m_Animator.SetBool("isAttack", true);
        }

    }

    void OnTriggerExit( Collider col_obj)
    {
        //レイヤー名取得
        string layer_name = LayerMask.LayerToName(col_obj.gameObject.layer);
        if (layer_name == "Player" || layer_name == "FieldObject")
        {
            m_Animator.SetBool("isAttack", false);
            m_Animator.SetBool("isStay", true);
        }
    }

    //void OnCollisionEnter(Collision col_obj)
    //{
    //    //レイヤー名取得
    //    string layer_name = LayerMask.LayerToName(col_obj.gameObject.layer);
    //    if (layer_name == "Player" || layer_name == "FieldObject")
    //    {
    //        m_Animator.SetBool("isAttack", true);
    //        m_Trigger.OnCollisionBegin();
    //    }
    //}

    //void OnCollisionExit(Collision col_obj)
    //{
    //    //レイヤー名取得
    //    string layer_name = LayerMask.LayerToName(col_obj.gameObject.layer);
    //    if (layer_name == "Player" || layer_name == "FieldObject")
    //    {
    //        m_Animator.SetBool("isAttack", false);
    //        m_NoneActionTimer = m_NoneActionSecond * 60;
    //        m_Trigger.onCollisionEnd();
    //    }
    //}

    /*
     * 
     *      時間帯系
     * 
     */
    //自分の所属する時間帯の取得
    private void Calculate_CurrentTimeZone()
    {
        int[] counter = new int[3];
        for (int i = 0; i < 3; i++)
            counter[0] = 0;

        counter[(int)m_LeftCollider.m_myColliderTimeZone]++;
        counter[(int)m_RightCollider.m_myColliderTimeZone]++;
        counter[(int)m_ColliderTimeZone]++;
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
        if ((PanelParametor.TIMEZONE)max_index == m_CurrentTimeZone) return;

        m_CurrentTimeZone = (PanelParametor.TIMEZONE)max_index;
        if (Is_SleepTimeZone(m_CurrentTimeZone))
        {
            SleepEffectPlay();
            m_Animator.SetBool("isSleep", true);
        }
        if (Is_ActiveTimeZone(m_CurrentTimeZone))
        {
            SleepEffectStop();
            m_Animator.SetBool("isStay", true);
        }

        GetComponent<ShaderChanger>().Change( m_CurrentTimeZone);
            
    }

    public void SetTimer()
    {
        m_NoneActionTimer = m_NoneActionSecond * 60;
    }

}

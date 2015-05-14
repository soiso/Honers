using UnityEngine;
using System.Collections;

public class Frog : FieldObjectInterface
{
    private PanelParametor.TIMEZONE m_ColliderTimeZone;

    public TimeZone_BoxCollider m_LeftCollider { get; private set; }
    public TimeZone_BoxCollider m_RightCollider { get; private set; }
    private SearchCollider m_SearchZone;

    [SerializeField, HeaderAttribute("潜る位置")]
    private Transform m_TargetTransform;
    private Vector3 m_DefaultPosition;

    [SerializeField, Range(0.1f, 5.0f), HeaderAttribute("潜るスピード")]
    private float m_DiveSpeed;

    private float m_CurrentLerp = .0f;

    private Vector3 m_TargetDir;
    [SerializeField, HeaderAttribute("水玉の同時出現最大数")]
    private int m_MaxWater;
    public int m_CurrentWater = 0;
    [SerializeField, HeaderAttribute("攻撃後の放心状態の秒数")]
    private int m_NoneActionSecond;
    [SerializeField, HeaderAttribute("攻撃に使うモデル")]
    private GameObject m_AttackModel;
    [SerializeField, Range(0.1f, 5.0f), HeaderAttribute("水玉の速度")]
    private float m_WaterSpeed;
    private int m_NoneActionTimer = 0;
    private bool m_FindEnemy = false;

    [SerializeField, HeaderAttribute("発射口")]
    Transform m_Mouse;

	// Use this for initialization
	void Start ()
    {
        m_RightCollider = transform.GetChild(0).GetComponent<TimeZone_BoxCollider>();
        m_LeftCollider = transform.GetChild(1).GetComponent<TimeZone_BoxCollider>();
        m_SearchZone = transform.GetChild(2).GetComponent<SearchCollider>();
        m_DefaultPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        Calculate_CurrentTimeZone();

        if( m_NoneActionTimer > 0 )
        {
            m_NoneActionTimer--;
            return;
        }

        if( Is_ActiveTimeZone( m_CurrentTimeZone ) )
        {
            m_SearchZone.GetComponent<SphereCollider>().enabled = true;

            Search();

            if( m_SearchZone.m_FindTarget && m_CurrentLerp >= 1.0f )
            {
                Attack();
            }
        }
        else
        {
            m_SearchZone.m_FindTarget = false;
            m_SearchZone.GetComponent<SphereCollider>().enabled = false;
            Sleep();
        }
	}

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
        m_CurrentLerp = .0f;
        m_CurrentTimeZone = (PanelParametor.TIMEZONE)max_index;
    }

    void Attack()
    {
        if (m_CurrentWater >= m_MaxWater) return;

        //とりあえず近い方に攻撃
        Vector3 p = GameObject.Find("Player").transform.position;
        p.y += 0.5f;
        Vector3 r = GameObject.Find("Raven").transform.position;

        if( Vector3.Distance( p, this.transform.position ) < Vector3.Distance( r, this.transform.position ) )
        {
            m_TargetDir = p - this.transform.position;
        }
        else
        {
            m_TargetDir = r - this.transform.position;
        }
        m_TargetDir = Vector3.Normalize( m_TargetDir );

        //弾発射
        GameObject obj = Instantiate(m_AttackModel);
        obj.transform.position = m_Mouse.position;
        obj.transform.rotation = m_Mouse.rotation;

        obj.GetComponent<FrogAttack>().SetParam(m_TargetDir, m_WaterSpeed);

        m_CurrentWater++;
        m_NoneActionTimer = m_NoneActionSecond * 60;

    }

    void Search()
    {
        m_CurrentLerp += m_DiveSpeed;
        m_CurrentLerp = Mathf.Clamp(m_CurrentLerp, 0, 1);
        this.transform.position = Vector3.Lerp(this.transform.position, m_DefaultPosition, m_CurrentLerp);
    }

    void Sleep()
    {
        m_CurrentLerp += m_DiveSpeed;
        m_CurrentLerp = Mathf.Clamp(m_CurrentLerp, 0, 1);
        this.transform.position = Vector3.Lerp(this.transform.position, m_TargetTransform.position, m_CurrentLerp);
    }

    public void SetTimer()
    {
        m_NoneActionTimer = m_NoneActionSecond * 60;
    }

    public void SubWater()
    {
        m_CurrentWater--;
        if (m_CurrentWater < 0) m_CurrentWater = 0;
    }
}

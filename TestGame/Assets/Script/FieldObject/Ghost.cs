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

    [SerializeField, HeaderAttribute("現在ステージのパネルまとめたやつ(PanelChanger)")]
    private GameObject m_changer;

    private Vector3 m_TargetPos;
    private Animator m_Animator;
    private bool m_Sporn;
    private bool m_Threaten;
    private FruitInterFace.FRUIT_TYPE m_type;   //奪ったフルーツの種類
    private int m_index;
    private int m_target_index;
    private bool m_isChange;

	// Use this for initialization
	void Start () 
    {
        if(!m_owner_GameObject)
        {
            Debug.Log(gameObject.name +"OwnerGameobj is Null !!");
        }
        m_ownerCementery = m_owner_GameObject.GetComponent<Cementery>();
        //this.transform.position = m_dafault_Position.position;

        m_rightTimetrigger = this.transform.GetChild(1).GetComponent<TimeZone_BoxCollider>();
        m_leftTimetrigger = this.transform.GetChild(2).GetComponent<TimeZone_BoxCollider>();

        m_Animator = GetComponent<Animator>();
        m_Sporn = false;
        m_Threaten = false;
        m_isChange = false;
	}

    private void Sub_Alpha()
    {
        m_current_Alpha = Mathf.Clamp(m_current_Alpha - m_erase_speed, 0, 1);
        if(m_current_Alpha <=0)
        {
            SkinnedMeshRenderer[] renderer = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach( var r in renderer )
            {
                r.enabled = false;
            }
            GetComponent<CapsuleCollider>().enabled = false;
            m_Sporn = false;
            m_Threaten = false;
            m_isChange = false;
        }
    }

    private void Add_Alpha()
    {
        if (m_current_Alpha <= 0)
        {
            SkinnedMeshRenderer[] renderer = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var r in renderer)
            {
                r.enabled = true;
            }
            GetComponent<CapsuleCollider>().enabled = true;
            this.transform.position = m_dafault_Position.position;
        }

        m_current_Alpha = Mathf.Clamp(m_current_Alpha + m_erase_speed, 0, 1);
        if( m_current_Alpha >= 1.0f )
        {
            m_Sporn = true;
            m_TargetPos = GameObject.Find("Player").transform.position;
            m_TargetPos.z = .0f;
        }
    }

    void Move()
    {
        if (!m_Threaten)
        {
            m_TargetPos = GameObject.Find("Player").transform.position;
            m_TargetPos.z = .0f;
        }
        Vector3 move_Vec = m_TargetPos - transform.position;
        if( m_Threaten && Vector3.Distance( transform.position, m_TargetPos ) < 0.5f )
        {
            if (!m_isChange)
            {
                if( m_type != FruitInterFace.FRUIT_TYPE.error )
                {
                    DropFruit(m_type);
                }
                ChangePanel();
            }
            m_isChange = true;
        }
        m_current_MoveSpeed += m_acceleration;
        m_current_MoveSpeed = Mathf.Clamp(m_current_MoveSpeed, 0, m_max_MoveSpeed);
        move_Vec.Normalize();
        move_Vec = move_Vec * m_current_MoveSpeed * Time.deltaTime;

        this.transform.Translate(move_Vec);

        m_Animator.SetBool("isMove", true);

    }

    void Threaten()
    {
        m_type = Objectmanager.m_instance.m_fruit_Counter.SnatchFruit();
        //行き先決定
        var changer = m_changer.GetComponent<PanelChanger>();

        //自分のいるパネル検出
        Ray ray = new Ray(transform.position, new Vector3(0, 0, 1));
        RaycastHit hit;
        for (int i = 0; i < changer.m_Panel.Length; i++)
        {
            if (changer.m_Panel[i].GetComponent<MeshCollider>().Raycast(ray, out hit, 50.0f))
            {
                //当たったパネルのインデックス保存
                m_index = i;
                break;
            }
        }

        //ランダムで入れ替える場所の抽選
        //自分と同じ場所だった場合は再度抽選
        do
        {
            m_target_index = Random.Range(0, changer.m_Panel.Length);
        } while (m_target_index == m_index || changer.m_Panel[m_target_index].GetComponent<PanelParametor>().GetTimezone == PanelParametor.TIMEZONE.night );

        m_TargetPos = changer.m_Panel[m_target_index].transform.position;
        m_TargetPos.z = .0f;

    }
	
	// Update is called once per frame
	void Update () 
    {
        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        if (m_ownerCementery.m_is_active)
        {
            if( !m_Sporn ) Add_Alpha();
            else Move();
        }
        else
        {
            Sub_Alpha();
            m_Animator.SetBool("isMove", false);
        }

        SkinnedMeshRenderer[] renderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var r in renderer)
        {
            Material m = r.material;
            var col = new Color(m.color.r, m.color.g, m.color.b, m_current_Alpha);
            m.color = col;
        }

	}

    //強制パネル入れ替え
    void ChangePanel()
    {
        var changer = m_changer.GetComponent<PanelChanger>();

        //自分のいるパネル検出
        Ray ray = new Ray(transform.position, new Vector3(0, 0, 1));
        RaycastHit hit;
        for (int i = 0; i < changer.m_Panel.Length; i++)
        {
            if (changer.m_Panel[i].GetComponent<MeshCollider>().Raycast(ray, out hit, 50.0f))
            {
                //当たったパネルのインデックス保存
                m_index = i;
                break;
            }
        }

        //ランダムで入れ替える場所の抽選
        //自分と同じ場所だった場合は再度抽選
        do{
            m_target_index = Random.Range( 0, changer.m_Panel.Length );
        } while (m_target_index == m_index || changer.m_Panel[m_target_index].GetComponent<PanelParametor>().GetTimezone != PanelParametor.TIMEZONE.night);

        //パネルの入れ替え用にUV計算
        PanelParametor.TIMEZONE temp = changer.m_Panel[m_index].GetComponent<PanelParametor>().GetTimezone;
        changer.m_Panel[m_index].GetComponent<Panel>().Set_TargetUV(changer.m_Panel[m_target_index].GetComponent<PanelParametor>().GetTimezone);
        changer.m_Panel[m_target_index].GetComponent<Panel>().Set_TargetUV(temp);

        changer.m_change_Endcount = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        m_Threaten = true;
        Threaten();
    }

    private void DropFruit( FruitInterFace.FRUIT_TYPE type )
    {
        GameObject insert = GameObject.Find("FruitManager").GetComponent<FruitArrangeManager>().m_factory.Create_Object(type, -1);
        insert.transform.position = transform.position;
        insert.transform.rotation = transform.rotation;
        if (insert.GetComponent<FruitInfomation>().fruit_type == Fruit.FRUIT_TYPE.apple)
        {
            Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            insert.transform.rotation = q;
        }
    }

}

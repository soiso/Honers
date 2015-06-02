using UnityEngine;
using System.Collections;

public class Owl : FieldObjectInterface
{
    [SerializeField, HeaderAttribute("夜にしたときにフルーツが出る確率"), Range( 2, 10 )]
    public int m_TargetNum;
    private int m_num;
    private int m_CurrentNum;
    private bool m_isFlg;
    private bool m_once;
    private bool m_isSporn;

    private TimeZone_BoxCollider m_TimeTrigger;

    [SerializeField, HeaderAttribute("フルーツ出現位置")]
    private Transform m_SpornPoint;

    [SerializeField, HeaderAttribute("出現フルーツ")]
    private GameObject[] m_Fruit;

    private FruitArrangeManager m_owner;

    private Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        m_TimeTrigger = GetComponentInChildren<TimeZone_BoxCollider>();
        m_owner = GetComponentInParent<FruitArrangeManager>();
        m_Animator = GetComponent<Animator>();
        m_CurrentNum = 0;
        m_num = m_TargetNum;
        m_once = false;
        m_isSporn = false;
        m_isFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        m_CurrentTimeZone = m_TimeTrigger.m_myColliderTimeZone;
        //GetComponent<ShaderChanger>().Change(m_CurrentTimeZone);

        if (Is_ActiveTimeZone(m_CurrentTimeZone))
        {
           // GetComponent<Renderer>().enabled = true;
            m_Animator.SetBool("isSporn", true);

            if (!m_once)
            {
                if (Random.Range(0.0f, 1.0f) <= 1 / m_num)
                {
                    m_isFlg = true;
                    m_num = m_TargetNum;
                }
                else
                {
                    if (m_isFlg) m_isFlg = false;
                    m_num--;
                    if (m_num <= 0) m_num = m_TargetNum;
                }
            }
            m_once = true;

            if (m_isFlg && !m_isSporn)
            {
                //レアフルーツ生成したいなぁ
                m_Animator.SetBool("isThrow", true);
                SpornFruit(m_Fruit[0].GetComponent<FruitInfomation>().fruit_type);
                m_isSporn = true;
            }
        }
        else
        {
            //GetComponent<Renderer>().enabled = false;
            m_Animator.SetBool("isSporn", false);
            m_Animator.SetBool("isThrow", false);
            m_once = false;
            m_isSporn = false;
        }
    }

    void SpornFruit(FruitInterFace.FRUIT_TYPE type)
    {
        GameObject insert = m_owner.m_factory.Create_Object(type, -1);
        insert.transform.position = m_SpornPoint.position;
        insert.transform.rotation = m_SpornPoint.rotation;
        if (insert.GetComponent<FruitInfomation>().fruit_type == Fruit.FRUIT_TYPE.apple)
        {
            Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            insert.transform.rotation = q;
        }
    }
}

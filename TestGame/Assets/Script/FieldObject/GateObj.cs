using UnityEngine;
using System.Collections;

public class GateObj : FieldObjectInterface 
{
    [SerializeField, HeaderAttribute("花を咲かせる回数")]
    public int m_TargetNum;
    private int m_CurrentNum;
    private bool m_once;
    private bool m_isSporn;

    private TimeZone_BoxCollider m_TimeTrigger;

    [SerializeField, HeaderAttribute("フルーツ出現位置")]
    private Transform m_SpornPoint;

    [SerializeField, HeaderAttribute("出現フルーツ")]
    private GameObject[] m_Fruit;

    private FruitArrangeManager m_owner;

	// Use this for initialization
	void Start () 
    {
        m_TimeTrigger = GetComponentInChildren<TimeZone_BoxCollider>();
        m_owner = GetComponentInParent<FruitArrangeManager>();
        m_CurrentNum = 0;
        m_once = false;
        m_isSporn = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        m_CurrentTimeZone = m_TimeTrigger.m_myColliderTimeZone;
        //GetComponent<ShaderChanger>().Change(m_CurrentTimeZone);

        if( Is_ActiveTimeZone( m_CurrentTimeZone ) )
        {
            //GetComponent<Renderer>().material.color = Color.red;
            MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
            foreach( MeshRenderer m in mesh )
            {
                m.enabled = true;
            }

            if (!m_once)
            {
                m_CurrentNum++;
            }
            m_once = true;

            if( m_CurrentNum == m_TargetNum && !m_isSporn)
            {
                //レアフルーツ生成したいなぁ
                SpornFruit(m_Fruit[0].GetComponent<FruitInfomation>().fruit_type);
                m_isSporn = true;
            }
        }
        else
        {
            //GetComponent<Renderer>().material.color = Color.white;
            MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in mesh)
            {
                if (m.transform.childCount == 0)
                    m.enabled = false;
                else
                    m.enabled = true;
            }
            m_once = false;
        }
	}

    void SpornFruit(FruitInterFace.FRUIT_TYPE type)
    {
        GameObject insert = m_owner.m_factory.Create_Object(type,-1);
        insert.transform.position = m_SpornPoint.position;
        insert.transform.rotation = m_SpornPoint.rotation;
        if (insert.GetComponent<FruitInfomation>().fruit_type == Fruit.FRUIT_TYPE.apple)
        {
            Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            insert.transform.rotation = q;
        }
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TreeParametor))]
public class Tree : MonoBehaviour {

	[SerializeField,HeaderAttribute("フルーツの出現位置")]
    private Transform m_sporn_Transform;

    private FruitArrangeManager m_owner;

    [SerializeField,HeaderAttribute("出現するフルーツの種類"),
    Range(0,(int)Fruit.FRUIT_TYPE.num_fruit)]
    private int m_max_fruit_Type = (int)Fruit.FRUIT_TYPE.num_fruit;

    private float m_defaultUpdateInterval;
    private float m_adjust_Second;

    private float m_last_SpornTime;
    private float m_next_SpornTime;

    [SerializeField, HeaderAttribute("作成するフルーツ（Rendererを入れる）")]
    private GameObject[] m_create_FruitRenderer;


    private bool m_is_Feaver = false;
    private float m_begin_feaver_Time = 0;
    private float m_feaversporn_Interval = 0.5f;


    [SerializeField, HeaderAttribute("街灯の影響を受けるかどうか")]
    private bool m_impact_StreetLight = false;

    private bool m_active = true;

    private RandamRotate m_rotate;

    private GameObject m_current_GrawFruit = null;

    private Vector3 m_default_SpornPosition;


    private TreeParametor m_param;

	void Start () 
    {
        m_param = GetComponent<TreeParametor>();
        m_owner = GetComponentInParent<FruitArrangeManager>();
        m_defaultUpdateInterval = m_owner.m_default_SpornInterval;
        m_adjust_Second = m_owner.m_adjust_Second;
        float adjust = Random.Range(-m_adjust_Second,m_adjust_Second);
        m_rotate = GetComponent<RandamRotate>();
        m_next_SpornTime = m_defaultUpdateInterval + adjust;
        m_default_SpornPosition = m_sporn_Transform.position;
        m_default_SpornPosition.z = 0f;
        Direction_NextGrawFruit();
        
	}
	
    void Direction_NextGrawFruit()
    {
        if (m_current_GrawFruit)
        {
            DestroyObject(m_current_GrawFruit);
            m_current_GrawFruit = null;
        }

        //Vector3 adjust = new Vector3(Random.Range(-0.1f, 0.1f),Random.Range(-0.1f, 0.1f), 0);
        Vector3 adjust = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0);
        //adjust.y += 0.2f;
        m_sporn_Transform.position =m_default_SpornPosition + adjust;
        int next_Fruit = Random.Range(0,m_create_FruitRenderer.Length - 1);
        if(Random.Range(0,100) < m_param.m_donguri_Probability)
            next_Fruit = (int)Fruit.FRUIT_TYPE.donguri;
        m_current_GrawFruit = GameObject.Instantiate(m_create_FruitRenderer[next_Fruit]);
        m_sporn_Transform.transform.localScale = new Vector3(0,0,0);
        m_sporn_Transform.rotation = Quaternion.identity;
    }

    bool Update_Feaver()
    {
        if(Time.time > m_next_SpornTime)
        {
           Sporn_Fruit(Random.Range(0,(int)Fruit.FRUIT_TYPE.num_fruit - 1));
           m_next_SpornTime = Time.time + m_feaversporn_Interval;
        }
        if (Time.time >= m_param.m_feaverTime + m_begin_feaver_Time)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        if (Time.timeScale <= 0.1f)
            return;

        if(!m_active)
        {
            var material = GetComponentInChildren<MeshRenderer>().material;
            material.color = Color.black;
            return;
        }

        if(m_is_Feaver)
        {
            if (Update_Feaver())
            {
                End_Feaver();
                Direction_NextGrawFruit();
            }
        }
        else 
        {
            if (Graw_Fruit())
            {
                Sporn_Fruit((int)m_current_GrawFruit.GetComponent<FruitInfomation>().fruit_type);
                Direction_NextGrawFruit();
            }
        }

    }

    bool Graw_Fruit()
    {
        //Scale決定
        float range = m_next_SpornTime - m_last_SpornTime;
        float progress_Time = Time.time - m_last_SpornTime;

        var material = GetComponentInChildren<MeshRenderer>().material;
        float rate = progress_Time / range;
        m_sporn_Transform.transform.localScale = new Vector3(rate, rate, rate);

        //rotate
        float current_rotete_Angle = m_param.m_max_RotateAngle * Mathf.Sin(Time.time)*m_param.m_rotate_speed;
        Quaternion rot = Quaternion.AngleAxis(current_rotete_Angle, new Vector3(0, 0, 1));

        //if(m_current_GrawFruit.GetComponent<FruitInfomation>().fruit_type == Fruit.FRUIT_TYPE.apple)
        //{
        //    Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
        //    rot *= q;
        //}
        m_sporn_Transform.rotation = rot;
        m_current_GrawFruit.transform.position = m_sporn_Transform.transform.position;
        m_current_GrawFruit.transform.rotation = m_sporn_Transform.transform.rotation;
        m_current_GrawFruit.transform.localScale = m_sporn_Transform.transform.localScale;
        if (rate >= 1.0f)
            return true;
        return false;
    }

    public bool Sporn_Fruit(int type)
    {
        GameObject insert = m_owner.m_factory.Create_Object(type);
        insert.transform.position = m_sporn_Transform.position;
        insert.transform.rotation = m_sporn_Transform.rotation;
        if (insert.GetComponent<FruitInfomation>().fruit_type == Fruit.FRUIT_TYPE.apple)
        {
            Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            insert.transform.rotation = q;
        }

        float next_Interval = Random.Range(-m_adjust_Second, m_adjust_Second);
        m_next_SpornTime = Time.time + m_defaultUpdateInterval + next_Interval;
        return true;
    }

    public bool Begin_Feaver()
    {
        if (m_is_Feaver)
            return false;

        m_is_Feaver = true;

        if(m_current_GrawFruit)
        {
            DestroyObject(m_current_GrawFruit);
            m_current_GrawFruit = null;
        }

        m_begin_feaver_Time = Time.time;
        m_next_SpornTime = Time.time + m_feaversporn_Interval;
        return true;

    }


    private void End_Feaver()
    {
        m_is_Feaver = false;
    }

    //Message
    public void ElectricLight_ON()
    {
        if (!m_impact_StreetLight)
            return;
        m_active = true;
    }

    public void ElectricLight_OFF()
    {
        if (!m_impact_StreetLight)
            return;
        m_active = false;
       
    }
}

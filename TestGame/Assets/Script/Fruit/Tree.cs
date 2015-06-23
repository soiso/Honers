using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TreeParametor))]
public class Tree : MonoBehaviour
{

    [SerializeField, HeaderAttribute("フルーツの出現位置")]
    private Transform m_sporn_Transform;

    private FruitArrangeManager m_owner;

    [SerializeField, HeaderAttribute("出現するフルーツの種類"),
    Range(0, (int)FruitInterFace.FRUIT_TYPE.num_normal_fruit)]
    private int m_max_fruit_Type = (int)FruitInterFace.FRUIT_TYPE.num_normal_fruit;

    private float m_defaultUpdateInterval;
    private float m_adjust_Second;

    private float m_last_SpornTime;

    private float m_next_SpornTime;

    FruitRendererFactory m_renderer_Factory;
    private bool m_is_Feaver = false;
    private float m_begin_feaver_Time = 0;
    private float m_feaversporn_Interval = 0.5f;

    [SerializeField, HeaderAttribute("街灯の影響を受けるかどうか")]
    private bool m_impact_StreetLight = false;
    private bool m_active = true;
    private RandamRotate m_rotate;
    [SerializeField, HeaderAttribute("ログハウス持ちのやつかどうか")]
    private bool m_hasLogHouse = false;

    private GameObject m_current_GrawFruit = null;

    private Vector3 m_default_SpornPosition;
    private TreeParametor m_param;


    private Light m_light;

    private AudioSource m_sporn_sound;

    void Start()
    {

        m_sporn_sound = GetComponent<AudioSource>();
        m_light = GetComponentInChildren<Light>();
        m_renderer_Factory = GetComponent<FruitRendererFactory>();
        m_param = GetComponent<TreeParametor>();
        m_owner = GetComponentInParent<FruitArrangeManager>();
        m_defaultUpdateInterval = m_owner.m_default_SpornInterval;
        m_adjust_Second = m_owner.m_adjust_Second;
        float adjust = Random.Range(-m_adjust_Second, m_adjust_Second);
        m_rotate = GetComponent<RandamRotate>();
        m_next_SpornTime = m_defaultUpdateInterval + adjust;
        m_default_SpornPosition = m_sporn_Transform.position;
        m_default_SpornPosition.z = 0f;
        //Direction_NextGrawFruit();

        if (m_impact_StreetLight) m_active = false;
        else m_active = true;
        
	}


    void Direction_NextGrawFruit()
    {
        if (m_current_GrawFruit)
        {
            DestroyObject(m_current_GrawFruit);
            m_current_GrawFruit = null;
        }

        //Vector3 adjust = new Vector3(Random.Range(-0.1f, 0.1f),Random.Range(-0.1f, 0.1f), 0);
        Vector3 adjust = new Vector3(Random.Range(-0.5f, 0.5f),0, 0);
        //adjust.y += 0.2f;
        m_sporn_Transform.position = m_default_SpornPosition + adjust;
        int next_Fruit = Random.Range(0, (int)FruitInterFace.FRUIT_TYPE.num_normal_fruit);

        if (m_param.m_book_fruit != FruitInterFace.FRUIT_TYPE.error)
            next_Fruit = (int)m_param.m_book_fruit;
        m_current_GrawFruit = m_renderer_Factory.Create_Object(next_Fruit);
        //if (m_current_GrawFruit.GetComponent<FruitInfomation>().fruit_type == FruitInterFace.FRUIT_TYPE.apple)
        //    Debug.Log("apple");

        //とりあえずのエラー処理
        if (!m_current_GrawFruit)
        {
            m_current_GrawFruit = m_renderer_Factory.Create_Object(Random.Range(0, (int)FruitInterFace.FRUIT_TYPE.num_normal_fruit));
        }
        m_param.m_book_fruit = FruitInterFace.FRUIT_TYPE.error;
        m_sporn_Transform.transform.localScale = new Vector3(0, 0, 0);
        m_sporn_Transform.rotation = Quaternion.identity;

        m_current_GrawFruit.transform.position = m_sporn_Transform.transform.position;
        m_current_GrawFruit.transform.rotation = m_sporn_Transform.transform.rotation;
        m_current_GrawFruit.transform.localScale = m_sporn_Transform.transform.localScale;
    }

    bool Update_Feaver()
    {
        if (Time.time > m_next_SpornTime)
        {

            int index = Random.Range(0, (int)FruitInterFace.FRUIT_TYPE.num_normal_fruit - 1);
            Sporn_Fruit((FruitInterFace.FRUIT_TYPE)index,true);
            // m_owner.m_factory.Create_Object((FruitInterFace.FRUIT_TYPE)index);
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

        if (!m_current_GrawFruit)
            Direction_NextGrawFruit();

        if (!m_active)
        {
            //var material = GetComponentInChildren<MeshRenderer>().material;
            //material.color = Color.black;
            return;
        }

        if (m_is_Feaver)
        {
            m_light.enabled = true;
            if (Update_Feaver())
            {
                Objectmanager.m_instance.m_fruit_Counter.End_FeaverTime();
                End_Feaver();
                Direction_NextGrawFruit();
            }
        }
        else
        {
            m_light.enabled = false;
            if (Graw_Fruit())
            {
                Sporn_Fruit(m_current_GrawFruit.GetComponent<FruitInfomation>().fruit_type,false);
                float next_Interval = Random.Range(-m_adjust_Second, m_adjust_Second);
                m_next_SpornTime = Time.time + m_defaultUpdateInterval + next_Interval;
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
        float current_rotete_Angle = m_param.m_max_RotateAngle * Mathf.Sin(Time.time) * m_param.m_rotate_speed;
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

    public bool Sporn_Fruit(FruitInterFace.FRUIT_TYPE type,bool is_Feaver)
    {
        GameObject insert = m_owner.m_factory.Create_Object(type, m_param.m_event_Affiliation);
        insert.transform.position = m_sporn_Transform.position;
        insert.transform.rotation = m_sporn_Transform.rotation;
        //if (insert.GetComponent<FruitInfomation>().fruit_type == FruitInterFace.FRUIT_TYPE.apple)
        //{
        //    Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
        //    insert.transform.rotation = q;
        //}
       var script = insert.GetComponent<FruitInterFace>();
       if (is_Feaver)
           script.m_IsFeaverSporn = true;
       else
       {
           var fruit_type = insert.GetComponent<FruitInfomation>().fruit_type;

           if (fruit_type == FruitInterFace.FRUIT_TYPE.donguri)
               m_sporn_sound.clip = m_owner.Get_SpornSound[1];
           else
               m_sporn_sound.clip = m_owner.Get_SpornSound[0];


           m_sporn_sound.Play();
           script.m_IsFeaverSporn = false;
       }
        return true;
    }

    public bool Begin_Feaver()
    {
        if (m_is_Feaver)
            return false;

        m_is_Feaver = true;

        if (m_current_GrawFruit)
        {
            DestroyObject(m_current_GrawFruit);
            m_current_GrawFruit = null;
        }
        m_param.m_book_fruit = FruitInterFace.FRUIT_TYPE.error;
        m_begin_feaver_Time = Time.time;
        m_next_SpornTime = Time.time + m_feaversporn_Interval;
        return true;

    }


    private void End_Feaver()
    {
        m_is_Feaver = false;
    }

    public void Set_BookFruit(FruitInterFace.FRUIT_TYPE type)
    {
        m_param.m_book_fruit = type;
    }

    //Message
    public void ElectricLight_ON()
    {
        if (!m_impact_StreetLight)
            return;
        m_active = true;
        if (m_hasLogHouse)
        {
            MeshRenderer[] renderer = transform.GetChild(2).GetComponentsInChildren<MeshRenderer>();
            foreach( MeshRenderer m in renderer )
            {
                m.enabled = false;
            }
            renderer = transform.GetChild(3).GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in renderer)
            {
                m.enabled = true;
            }
        }
        else
        {
            MeshRenderer[] renderer = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in renderer)
            {
                m.material.mainTexture = (Texture)Resources.Load("woods2_light");
            }
        }
    }

    public void ElectricLight_OFF()
    {
        if (!m_impact_StreetLight)
            return;
        m_active = false;
        if (m_hasLogHouse)
        {
            MeshRenderer[] renderer = transform.GetChild(2).GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in renderer)
            {
                m.enabled = true;
            }
            renderer = transform.GetChild(3).GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in renderer)
            {
                m.enabled = false;
            }
        }
        else
        {
            MeshRenderer[] renderer = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in renderer)
            {
                m.material.mainTexture = (Texture)Resources.Load("woods2_shadow");
            }
        }

    }
}
using UnityEngine;
using System.Collections;

public class TestTree : MonoBehaviour {


    private PanelParametor.TIMEZONE[] m_fruit_Sporn_Array;
    private int m_current_Index = 0;
    private PanelParametor.TIMEZONE m_next_Timezone;
    TimeZone_BoxCollider m_collider;
    private FruitFactory m_factory;
    private TimeZoneRenderer m_next_Renderer;
    [SerializeField, HeaderAttribute("フルーツの出現位置")]
    private Transform m_sporn_Transform;


    private float work_interval = 1.0f;
    private float work_nextSwitchTime = 0;

    private void Initialize_SpornArray()
    {
        //m_fruit_Sporn_Array = new PanelParametor.TIMEZONE[Random.Range(2,4)];
        m_fruit_Sporn_Array = new PanelParametor.TIMEZONE[4];
        m_fruit_Sporn_Array[m_fruit_Sporn_Array.Length -1] = PanelParametor.TIMEZONE.error;

        PanelParametor.TIMEZONE before;
        for(int i = 0 ; i < m_fruit_Sporn_Array.Length ; i++)
        {

            //int BEFORE = i;
            //BEFORE = Mathf.Clamp(-BEFORE,0,m_fruit_Sporn_Array.Length);
            if (i == 0)
                before = m_fruit_Sporn_Array[m_fruit_Sporn_Array.Length - 1];
            else
               before = m_fruit_Sporn_Array[i-1];
            PanelParametor.TIMEZONE insert;
            do
            {
                insert = (PanelParametor.TIMEZONE)Random.Range(0, 3);

            } while (before == insert);
            
            m_fruit_Sporn_Array[i] = insert;
        }
        m_next_Timezone = m_fruit_Sporn_Array[0];
        m_next_Renderer.SetRenderTime((int)m_next_Timezone);
    }

	void Start () 
    {
        m_collider = GetComponentInChildren<TimeZone_BoxCollider>();
        m_factory = GetComponent<FruitFactory>();
        m_next_Renderer = GetComponentInChildren<TimeZoneRenderer>();
        Initialize_SpornArray();
	}
	
    void Change_NextTimezone()
    {
        m_current_Index++;
        if(m_current_Index >= m_fruit_Sporn_Array.Length)
        {
            m_current_Index = 0;
            Sporn_Fruit(Random.Range(0,4));
            m_next_Timezone = m_fruit_Sporn_Array[m_current_Index];
            return;
        }
        m_next_Timezone = m_fruit_Sporn_Array[m_current_Index];
        m_next_Renderer.SetRenderTime((int)m_next_Timezone);
    }

    public bool Sporn_Fruit(int type)
    {
        GameObject insert = m_factory.Create_Object(type);
       insert.transform.position = m_sporn_Transform.position;
       insert.transform.rotation = m_sporn_Transform.rotation;
        if (insert.GetComponent<FruitInfomation>().fruit_type == Fruit.FRUIT_TYPE.apple)
        {
            Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            insert.transform.rotation = q;
        }
        return true;
    }

	// Update is called once per frame
	void Update () 
    {
        if (m_collider.m_myColliderTimeZone == m_next_Timezone)
        {
           Change_NextTimezone();
        }


        if(m_current_Index == m_fruit_Sporn_Array.Length-2)
        {
            if (work_nextSwitchTime < Time.time)
            {
                m_next_Renderer.enabled = !m_next_Renderer.enabled;
                work_nextSwitchTime = Time.time + work_interval;
            }
            
        }
       
	}
}

using UnityEngine;
using System.Collections;

public class FruitEventManager : MonoBehaviour {

    class EvenLocker
    {
        public bool m_is_Enable = true;
        public float m_lastEventTime = 0f;
    }

    [SerializeField,HeaderAttribute("イベントが始まる始点位置")]
    private Transform[] m_sporn_Point;
    private EvenLocker[] m_event_Checker;
 
    [SerializeField, HeaderAttribute("再び同じ場所でフルーツを拾ったときにイベントを起こす間隔")]
    private float m_sameEventInterval;
    [SerializeField,HeaderAttribute("イベントオブジェクト")]
    GameObject[] m_event_ObjectArray;


    public EventPointHolder m_evepointHolder { get; private set; }

    public void Calculate_TreePoint(TreeParametor parametor)
    {
        float min_dist = 1000000000.0f;
        int index = -1;
        for (int i = 0; i < m_sporn_Point.Length; i++ )
        {
            float dist = Vector3.Distance(m_sporn_Point[i].position, parametor.transform.position);
            if (dist < min_dist)
            {
                min_dist = dist;
                index = i;
            }
        }
        parametor.Set_EventAffiliation(index);
    }

    void Awake()
    {
        m_evepointHolder = GetComponent<EventPointHolder>();
        if(m_sporn_Point.Length != 0)
        {
            m_event_Checker = new EvenLocker[m_sporn_Point.Length];
            for(int i = 0 ; i < m_event_Checker.Length ; i++)
            {
                m_event_Checker[i] = new EvenLocker();
            }
        }
   

    }

	void Start () 
    {
      
	}

    void Update_EventLock()
    {
        foreach(var it in m_event_Checker)
        {
            if (it.m_is_Enable)
                continue;

            if(Time.time >= it.m_lastEventTime + m_sameEventInterval)
            {
                it.m_is_Enable = true;
            }
        }
    }

	void Update () 
    {
        Update_EventLock();
	}

    public bool Event_Check(int point_No)
    {
        if (point_No < 0 || point_No >= m_sporn_Point.Length)
            return false;

        if (!m_event_Checker[point_No].m_is_Enable)
            return false;

        Create_EventObject(point_No);
        return true;
    }

    private void Create_EventObject(int point_No)
    {
        //現在のPoint以外の場所でイベントを起こす
        int index = point_No;
        while(index == point_No)
        {
            index = Random.Range(0, m_sporn_Point.Length);
        }
        GameObject event_Object = GameObject.Instantiate(m_event_ObjectArray[Random.Range(0,m_event_ObjectArray.Length)]);
        event_Object.transform.position = m_sporn_Point[index].transform.position;
        m_event_Checker[point_No].m_is_Enable = false;
        m_event_Checker[point_No].m_lastEventTime = Time.time;
        //Objectmanager.m_instance.m_fruit_Counter.m_fruitmanager.m_event_Manager.m_evepointHolder.GetUsagiPoint();
    }
}

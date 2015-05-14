using UnityEngine;
using System.Collections;

public class Hariyama : MonoBehaviour {

    [SerializeField]
    private Switch[] m_switch_array;
    private int m_array_size;
    private Vector3 m_default_position;
    [SerializeField]
    private float m_max_Down;   //とりあえず
    private float m_current_Down;   //とりあえず

    private float m_offset;

    WayPoint_MockObserver m_observer;
    
  //  DamageTrigger m_damage_trigger;

	// Use this for initialization
	void Start () 
    {
        m_array_size = m_switch_array.Length;
        m_offset = m_max_Down / (float)m_array_size;
        m_default_position = this.transform.position;
        m_observer = GetComponent<WayPoint_MockObserver>();
      //  m_damage_trigger = this.transform.GetChild(0).GetComponent<DamageTrigger>();
	}
	
    int Active_SwitchCheck()
    {
        int ret = 0;
        for(int i = 0 ; i < m_array_size ; i++)
        {
            if (m_switch_array[i].m_active)
                ret++;
        }
        return ret;
    }

	// Update is called once per frame
	void Update () 
    {

        m_observer.CutOffRoute();
        int active_switch = Active_SwitchCheck();
        if(active_switch == 0)
        {
          //  m_damage_trigger.OnCollisionBegin();
            m_observer.CutOffRoute();
        }
        else
        {
            //m_damage_trigger.onCollisionEnd();
            m_observer.OpenRoute();
        }

        float dist = m_offset * active_switch;
         
        Vector3 add_pos = m_default_position;
        add_pos.y += -dist;
        if (!this.transform.root.GetComponent<PicturePaper>().m_move)
         this.transform.position = this.transform.position * 0.7f + add_pos * 0.3f;

	}
}

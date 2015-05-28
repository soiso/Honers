using UnityEngine;
using System.Collections;

public class ElectricLight : FieldObjectInterface
{

    Light m_pointLight;
    LayerContainer_Collider m_collider;
    TimeZone_BoxCollider m_timezoneCollider;

    public AudioClip clip;
    private AudioSource audio;

    [SerializeField, HeaderAttribute("動力源(あれば)")]
    public GameObject m_Owner = null;

	void Start () 
    {
        m_pointLight = this.transform.GetChild(0).GetComponent<Light>();
        m_collider = this.transform.GetChild(1).GetComponent<LayerContainer_Collider>();
        m_timezoneCollider = this.transform.GetChild(2).GetComponent<TimeZone_BoxCollider>();
        audio = GetComponent<AudioSource>();
        audio.clip = clip;
        if (m_Owner != null) m_timezoneCollider.enabled = false;
	}

    private void NullcheckContainer()
    {
        foreach(var it in m_collider.m_exit_List)
        {
            if (!it)
                m_collider.m_exit_List.Remove(it);
        }

        foreach (var it in m_collider.m_active_list)
        {
            if (!it)
                m_collider.m_exit_List.Remove(it);
        }
    }

	
    void    CheckExitList()
    {
        if (m_collider.m_exit_List.Count == 0)
            return;

        foreach(GameObject it in m_collider.m_exit_List)
        {
            //そのうち修正する
            if (!it)
                continue;
            it.SetActive(true);
        }
    }

    void    Calculate_CurrentTimezone()
    {
        m_CurrentTimeZone = m_timezoneCollider.m_myColliderTimeZone;
    }

    void    Active_InLightObject()
    {
        if(!m_pointLight.enabled)
            audio.Play();

        m_pointLight.enabled = true;
        if (m_collider.m_active_list.Count == 0)
            return;
        
        foreach (GameObject it in m_collider.m_active_list)
        {
            //it.SetActive(true);
            it.SendMessage("ElectricLight_ON",SendMessageOptions.DontRequireReceiver);
        }
    }

    void    Hidden_InlightObject()
    {
        m_pointLight.enabled = false;
        if (m_collider.m_active_list.Count == 0)
            return;
        foreach (GameObject it in m_collider.m_active_list)
        {
            //it.SetActive(false);
            //結構危険なので気を付ける
            it.SendMessage("ElectricLight_OFF",
                SendMessageOptions.DontRequireReceiver);
           
        }
    }

	void Update () 
    {
        if( m_Owner != null )
        {
            var obj = m_Owner.GetComponent<Hamster>();
            this.m_CurrentTimeZone = obj.m_time_Zone;
        }
        else
        {
            Calculate_CurrentTimezone();
        }
       
        //NullcheckContainer();
	    if(Is_ActiveTimeZone(this.m_CurrentTimeZone))
        {
            Active_InLightObject();
        }
        else
        {
            Hidden_InlightObject();
        }
        CheckExitList();

        
	}
}

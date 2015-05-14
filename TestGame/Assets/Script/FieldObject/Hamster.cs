using UnityEngine;
using System.Collections;

public class Hamster : MonoBehaviour {


    private TimeZone_BoxCollider m_collider;

    public PanelParametor.TIMEZONE m_time_Zone { get; private set; }
	
	void Start () 
    {
        m_collider = GetComponentInChildren<TimeZone_BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_time_Zone = m_collider.m_myColliderTimeZone;

        Material m = GetComponentInChildren<MeshRenderer>().material;
        switch(m_time_Zone)
        {
            case PanelParametor.TIMEZONE.morning :
                m.color = Color.blue;

                break;

            case PanelParametor.TIMEZONE.noon :
                m.color = Color.red;
                break;

            case PanelParametor.TIMEZONE.night :
                m.color = Color.black;
                break;
        }
	}
}

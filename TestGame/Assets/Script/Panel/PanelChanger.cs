using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelChanger : MonoBehaviour {

    public enum CHANGEMODE
    {
        move,
        uv,
    }

    [SerializeField]
    private CHANGEMODE m_changeMode;
    public int m_current_Selectpanel { get; private set; }

    private Panel[] m_change_Panel;
    public bool m_is_changeNow { get; private set; }
    public int m_change_Endcount { get; private set; }

    [SerializeField, Range(1.0f, 5.0f)]
    public float m_ChangeSecond;

    public AudioClip clip;
    private AudioSource sound;

    [SerializeField, HeaderAttribute("登録パネル")]
    public GameObject[] m_Panel;

    private TouchMesh m_Touch;

	// Use this for initialization
	void Start ()
    {
        m_current_Selectpanel = 0;
        m_change_Panel = new Panel[2];
        m_is_changeNow = false;
        m_change_Endcount = 0;

        sound = gameObject.GetComponent<AudioSource>();
        sound.clip = clip;

        int index = transform.childCount;
        m_Touch = transform.GetChild( index-1 ).GetComponent<TouchMesh>();
	}

    public void    SubCount_SelectPanel()
    {
        Mathf.Clamp(--m_current_Selectpanel,0,2);
    }

    public void AddCount_SelectPanel()
    {
        Mathf.Clamp(++m_current_Selectpanel, 0, 2);
    }
   
    bool    ChangePanel()
    {
       
        return true;
    }

    Panel.ROTATE_MODE Calculate_RotateMode( Vector3 v1, Vector3 v2)
    {
        Panel.ROTATE_MODE ret = Panel.ROTATE_MODE._Y;
        Vector3 base_Vec = new Vector3(0, 1, 0);
        Vector3 v1_v2 = v2 - v1;

        float dot = Vector3.Dot(base_Vec, v1_v2.normalized);
        if (dot > 0)
            ret = Panel.ROTATE_MODE._X;
        return ret;
    }

    private void    ChangeBegin(ref Panel p1,ref Panel p2)
    {

    }
    
    private void  Check_Panel()
    {
        //とりあえず
        int counter = 0;
        for (int i = 0; i < this.transform.childCount-1; i++)
        {
            Panel child = this.transform.GetChild(i).GetComponent<Panel>();
            if (child.GetComponent<PanelParametor>().m_touchMesh.m_is_select)
            {
                m_change_Panel[counter] = child;
                counter++;
            }
        }
        if (counter == 2)
        {
            Panel.ROTATE_MODE rotate_Mode = Calculate_RotateMode( m_change_Panel[0].transform.position,
                m_change_Panel[1].transform.position);
            m_is_changeNow = true;
            //m_change_Panel[0].Begin_Move(m_change_Panel[1].transform.position,rotate_Mode);
            //m_change_Panel[1].Begin_Move(m_change_Panel[0].transform.position,rotate_Mode);
            PanelParametor.TIMEZONE temp = m_change_Panel[0].GetComponent<PanelParametor>().GetTimezone;
            m_change_Panel[0].Set_TargetUV(m_change_Panel[1].GetComponent<PanelParametor>().GetTimezone);
            m_change_Panel[1].Set_TargetUV(temp);
            //m_change_Panel[0].GetComponent<BoxCollider>().enabled = false;
           // m_change_Panel[1].GetComponent<BoxCollider>().enabled = false;
            m_change_Endcount = 0;
            /*sound*/
            sound.Play();
           // Time.timeScale = 0;
        }
        if (counter > 2)
        {
            Debug.Log("ERROR counter is Over Capacity");
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (!m_is_changeNow)
        {
            m_Touch.IsTouch();
            Check_Panel();
        }

	}

    public void   Change_End()
    {
        m_change_Endcount++;
        if(m_change_Endcount >=2)
        {
            m_change_Panel[0].GetComponent<BoxCollider>().enabled = true;
            m_change_Panel[1].GetComponent<BoxCollider>().enabled = true;
            m_is_changeNow = false;
            m_change_Endcount = 0;
            m_current_Selectpanel = 0;
            sound.Stop();
          //  sound.clip = null;
            //Time.timeScale = 1.0f;
        }
    }
}

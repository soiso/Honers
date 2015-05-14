using UnityEngine;
using System.Collections;

public class See_Saw : MonoBehaviour {

    [SerializeField]
    private float m_max_RotateAngle;    //最大Z回転の角度

    [SerializeField,Range(0,50)]
    private float m_default_Rotate_Z;

    private Weight_Collider m_left_Trigger;
    private Weight_Collider m_right_Trigger;

    private int m_right_Weight = 0;
    private int m_left_Weight = 0;

    private float speed = 0.01f;
    private float m_current_Angle =0;

    private Boolean_BoxCollider m_right_UnderTrigger;
    private Boolean_BoxCollider m_left_UnderTrigger;

    public enum AXIS
    {
        LEFT = -1,
        RIGHT = 1,
    }

    [SerializeField]
    AXIS default_AXIS;

    [SerializeField]
    private bool CompliancePlayer = true;
    
    [SerializeField]
    GameObject m_player;

	// Use this for initialization
	void Start () 
    {
        if(!m_player)
        {
            m_player = GameObject.Find("Player");
        }
        m_left_Trigger = transform.GetChild(0).GetComponent<Weight_Collider>();
        m_right_Trigger = transform.GetChild(1).GetComponent<Weight_Collider>();
        m_left_UnderTrigger = transform.GetChild(2).GetComponent<Boolean_BoxCollider>();
        m_right_UnderTrigger = transform.GetChild(3).GetComponent<Boolean_BoxCollider>();
	}
	
    //そのうち全部直す
    void    Right_Axis()
    {

        m_current_Angle = m_current_Angle * (1.0f - speed) + -m_max_RotateAngle * speed;
        m_current_Angle = Mathf.Clamp(m_current_Angle, -m_max_RotateAngle, m_max_RotateAngle);
        Vector3 axis = new Vector3(0, 0, 1);
        Quaternion angle = Quaternion.AngleAxis(m_current_Angle, axis);
        this.transform.rotation = angle;
        m_left_UnderTrigger.GetComponent<BoxCollider>().enabled = true;
        m_right_UnderTrigger.GetComponent<BoxCollider>().enabled = false;
    }

    void    Left_Axis()
    {
        m_current_Angle = m_current_Angle * (1.0f - speed) + m_max_RotateAngle * speed;
        m_current_Angle = Mathf.Clamp(m_current_Angle, -m_max_RotateAngle, m_max_RotateAngle);
        Vector3 axis = new Vector3(0, 0, 1);
        Quaternion angle = Quaternion.AngleAxis(m_current_Angle, axis);
        this.transform.rotation = angle;
        m_left_UnderTrigger.GetComponent<BoxCollider>().enabled = true;
        m_right_UnderTrigger.GetComponent<BoxCollider>().enabled = false;
    }

    void    Horizontal()
    {
        m_current_Angle = m_current_Angle * (1.0f - speed) + 0 * speed;
        m_current_Angle = Mathf.Clamp(m_current_Angle, -m_max_RotateAngle, m_max_RotateAngle);
        Vector3 axis = new Vector3(0,0,1);
        Quaternion angle = Quaternion.AngleAxis(m_current_Angle, axis);
        this.transform.rotation = angle;
        m_left_UnderTrigger.GetComponent<BoxCollider>().enabled = true;
        m_right_UnderTrigger.GetComponent<BoxCollider>().enabled = true;
    }


    void    Calculate_DefaultRotate()
    {
        Vector3 player_pos = m_player.transform.position;
        default_AXIS = (player_pos.x - this.transform.position.x <= 0) ? AXIS.RIGHT : AXIS.LEFT; 
    }

    void    Default_Rotate()
    {
        m_current_Angle = (m_current_Angle * (1.0f - speed)) + ((m_default_Rotate_Z * (int)default_AXIS) * speed);
        m_current_Angle = Mathf.Clamp(m_current_Angle, -m_max_RotateAngle, m_max_RotateAngle);
        Vector3 axis = new Vector3(0, 0, 1);
        Quaternion angle = Quaternion.AngleAxis(m_current_Angle, axis);
        this.transform.rotation = angle;
        if(m_current_Angle < 0)
        {
            m_left_UnderTrigger.GetComponent<BoxCollider>().enabled = true;
            m_right_UnderTrigger.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            m_left_UnderTrigger.GetComponent<BoxCollider>().enabled = false;
            m_right_UnderTrigger.GetComponent<BoxCollider>().enabled = true;
        }
    }

	// Update is called once per frame
    void Update()
    {
        if(CompliancePlayer)
             Calculate_DefaultRotate();

        if (m_left_Trigger.m_currentMaxWeight == m_right_Trigger.m_currentMaxWeight)
        {
            if(m_left_Trigger.m_currentMaxWeight ==0 &&
                m_right_Trigger.m_currentMaxWeight ==0)
            {
                Default_Rotate();
            }
            else 
                Horizontal();
        }
        else if (m_left_Trigger.m_currentMaxWeight > m_right_Trigger.m_currentMaxWeight)
        {
            Left_Axis();
        }
        else if (m_left_Trigger.m_currentMaxWeight < m_right_Trigger.m_currentMaxWeight)
        {
            Right_Axis();
        }

    }

}

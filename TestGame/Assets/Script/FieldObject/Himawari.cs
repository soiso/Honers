using UnityEngine;
using System.Collections;

public class Himawari : MonoBehaviour {

    [SerializeField]
    private GameObject m_kuki;
    [SerializeField]
    private GameObject m_hana;
    [SerializeField]
    private GameObject m_rotate_root;

    [SerializeField]
    private GameObject[] m_move_PointArray;
    [SerializeField]
    GameObject m_default_Point;
    [SerializeField]
    private GameObject m_ashiba;

    [SerializeField, HeaderAttribute("回転速度"),Range(0f,0.9f)]
    private float m_rotate_Speed = 0.3f;
    [SerializeField, HeaderAttribute("伸縮速度")]
    private float m_move_Speed = 0.5f;
    private GameObject m_current_Movetarget;

    private bool m_isStay = true;

    private float m_current_Yscale = 1.0f;

    private Vector3 m_hana_DefaultScale;
    //MoveTargetに変更があればtrue
    private bool Caluculate_MoveTarget()
    {
        GameObject candidate = null;

        foreach(var it in m_move_PointArray)
        {
            var time_collider = it.GetComponent<TimeZone_BoxCollider>();
            if (time_collider.m_myColliderTimeZone == PanelParametor.TIMEZONE.noon)
                candidate = it;
        }

        if(m_current_Movetarget != candidate)
        {
            m_current_Movetarget = candidate;
            return true;
        }
        return false;
        
    }

    void Awake()
    {
        m_current_Movetarget = m_default_Point;
        
    }

    Vector3 default_Scale;
	// Use this for initialization
	void Start () {
        default_Scale = m_hana.transform.lossyScale;
       m_hana_DefaultScale = m_hana.transform.lossyScale;
	}
	

    void Adjust_Ashiba(Vector3 axis, float angle)
    {
        Quaternion rot = Quaternion.identity;
        Quaternion q = Quaternion.Slerp(m_ashiba.transform.transform.rotation, rot, 0.1f);
        m_ashiba.transform.rotation = q;
    }

    bool Rotate()
    {
        Vector3 target_vec = m_current_Movetarget.transform.position - m_rotate_root.transform.position;
        Vector3 current_vec = m_hana.transform.position - m_rotate_root.transform.position;
        float cos = Vector3.Dot(current_vec.normalized, target_vec.normalized);
        if (cos > 0.999f)
            return true;

        Vector3 axis = Vector3.Cross(current_vec, target_vec);
        axis.Normalize();
        //Vector3 axis = new Vector3(0, 0, 1);
        cos = Mathf.Acos(cos);
        cos = cos / Mathf.PI * 180.0f;
        Quaternion rot = m_rotate_root.transform.rotation * Quaternion.AngleAxis(cos, axis.normalized);
        m_rotate_root.transform.rotation = Quaternion.Slerp(m_rotate_root.transform.transform.rotation, rot, m_rotate_Speed);

        Adjust_Ashiba(axis, cos);
        return false;
    }

    void Scaling()
    {
        Vector3 vec = m_current_Movetarget.transform.position - m_hana.transform.position;
        if (vec.magnitude < 0.5f)
            return;

        Vector3 axis = Vector3.Cross(Vector3.up, vec.normalized);
        float cos = Vector3.Dot(Vector3.up, vec.normalized);
    
        if(axis.y < 0)
        {
            if(cos > 0)
            {
                m_current_Yscale -= m_move_Speed;
                
            }
            else
            {
                m_current_Yscale += m_move_Speed;
              
            }
        }
        else
        {
            if (cos > 0)
            {
                m_current_Yscale += m_move_Speed;
               
            }
            else
            {
                m_current_Yscale -= m_move_Speed; 
            }
        }

        Vector3 scale = Vector3.one;
        scale.y = m_current_Yscale;
        m_rotate_root.transform.localScale = scale;
        Vector3 hana_scale = Vector3.one;
       // float localscaley = m_rotate_root.transform.localScale.y * m_kuki.transform.localScale.y;

        //hana_scale.x = 1.0f  /  (m_rotate_root.transform.localScale.x * m_kuki.transform.localScale.x) * m_hana_DefaultScale.x;
        ////hana_scale.y = 1.0f / (m_rotate_root.transform.localScale.y * m_kuki.transform.localScale.y) * m_hana_DefaultScale.y;
        //hana_scale.y = (m_rotate_root.transform.localScale.y * m_kuki.transform.localScale.y) * m_hana_DefaultScale.y;
        //hana_scale.y = m_hana_DefaultScale.y;
        //hana_scale.z = 1.0f / (m_rotate_root.transform.localScale.z * m_kuki.transform.localScale.z) * m_hana_DefaultScale.z; 
        //hana_scale.x = m_hana.transform.localScale.x / m_hana.transform.lossyScale.x * default_Scale.x;
        //hana_scale.y = m_hana.transform.localScale.y / m_hana.transform.lossyScale.y * default_Scale.y;
        //hana_scale.z = m_hana.transform.localScale.z / m_hana.transform.lossyScale.z * default_Scale.z;
        //m_hana.transform.localScale = hana_scale;
       
    }


	// Update is called once per frame
	void Update () {

        if (Time.timeScale < 0.1f)
            return;
        Caluculate_MoveTarget();
        if(Rotate())
        {
            Scaling();
        }
        Vector3 hana_scale;
        hana_scale.x = m_hana.transform.localScale.x / m_hana.transform.lossyScale.x * m_hana_DefaultScale.x;
        hana_scale.y = m_hana.transform.localScale.y / m_hana.transform.lossyScale.y * m_hana_DefaultScale.y;
        hana_scale.z = m_hana.transform.localScale.z / m_hana.transform.lossyScale.z * m_hana_DefaultScale.z;
        m_hana.transform.localScale = hana_scale;
        Quaternion rot = Quaternion.identity;
        //m_hana.transform.rotation = Quaternion.Slerp(m_hana.transform.rotation, rot, 0.1f);
     // m_rotate_root.transform.localScale = scale;
	
	}
}

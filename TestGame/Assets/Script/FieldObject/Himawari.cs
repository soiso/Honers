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
        //m_hana = this.transform.FindChild("ModelData").gameObject;
        //m_kuki = this.transform.FindChild("Kuki").gameObject;
        //m_rotate_root = this.transform.FindChild("RotateRoot").gameObject;
        m_hana_DefaultScale = m_hana.transform.localScale;
        Caluculate_MoveTarget();
    }

	// Use this for initialization
	void Start () {
	
	}
	

    void Adjust_Ashiba(Vector3 axis, float angle)
    {
        axis.z *= -1.0f;
        Quaternion rot = m_ashiba.transform.rotation * Quaternion.AngleAxis(angle, axis.normalized);
        m_ashiba.transform.rotation = Quaternion.Slerp(m_ashiba.transform.transform.rotation, rot, 0.05f);
        
    }

    bool Rotate()
    {
        Vector3 target_vec = m_current_Movetarget.transform.position - m_rotate_root.transform.position;
        Vector3 current_vec = m_hana.transform.position - m_rotate_root.transform.position;
        float cos = Vector3.Dot(current_vec.normalized, target_vec.normalized);
        if (cos > 0.999f)
            return true;

        Vector3 axis = Vector3.Cross(current_vec, target_vec); 

        //Vector3 axis = new Vector3(0, 0, 1);
        cos = Mathf.Acos(cos);
        cos = cos / Mathf.PI * 180.0f;
        Quaternion rot = m_rotate_root.transform.rotation * Quaternion.AngleAxis(cos, axis.normalized);
        m_rotate_root.transform.rotation = Quaternion.Slerp(m_rotate_root.transform.transform.rotation, rot, 0.05f);

        Adjust_Ashiba(axis, cos);
        return false;
    }

    void Scaling()
    {
        Vector3 vec = m_current_Movetarget.transform.position - m_hana.transform.position;
        if (vec.magnitude < 1.15f)
            return;

        Vector3 axis = Vector3.Cross(Vector3.up, vec.normalized);
        float cos = Vector3.Dot(Vector3.up, vec.normalized);
    
        if(axis.y < 0)
        {
            if(cos > 0)
            {
                m_current_Yscale -= 0.01f;
                
            }
            else
            {
                m_current_Yscale += 0.01f;
              
            }
        }
        else
        {
            if (cos > 0)
            {
                m_current_Yscale += 0.01f;
               
            }
            else
            {
                m_current_Yscale -= 0.01f; 
            }
        }

        Vector3 scale = Vector3.one;
        scale.y = m_current_Yscale;
        m_rotate_root.transform.localScale = scale;
        Vector3 hana_scale = Vector3.one;
        hana_scale.x = 1.0f  /  (m_rotate_root.transform.localScale.x * m_kuki.transform.localScale.x) * m_hana_DefaultScale.x;
        hana_scale.y = 1.0f / (m_rotate_root.transform.localScale.y * m_kuki.transform.localScale.y) * m_hana_DefaultScale.y;
        hana_scale.z = 1.0f / (m_rotate_root.transform.localScale.z * m_kuki.transform.localScale.z) * m_hana_DefaultScale.z; 

        m_hana.transform.localScale = hana_scale;
       
    }


	// Update is called once per frame
	void Update () {

        Caluculate_MoveTarget();
        if(Rotate())
        {
            Scaling();
        }

     // m_rotate_root.transform.localScale = scale;
	
	}
}

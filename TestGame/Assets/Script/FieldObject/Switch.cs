using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    private BoxCollider m_collider;
    public bool m_active { get; private set; }
    [SerializeField]
    private float m_max_cavein; //めり込み量
    [SerializeField]
    private float m_cavein_speed;
    private float m_current_Cavein = 0f;

    private bool m_onObject = false;
	// Use this for initialization

    private Vector3 m_default_position;

	void Start () 
    {
        m_active = false;
        m_onObject = false;
        m_collider = GetComponent<BoxCollider>();
        m_collider.isTrigger = true;
        m_default_position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(m_onObject)
        {
            m_current_Cavein += m_cavein_speed;
            if (m_current_Cavein >= m_max_cavein)
                m_active = true;
        }
        else
        {
                m_current_Cavein -= m_cavein_speed;
                if (m_current_Cavein <= 0)
                    m_active = false;
        }
        m_current_Cavein = Mathf.Clamp(m_current_Cavein, 0, m_max_cavein);


        Vector3 add_pos = new Vector3(0, -m_current_Cavein, 0);
        if (!this.transform.root.GetComponent<PicturePaper>().m_move)
            this.transform.position = m_default_position + add_pos;

        if(m_active)
        {
            var renderer = GetComponent<MeshRenderer>();
            renderer.material.color = Color.yellow;
        }
        else
        {
            var renderer = GetComponent<MeshRenderer>();
            renderer.material.color = Color.white;
        }
	}
    
    void OnTriggerEnter(Collider col_object)
    {
        m_onObject = true;
    }

    void    OnTriggerStay(Collider col_object)
    {
        m_onObject = true;
    }

    void OnTriggerExit(Collider col_object)
    {
        m_onObject = false;
    }


      void OnControllerColliderHit(ControllerColliderHit hit)
    {
        m_onObject = true;
    }

}

using UnityEngine;
using System.Collections;

public class Kakashi : MonoBehaviour {
    
    private DamageTrigger m_trigger;

    bool m_active = false;
    bool m_Raven_Hit = false;


    [SerializeField, HeaderAttribute("Motionさせる時間")]
    private float m_motionTime;


    //test
    public GameObject model_root; 

    private float m_current_MotionTime;
    void Start ()
    {
        m_trigger = GetComponentInChildren<DamageTrigger>();
	}

    void AngryMotion()
    {
        var renderer = GetComponentInChildren<MeshRenderer>();
        renderer.material.color = Color.red;
        if(m_current_MotionTime < Time.time)
        {
            m_Raven_Hit = false;
        }
    }
	
	// Update is called once per frameA
	void Update ()
    {
	    if(m_trigger.IsHit() && m_active)
        {
            m_current_MotionTime = Time.time + m_motionTime;
            m_Raven_Hit = true;
        }

        if(m_Raven_Hit)
        {
            AngryMotion();
        }
        else
        {
            var renderer = GetComponentInChildren<MeshRenderer>();
            renderer.material.color = Color.white;
        }

   
       for(int i =0 ; i < model_root.transform.childCount ;i++)
       {
           var material = model_root.transform.GetChild(i).GetComponent<MeshRenderer>().material;
           material.color =(m_active)? Color.red : Color.white;
       }
   }

	

    public void ElectricLight_ON()
    {
        m_active = true;
        m_trigger.OnCollisionBegin();
    }

    public void ElectricLight_OFF()
    {
        m_active = false;
        m_trigger.onCollisionEnd();
    }
}

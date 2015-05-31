using UnityEngine;
using System.Collections;

public class EventRaven : MonoBehaviour {


    private Transform[] m_movetargetTransForm;
    private FruitDropper m_dropper;

    [SerializeField, HeaderAttribute("移動スピード")]
    private float move_speed = 1.0f;

    Animator m_animator;


    private bool m_drop = false;

	// Use this for initialization
	void Start ()
    {
        m_dropper = GetComponent<FruitDropper>();
        m_movetargetTransForm = Objectmanager.m_instance.m_fruit_Counter.m_fruitmanager.m_event_Manager.m_evepointHolder.GetKarasuPoint();
        this.transform.position = m_movetargetTransForm[0].position;
	}

    void Move()
    {
        Vector3 vec = m_movetargetTransForm[1].position - m_movetargetTransForm[0].position;
        this.transform.position += vec.normalized * Time.deltaTime * move_speed;

        Vector3 d = this.transform.position - m_movetargetTransForm[1].position;
        if(d.magnitude < 0.1f && !m_drop)
        {
            m_dropper.Drop();
            m_drop = true;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}
}

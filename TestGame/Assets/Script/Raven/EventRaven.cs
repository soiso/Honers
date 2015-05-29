using UnityEngine;
using System.Collections;

public class EventRaven : MonoBehaviour {


    private Transform[] m_movetargetTransForm;
    private FruitDropper m_dropper;

    Animator m_animator;


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
        this.transform.position += vec.normalized * Time.deltaTime;

    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}
}

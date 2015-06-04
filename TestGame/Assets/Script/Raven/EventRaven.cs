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
        m_animator = GetComponent<Animator>();
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
            m_animator.SetBool("isDrop", true);
            m_dropper.Drop();
            m_drop = true;
        }

    }
	
    void EraseCheck()
    {
        Vector3 screenpos = Camera.main.WorldToViewportPoint(this.transform.position);

        if (screenpos.x >= 1.1 ||
            screenpos.x <= -0.1 ||
            screenpos.y <= -0.1 ||
            screenpos.y >= 1.1)
        {
            DestroyObject(this.gameObject);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        Move();
        if(m_drop)
        {
            EraseCheck();
        }
	}
}

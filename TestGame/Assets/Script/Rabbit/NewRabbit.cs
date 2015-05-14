using UnityEngine;
using System.Collections;

public class NewRabbit : FieldObjectInterface {

    public enum STATE_TYPE
    {
        move,
        sleep,
        eat,
        num_state,
    }
    [SerializeField,HeaderAttribute("人参の場所")]
    public GameObject m_carrot;
    [SerializeField, HeaderAttribute("寝床の場所")]
    public GameObject m_sleepSpot;

    public GameObject m_current_MoveTarget { get; set; }
    private TimeZone_BoxCollider m_myTimezoneCollider;
    public Animator m_animator { get; set; }

    public FieldObjectStateMachine m_state_Machine { get; private set; }

    public FieldObjectStateInterface m_move_State { get; private set; }

    public bool m_CanMove { get; set; }

    public CharacterController m_controller;

	void Start () 
    {
        m_controller = GetComponent<CharacterController>();
        m_myTimezoneCollider = GetComponentInChildren<TimeZone_BoxCollider>();
        m_animator = GetComponent<Animator>();
        m_state_Machine = GetComponent<FieldObjectStateMachine>();
        var tes = m_animator.GetBehaviour<RabbitMotion>();
        tes.m_owner = this;
        m_move_State  = gameObject.AddComponent<Rbi_Move>();
        m_state_Machine.SetBook_State
            (m_sleepSpot.GetComponent<Container_FieldObjectState>().m_state);
       
        m_state_Machine.ChangeState(gameObject);

	}

	void Update () 
    {

        m_CurrentTimeZone = m_myTimezoneCollider.m_myColliderTimeZone;
        m_state_Machine.Execute(this.gameObject);

        //Debug.Log("CurrentState : " + m_state_Machine.m_current_State.ToString());

        Vector3 g = new Vector3(0, 0, 0);
        g.y = Physics.gravity.y * Time.deltaTime;
        m_controller.Move(g);
	}


    public bool  Set_MoveTarget(GameObject target)
    {
        if (m_current_MoveTarget == target)
            return false;

        m_current_MoveTarget = target;
        return true;
    }
    
}

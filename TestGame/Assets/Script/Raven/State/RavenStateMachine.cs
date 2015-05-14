using UnityEngine;
using System.Collections;

public class RavenStateMachine : MonoBehaviour {

    public Raven m_owner{get; private set;}
    public RavenStateBase   m_current_State{get; private set;}
    public RavenStateBase m_default_state{get; set;}

    //そのうちこっちに
    public RavenStateBase m_book_State { get; private set; }

    void Start()
    {
        m_owner = GetComponent<Raven>();
        m_default_state = gameObject.AddComponent<R_Patrol>();
        SetBookState(m_default_state);
        ChangeState();
        
    }

    public void Execute()
    {
        if(m_current_State.Execute(this))
        {
            if(m_book_State)
                ChangeState();
        }
    }

    public bool ChangeState()
    {
        if (m_current_State)
            m_current_State.Exit_State(this);
        m_current_State = m_book_State;
        m_current_State.Enter(this);
        m_book_State = null;
        return true;
    }

    public bool SetBookState(RavenStateBase newstate)
    {
        if (m_book_State)
            return false;
        m_book_State = newstate;
        return true;
    }

}

using UnityEngine;
using System.Collections;

public class FieldObjectStateMachine : MonoBehaviour {

    public FieldObjectStateInterface m_current_State { get; private set; }

    private FieldObjectStateInterface m_book_State;

	void Start () {
	
	}

    public void Execute(GameObject owner)
    {
        if (m_current_State.Execute(owner))
        {
            if(m_book_State)
                ChangeState(owner);
        }
    }

    public bool ChangeState(GameObject owner)
    {
        if (m_current_State)
            m_current_State.Exit_State(owner);
        m_current_State = m_book_State;
        m_current_State.Enter(owner);
        m_book_State = null;
        return true;
    }

    public bool SetBook_State(FieldObjectStateInterface new_state)
    {
        if(m_book_State)
        {
            Debug.Log("CurrentBookState is " + m_book_State.ToString() + "!!");
            return false;
        }
        m_book_State = new_state;
        return true;
    }

    
}

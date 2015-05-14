using UnityEngine;
using System.Collections;

public class PlayerStateMachine : MonoBehaviour {

    private Player m_player = null;
    private PlayerStateInterFace m_current_State;
    private P_Move m_default_State;

    void Start()
    {
        m_player = GetComponent<Player>();
        m_default_State = gameObject.AddComponent<P_Move>();
        Change_State(m_default_State);
    }

    public  bool Execute()
    {
        if (!m_current_State)
        {
            Debug.Log("State is null !!");
            return false;
        }
      bool result = m_current_State.Execute(ref m_player);
     // Debug.Log(m_current_State.ToString());
      return result;
    }

   public  bool Change_State(PlayerStateInterFace new_state)
    {
        if (m_current_State)
            m_current_State.Exit_State(ref m_player);
        m_current_State = new_state;
        m_current_State.Enter(ref m_player);
        return true;
    }
    
    public void ReturnDefaultState()
   {
       m_current_State = m_default_State;
   }

}

using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class PlayerFrameInformation : MonoBehaviour
{

    public enum movedirection { LEFT = -1, STAY, RIGHT = 1 };
    public enum mouse_button
    { m_LEFT, m_RIGHT, m_WHEEL };


    public enum MOVE_TYPE
    {
        frame,
        slider,
    }

    public MOVE_TYPE m_movetype = MOVE_TYPE.frame;

    private Vector2 axis = Vector2.zero;
    public Vector2 GetAxis
    {
        get { return axis; }
    }
    private bool is_move = false;
    public bool Is_Move
    {
        get { return is_move; }
    }
    private movedirection move_Direction = movedirection.STAY;
    public movedirection MoveDirection
    {
        get { return move_Direction; }
    }

    [SerializeField]
    private GameObject m_right_touch;
    [SerializeField]
    private GameObject m_left_touch;

    [SerializeField]
    Slider m_slider;

    void Start()
    {
        if(!m_right_touch)
        {
            m_right_touch = GameObject.Find("RightToughMesh");
        }
        if(!m_left_touch)
        {
            m_left_touch = GameObject.Find("LeftToughMesh");
        }

       
    }

    public void Create_WithSlider()
    {
        if (!m_slider)
            return;

        float slidervalue = m_slider.value;
        
        if(slidervalue <= 33.3f)
        {
            gameObject.GetComponent<Player>().m_StopFrame = 0;
            is_move = true;
            move_Direction = movedirection.LEFT;
            return;
        }

        if(slidervalue >= 66.6f)
        {
            gameObject.GetComponent<Player>().m_StopFrame = 0;
            is_move = true;
            move_Direction = movedirection.RIGHT;
            return;
        }

        is_move = false;
        move_Direction = movedirection.STAY;
        gameObject.GetComponent<Player>().m_StopFrame++;
    }

    public void Create(Transform player_TransForm)
    {
        if (m_left_touch.GetComponent<Touch_Collider>().m_is_active && m_right_touch.GetComponent<Touch_Collider>().m_is_active)
        {
            is_move = false;
            move_Direction = movedirection.STAY;
            gameObject.GetComponent<Player>().m_StopFrame++;
        }
        else if (m_left_touch.GetComponent<Touch_Collider>().m_is_active && !m_right_touch.GetComponent<Touch_Collider>().m_is_active)
        {
               gameObject.GetComponent<Player>().m_StopFrame=0;
                is_move = true;
                move_Direction = movedirection.LEFT;
        }
        else if (!m_left_touch.GetComponent<Touch_Collider>().m_is_active && m_right_touch.GetComponent<Touch_Collider>().m_is_active)
        {
            gameObject.GetComponent<Player>().m_StopFrame = 0;
            is_move = true;
            move_Direction = movedirection.RIGHT;
        }
        else
        {
            is_move = false;
            move_Direction = movedirection.STAY;
            gameObject.GetComponent<Player>().m_StopFrame++;
        }
    }


}
  

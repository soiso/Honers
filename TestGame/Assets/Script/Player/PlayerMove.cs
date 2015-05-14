using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {


    //private Animator m_animator;
    //private Rigidbody m_rigidBody;
 

    //private PlayerStateInterFace m_current_State = null;

    //struct FrameInfomation
    //{


    //};
    //private FrameInfomation m_frameInfo; 

	//void Start () 
    //{
      //  m_animator = GetComponent<Animator>();
       // m_rigidBody = GetComponent<Rigidbody>();
       // m_current_Speed = 0f;
        //m_current_State = new P_Move();
	//}

    //void    Mouse_Event()
    //{
    //    //if(Input.GetMouseButton((int)mouse_button.m_LEFT))
    //    //{
    //    //    Vector3 mouse_pos = Input.mousePosition;
    //    //    なんかいるっぽい
    //    //    mouse_pos.z = 0.5f;
    //    //    mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
    //    //    mouse_pos.z = transform.position.z;
    //    //    BoxCollider col = GetComponent<BoxCollider>();
    //    //    Ray ray = new Ray();
    //    //    ray.origin = mouse_pos;
    //    //    ray.direction = new Vector3(1, 0, 0);
    //    //    RaycastHit hit = new RaycastHit();
    //    //    hit
    //    //    if(!col.Raycast(ray,out hit,1.0f))
    //    //    {
    //    //        m_frameInfo.is_move = true;
    //    //     m_frameInfo.move_Direction = (mouse_pos.x >= transform.position.x) ?
    //    //                                                       movedirection.RIGHT : movedirection.LEFT;
    //    //    }
    //    //    else
    //    //    {
    //    //        m_frameInfo.is_move = false;
    //    //        m_frameInfo.move_Direction = movedirection.STAY;
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    m_frameInfo.move_Direction = movedirection.STAY;
    //    //    m_frameInfo.is_move = false;
    //    //}
    //}

    //void Create_FrameInformation()
    //{
    //    Mouse_Event();
    //}

    void    Move()
    {
        Calculate_CurrentSpeed();
        Vector3 right = Camera.main.transform.right.normalized;
        //Vector3 move = right * m_current_Speed * (float)m_frameInfo.move_Direction;
        //m_rigidBody.MovePosition(this.transform.position + move);
        
    }

    void    Rotate()
    {
        //if (!m_frameInfo.is_move)
        //    return;
        //Vector3 rotate_Angle = new Vector3(0,0,0);
        //rotate_Angle.x = (m_frameInfo.move_Direction == movedirection.LEFT) ? -1f :1f;
        
        //Quaternion rotate_Q = Quaternion.LookRotation(rotate_Angle);
        //transform.rotation = Quaternion.Slerp(transform.rotation,rotate_Q,0.1f);
    }

    void    Animation()
    {
        //if (m_frameInfo.move_Direction == movedirection.STAY)
        //{
        //    m_animator.SetBool("is_Running", false);
        //}
        //else
        //    m_animator.SetBool("is_Running", true);
    }

    void Calculate_CurrentSpeed()
    {
        //if(m_frameInfo.move_Direction!= movedirection.STAY)
        //{
        //    m_current_Speed += m_acceleration;
        //    if (m_current_Speed > m_maxSpeed)
        //        m_current_Speed = m_maxSpeed;
        //}
        //else
        //{
        //    m_current_Speed -= m_brakeSpeed;
        //    if (m_current_Speed < 0)
        //        m_current_Speed = 0;
        //}
    }
   
	// Update is called once per frame
	void Update () 
    {
       // Create_FrameInformation();
        Move();
        Rotate();
        Animation();
        Vector3 test = transform.position;

        //c#のラムダ的なやつ
        //#はstructはコピー渡し、クラスは参照渡し
        //System.Func<int, int, int> aa =
        //     =>
        //    {
  
        //    };
       
	}

    public void Player_Warp(Vector3 warp_Pos)
    {
        this.transform.position = warp_Pos;
    }


}

using UnityEngine;
using System.Collections;

public class EventRabbit : MonoBehaviour
{
    private Transform[] m_Point;
    private Vector3 m_TargetPosition;

    private FruitDropper m_Dropper;

    private Animator m_Animator;

    private bool m_isDrop;

    enum State
    {
        Move = 0,
        Drop,
        Wait,
        Delete,
    }
    private State m_CurrentState;
    private int m_WaitTimer;

	// Use this for initialization
	void Start () 
    {
        m_Dropper = GetComponent<FruitDropper>();
        m_Animator = GetComponent<Animator>();
        m_Point = Objectmanager.m_instance.m_fruit_Counter.m_fruitmanager.m_event_Manager.m_evepointHolder.GetUsagiPoint();
        this.transform.position = m_Point[0].position;
        this.transform.rotation = m_Point[0].rotation;
        m_TargetPosition = m_Point[1].position;
        m_isDrop = false;
        m_CurrentState = State.Move;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;
        
        switch( m_CurrentState )
        {
            case State.Move:
                //移動
                Move();

                //目的地着いた時の次のステート
                if( Vector3.Distance( m_TargetPosition, this.transform.position) < 0.01f )
                {
                    if( m_isDrop )
                    {
                        m_CurrentState = State.Delete;
                    }
                    else
                    {
                        m_CurrentState = State.Drop;
                    }
                }
                break;
            case State.Drop:
                //フルーツ落とせ
                //右から来た時フルーツが裏返るから
                Quaternion temp = this.transform.rotation;
                this.transform.rotation = new Quaternion(temp.x, .0f, temp.z, temp.w);
                m_Dropper.Drop();
                this.transform.rotation = temp;

                m_isDrop = true;
                m_Animator.SetBool("isDrop", true);
                m_WaitTimer = 60;
                m_CurrentState++;
                break;
            case State.Wait:
                //その場で少し停止
                m_WaitTimer--;
                if (m_WaitTimer == 0)
                {
                    m_Animator.SetBool("isDrop", false);
                    m_CurrentState = State.Move;
                    m_TargetPosition = m_Point[0].position;
                }
                break;
            case State.Delete:
                //削除
                DestroyObject(gameObject);
                break;
        }
	}

    void Move()
    {
        Vector3 vec = m_TargetPosition - this.transform.position;

        Vector3 rotate_Angle = new Vector3(0, 0, 0);
        rotate_Angle.z = (vec.x <= .0f) ? -1.0f : 1.0f;

        vec = vec.normalized;
        this.transform.position += vec* Time.deltaTime;

        Quaternion rotate_Q = Quaternion.LookRotation(rotate_Angle);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotate_Q, 0.1f);
    }
}

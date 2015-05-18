using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    [SerializeField, Range(0f, 50f)]
    private float m_distance = 20f;
    [SerializeField]
    Transform m_target_TransForm;
    [SerializeField]
    Transform m_default_target;
    private Vector3 m_default_PosVec;
    [SerializeField, Range(0, 1)]
    private float m_lerp_Speed;
    [SerializeField, Range(0, 1)]
    private float m_lerp_Speed_Inv;
    private Vector3 m_default_Pos;
    [SerializeField, Range(0f, 10f)]
    private float m_timer = .0f;
    private bool cMove;
    private bool Invers;
    private float wait_begin_time;
	void Start () 
    {
        m_default_PosVec = new Vector3(.0f, .0f, -1f);
        m_default_Pos = this.transform.position;
        Vector3 default_target_Pos = m_default_target.transform.position;
        cMove = false;
        Invers = false;
	}
	

	void    Lock()
    {
        Vector3 target_pos = m_target_TransForm.position;
        target_pos += m_default_PosVec * m_distance;

        this.transform.position = Vector3.Lerp(this.transform.position, target_pos, m_lerp_Speed);
        Vector3 look_pos = m_target_TransForm.position;
        this.transform.LookAt(look_pos);
    }

	void Update () 
    {
        //Lock();
        cMove_Target();
	}

    public void cMove_Begin()
    {
        cMove = true;
        Invers = false;
        wait_begin_time = -1.0f;
    }

    public void cMove_Target()
    {
        if (!cMove) return;
        if (!Invers)
        {
            Vector3 target_pos = m_target_TransForm.position;
            target_pos.z = this.transform.position.z;

            this.transform.position = Vector3.Lerp(this.transform.position, target_pos, m_lerp_Speed);

            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 4.5f, 0.1f);

            if (Vector3.Distance(target_pos, this.transform.position) < 1.0f)
            {
                if (wait_begin_time < .0f)
                    wait_begin_time = Time.time;
                if (wait_begin_time + m_timer < Time.time)
                    Invers = true;
            }
        }
        else
        {
            Vector3 target_pos = m_default_Pos;
            target_pos.z = this.transform.position.z;

            this.transform.position = Vector3.Lerp(this.transform.position, target_pos, m_lerp_Speed_Inv);

            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 8.5f, 0.1f);
        }
    }

}
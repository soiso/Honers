using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    [SerializeField, Range(0f, 50f)]
    private float m_distance = 20f;
    [SerializeField]
    Transform m_target_TransForm;
    private Vector3 m_default_PosVec;
    [SerializeField, Range(0, 1)]
    private float m_slerp_Speed;
    

	void Start () 
    {
        m_default_PosVec = new Vector3(0f, 0f, -1f);
	}
	

	void    Lock()
    {
        Vector3 target_pos = m_target_TransForm.position;
        target_pos += m_default_PosVec * m_distance;

        this.transform.position = Vector3.Lerp(this.transform.position, target_pos, m_slerp_Speed);
        Vector3 look_pos = m_target_TransForm.position;
        this.transform.LookAt(look_pos);
    }

	void Update () 
    {
      //  Lock();
	}
}

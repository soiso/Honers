using UnityEngine;
using System.Collections;

public class Picture : MonoBehaviour
{

    [SerializeField]
    private Texture[] texture;

    [SerializeField]
    private GameObject m_target;

    private bool move_flg = false;
    [SerializeField, Range(0, 10)]
    private float move_speed = .0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (move_flg)
        {
            Move();
        }
    }

    public void SetPicture(int pic_num)
    {
        this.GetComponent<Renderer>().material.mainTexture = texture[pic_num];
    }

    private Vector3 m_StartPos;
    private Vector3 m_EndPos;
    private bool Touch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_StartPos = Input.mousePosition;
            m_StartPos.z = .0f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_EndPos = Input.mousePosition;
            m_EndPos.z = .0f;
            Vector3 FlickVec = m_EndPos - m_StartPos;
            FlickVec.Normalize();
            Vector3 Horlizon = new Vector3(1.0f, .0f, .0f);
            if (Vector3.Dot(FlickVec, Horlizon) < .0f)
            {
                return true;
            }
        }

        return false;
    }

    private void Move()
    {
        Vector3 angle = m_target.transform.position - this.transform.position;
        angle.Normalize();
        if(Vector3.Distance(m_target.transform.position,this.transform.position)<5.0f)
        {
            Vector3 new_pos = this.transform.position + angle * move_speed;
            this.transform.position = new_pos;
        }
    }
}

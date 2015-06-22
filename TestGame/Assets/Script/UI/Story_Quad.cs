using UnityEngine;
using System.Collections;

public class Story_Quad : MonoBehaviour {

    [SerializeField]
    private GameObject NextQuad;
    private bool quad_enable = false;
    [SerializeField,Header("最初のQuadかどうか")]
    private bool BeginQuad=false;
    [SerializeField, Header("最後のQuadかどうか")]
    private bool EndQuad = false;
    [SerializeField]
    private GameObject m_target;
    [SerializeField, Range(0, 10)]
    private float move_speed = .0f;
    private bool move_flg;

    [SerializeField, SceneName,Header("次のシーン(最後のカードのみ有効)")]
    private string next_scene_name;

	// Use this for initialization
	void Start () {
        Color col = this.GetComponent<Renderer>().material.color;
	    this.GetComponent<Renderer>().material.color = new Color(col.r,col.g,col.b,0);
        if(BeginQuad)
        {
            QuadEnable();
            Objectmanager.m_instance.m_scene_manager.NextSceneLoad(next_scene_name);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!quad_enable) return;
        if(Touch())
        {
            move_flg = true;
        }
        if (move_flg)
        {
            Move();
        }
	}
    public void QuadEnable()
    {
        quad_enable = true;
        Color col = this.GetComponent<Renderer>().material.color;
        this.GetComponent<Renderer>().material.color = new Color(col.r, col.g, col.b, 255);
    }

    private void Move()
    {
        Vector3 angle = m_target.transform.position - this.transform.position;
        angle.Normalize();
        //ターゲットとの距離を測る
        if (Vector3.Distance(m_target.transform.position, this.transform.position) > 5.0f)
        {
            Vector3 new_pos = this.transform.position + angle * move_speed;
            this.transform.position = new_pos;
        }
        else
        {
            //次に読むQuadがあるかないか
            if(EndQuad)
            {
                Objectmanager.m_instance.m_scene_manager.NextSceneLoad(next_scene_name);
            }
            else
            {
                Destroy(this);
            }
        }
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
                //次のQuadを有効化(EndQuadでないとき)
                if(!EndQuad)
                NextQuad.GetComponent<Story_Quad>().QuadEnable();
                return true;
            }
        }

        return false;
    }
}

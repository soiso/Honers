using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultBackGround : MonoBehaviour {

    private bool moveFlg = false;
    [SerializeField]
    private GameObject m_target;
    [SerializeField, Range(0, 10)]
    private float move_speed = .0f;
    [SerializeField]
    private GameObject image;

	// Use this for initialization
    void Awake()
    {
        image.GetComponent<Renderer>().material.mainTexture = Objectmanager.m_instance.m_scshot_Machine.Capture_Camera(Objectmanager.m_instance.m_screenShot_Camera);
        
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    //if(moveFlg)
    //{
    //    Move();
    //}
	}
    public void Move()
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
            Destroy(image);
        }
    }
    public void BeginMove(Transform pic)
    {
        //moveFlg = true;
        this.transform.SetParent(pic);
    }
}

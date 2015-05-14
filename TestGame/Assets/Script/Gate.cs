using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

    [SerializeField, HeaderAttribute("0: 朝1:昼2:夜")]
    private float[] m_gateHeight;

    [SerializeField, HeaderAttribute("ゲートの移動速度"),Range(0,0.1f)]
    private float m_move_Speed;

    [SerializeField, HeaderAttribute("ゲートを動かすハムスター(Root)")]
    private GameObject m_owner_GameObject;
    private Hamster m_owner_Ham;

    [SerializeField]
    private GameObject[] m_observer_Objcect;
    
	void Start () 
    {
        m_owner_Ham = m_owner_GameObject.GetComponent<Hamster>();
	}   
	
    void    Notice(int ham_time)
    {
       //やっつけ
        switch(ham_time)
        {
            case 0 :
                m_observer_Objcect[0].GetComponent<WayPoint_MockObserver>().OpenRoute();
                m_observer_Objcect[1].GetComponent<WayPoint_MockObserver>().OpenRoute();
                m_observer_Objcect[2].GetComponent<WayPoint_MockObserver>().CutOffRoute();
                break;

            case 1:
                m_observer_Objcect[0].GetComponent<WayPoint_MockObserver>().CutOffRoute();
                m_observer_Objcect[1].GetComponent<WayPoint_MockObserver>().OpenRoute();
                m_observer_Objcect[2].GetComponent<WayPoint_MockObserver>().OpenRoute();
                break;

            case 2:
                m_observer_Objcect[0].GetComponent<WayPoint_MockObserver>().CutOffRoute();
                m_observer_Objcect[1].GetComponent<WayPoint_MockObserver>().CutOffRoute();
                m_observer_Objcect[2].GetComponent<WayPoint_MockObserver>().OpenRoute();
                break;
        }
    }

    void Move()
    {
        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        int hamTimeZone = (int)m_owner_Ham.m_time_Zone;
        Notice(hamTimeZone);
        Vector3 move_Target = new Vector3(0, m_gateHeight[hamTimeZone], 0);
        this.transform.position = this.transform.position * (1.0f - m_move_Speed) + move_Target * m_move_Speed;
    }

	// Update is called once per frame
	void Update ()
    {
        Move();	
	}
}

using UnityEngine;
using System.Collections;

public class FruitDropper : MonoBehaviour {
    
    private FruitFactory m_factry;

    [SerializeField]
    private Transform m_drop_Point;


    [SerializeField, HeaderAttribute("落とすフルーツの種類数")]
    private int m_max_FruitType = (int)FruitInterFace.FRUIT_TYPE.num_normal_fruit;

    [SerializeField, HeaderAttribute("フルーツを落とすときの最小初速")]
    private float m_minForce;
    [SerializeField,HeaderAttribute("フルーツを落とすときの最大初速")]
    private float m_maxForce;

    [SerializeField, HeaderAttribute("自動で落とすかどうか")]
    private bool m_auto_Drop = false;

    [SerializeField, HeaderAttribute("フルーツを落とすまでの時間(autoDropにチェックが入ってる場合)")]
    private float m_drop_Interval = 1.0f;

    private float m_sporn_Time;

    void Start()
    {
        m_factry = GetComponent<FruitFactory>();
        m_sporn_Time = Time.time;
    }

    void Update()
    {
        if (!m_auto_Drop)
            return;

        if(Time.time >= m_sporn_Time + m_drop_Interval)
        {
            Drop();
            DestroyObject(gameObject);
        }
    }

    public  void Drop()
    {
        int index = Random.Range(0, m_max_FruitType);
        GameObject drop = m_factry.Create_Object((FruitInterFace.FRUIT_TYPE)index,-1);
        Vector3 force_vec = new Vector3(Mathf.Sign(Random.Range(-1.0f,1.0f)),
                                                                  Mathf.Sign(Random.Range(-1.0f,1.0f)),
                                                                        0f );
        drop.transform.position = m_drop_Point.position;

        //if (drop.GetComponent<FruitInfomation>().fruit_type == FruitInterFace.FRUIT_TYPE.apple)
        //{
          //  Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            //drop.transform.rotation = q;
        //}
        //else
            drop.transform.rotation = m_drop_Point.rotation;

        force_vec = force_vec.normalized * Random.Range(m_minForce, m_maxForce);

        Rigidbody r = drop.GetComponent<Rigidbody>();

    }
}

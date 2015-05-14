using UnityEngine;
using System.Collections;

public class FruitDropper : MonoBehaviour {
    
    private FruitFactory m_factry;

    [SerializeField]
    private Transform m_drop_Point;


    [SerializeField, HeaderAttribute("落とすフルーツの種類数")]
    private int m_max_FruitType = (int)Fruit.FRUIT_TYPE.num_fruit;

    [SerializeField, HeaderAttribute("フルーツを落とすときの最小初速")]
    private float m_minForce;
    [SerializeField,HeaderAttribute("フルーツを落とすときの最大初速")]
    private float m_maxForce;



    void Start()
    {
        m_factry = GetComponent<FruitFactory>();
    }

    public  void Drop()
    {
        GameObject drop = m_factry.Create_Object(Random.Range(0, m_max_FruitType));
        Vector3 force_vec = new Vector3(Mathf.Sign(Random.Range(-1.0f,1.0f)),
                                                                  Mathf.Sign(Random.Range(-1.0f,1.0f)),
                                                                        0f );
        drop.transform.position = m_drop_Point.position;

        if(drop.GetComponent<FruitInfomation>().fruit_type == Fruit.FRUIT_TYPE.apple)
        {
            Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
            drop.transform.rotation = q;
        }
        else
            drop.transform.rotation = m_drop_Point.rotation;

        force_vec = force_vec.normalized * Random.Range(m_minForce, m_maxForce);

        Rigidbody r = drop.GetComponent<Rigidbody>();
        //r.AddForce(force_vec);

    }
}

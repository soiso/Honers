using UnityEngine;
using System.Collections;

public class TestSporn : MonoBehaviour {

    private float m_last_SpornTime;
    private FruitArrangeManager m_owner;
    [SerializeField, HeaderAttribute("出現間隔")]
    private float m_interval ;
       [SerializeField, HeaderAttribute("確率")]
    private int m_kakuritu = 30;

	// Use this for initialization
	void Start () {
        m_owner = GetComponentInParent<FruitArrangeManager>();

        m_last_SpornTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Time.time  > m_last_SpornTime + m_interval)
        {
            int p = Random.Range(0, 100);
            if(p <m_kakuritu)
            {
                GameObject insert = m_owner.m_factory.Create_Object(Random.Range(0,5));
                insert.transform.position = this.transform.position;
                insert.transform.rotation = this.transform.rotation;
                if (insert.GetComponent<FruitInfomation>().fruit_type == Fruit.FRUIT_TYPE.apple)
                {
                    Quaternion q = Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
                    insert.transform.rotation = q;
                }
            }
            m_last_SpornTime = Time.time;
        }

	}
}

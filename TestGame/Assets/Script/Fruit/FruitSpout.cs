using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FruitSpout : MonoBehaviour {

    private FruitFactory m_factory;

    [SerializeField, HeaderAttribute("放出するフルーツ配列（Vacuumが一緒にアタッチされてる場合そっち優先）")]
    private int[] m_spoutArray;


    private bool m_isSpoutNow;

    private float m_interval = 0.1f;

    private List<int> m_spout_List;

    void Awake()
    {
        m_spoutArray = new int[(int)FruitInterFace.FRUIT_TYPE.num_normal_fruit];
        m_factory = GetComponent<FruitFactory>();
    }

    void Start () 
    {
	
	}
	
   IEnumerator SpoutFruit()
    {
       for(int i = 0 ; i < m_spoutArray.Length ; i++)
       {
           
           for(int j = 0 ; j < m_spoutArray[i] ; j++)
           {
               FruitInterFace.FRUIT_TYPE type = (FruitInterFace.FRUIT_TYPE)i;
               GameObject insert = m_factory.Create_Object(type);
               insert.transform.position = this.transform.position;
               insert.transform.rotation = Quaternion.identity;
               yield return new WaitForSeconds(m_interval);
           }
       }
    }

	// Update is called once per frame
	void Update () 
    {
	    if(m_isSpoutNow)
        {
            StartCoroutine("SpoutFruit");
            m_isSpoutNow = false;
        }
	}

    public void  Receive_Fruit(ref int[] send_Info)
    {
        if(m_spoutArray.Length != send_Info.Length)
        {
            Debug.Log("error!!");
            return;
        }

        for(int i = 0 ; i < m_spoutArray.Length ;i++)
        {
            m_spoutArray[i] = send_Info[i];
        }

        //for(int i = 0 ; i < send_Info.Length ; i++)
        //{
        //    for(int j = 0 ; )

        //}
    }

    public void Begin_Spout()
    {
        m_isSpoutNow = true;
    }
}

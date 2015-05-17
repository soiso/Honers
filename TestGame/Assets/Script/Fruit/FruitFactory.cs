using UnityEngine;
using System.Collections;

public class FruitFactory : MonoBehaviour {

    [SerializeField]
    GameObject[] m_createFruitList;
	// Use this for initialization
	void Start () {
	
	}

    public GameObject Create_Object(FruitInterFace.FRUIT_TYPE type)
   {
        GameObject ret  = null;
        int create_Index = (int)type;
       if (create_Index < 0 || create_Index >= m_createFruitList.Length)
           return null;

        for(int i =  0 ; i < m_createFruitList.Length ; i++)
        {
            var info = m_createFruitList[i].GetComponent<FruitInfomation>();
            int val = (int)info.fruit_type;
            if (val == create_Index)
            {
                ret = Instantiate(m_createFruitList[i]);
            }
        }

       return ret;
   }

	// Update is called once per frame
	void Update () 
    {
	
	}
}

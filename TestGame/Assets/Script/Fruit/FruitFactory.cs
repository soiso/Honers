﻿using UnityEngine;
using System.Collections;

public class FruitFactory : MonoBehaviour {

    [SerializeField]
    GameObject[] m_createFruitList;
	void Start () {
	
	}

    /**
    * @note イベントを起こさないフルーツは-1
    **/
    public GameObject Create_Object(FruitInterFace.FRUIT_TYPE type, int eventIndex = -1 )
   {
        GameObject ret  = null;
        int create_Index = (int)type;
       if (create_Index < 0)
           return null;

        for(int i =  0 ; i < m_createFruitList.Length ; i++)
        {
            var info = m_createFruitList[i].GetComponent<FruitInfomation>();
            int val = (int)info.fruit_type;
            if (val == create_Index)
            {
                ret = Instantiate(m_createFruitList[i]);
                ret.GetComponent<FruitInterFace>().m_event_Affiliation = eventIndex;
                return ret;
            }
        }

       return ret;
   }

	// Update is called once per frame
	void Update () 
    {
	
	}
}

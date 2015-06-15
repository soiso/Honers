using UnityEngine;
using System.Collections;

public class FruitFactory : MonoBehaviour {

    [SerializeField]
    Material[] m_materials;

    [SerializeField]
    GameObject[] m_createFruitList;
	void Start ()
    {
	
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
                var t = ret.GetComponent<MeshRenderer>();
                if (!t)
                    Debug.Log("t");
                t.material = m_materials[val];
                ret.GetComponent<FruitInterFace>().m_event_Affiliation = eventIndex;
                Objectmanager.m_instance.m_fruit_Counter.m_fruitmanager.Regist_Fruit(ret);
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

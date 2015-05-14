using UnityEngine;
using System.Collections;

public class FruitFactory : MonoBehaviour {

    [SerializeField]
    GameObject[] m_createFruitList;
	// Use this for initialization
	void Start () {
	
	}

    public GameObject Create_Object(int create_Index)
   {
       if (create_Index < 0 || create_Index >= m_createFruitList.Length)
           return null;
       GameObject ret = Instantiate(m_createFruitList[create_Index]);

       return ret;
   }

	// Update is called once per frame
	void Update () 
    {
	
	}
}

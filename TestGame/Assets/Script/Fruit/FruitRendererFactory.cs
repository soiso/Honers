using UnityEngine;
using System.Collections;

public class FruitRendererFactory : MonoBehaviour {


    [SerializeField, HeaderAttribute("作成するフルーツ（Rendererを入れる）")]
    GameObject[] m_createRendererList;


    public GameObject Create_Object(int create_Index)
    {
        GameObject ret = null;
        if (create_Index < 0)
            return null;

        for (int i = 0; i < m_createRendererList.Length; i++)
        {
            var info = m_createRendererList[i].GetComponent<FruitInfomation>();
            int val = (int)info.fruit_type;
            if (val == create_Index)
            {
                ret = Instantiate(m_createRendererList[i]);
                return ret;
            }
        }

        return ret;
    }

}

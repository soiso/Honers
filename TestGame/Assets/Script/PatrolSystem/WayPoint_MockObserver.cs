using UnityEngine;
using System.Collections;

public class WayPoint_MockObserver : MonoBehaviour {

    [SerializeField, HeaderAttribute("必ずWayPointクラスがアタッチされたgameobjectを登録する")]
    GameObject[] m_target;


    public bool CutOffRoute()
    {
        bool ret = false;
        foreach (GameObject from in m_target)
        {
            foreach (GameObject to in m_target)
            {
                if (from == to)
                    continue;

                var w = from.GetComponent<WayPoint>();
                if (from.GetComponent<WayPoint>().CutOFF_Route(to))
                {
                    ret = true;
                }
            }
        }
        return ret;
    }

    public bool OpenRoute()
    {
        bool ret = false;
        foreach (GameObject from in m_target)
        {
            foreach (GameObject to in m_target)
            {
                if (from == to)
                    continue;
                if (from.GetComponent<WayPoint>().OpenRoute(to))
                {
                    ret = true;
                }
            }
        }
        return ret;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WayPoint : MonoBehaviour {

    public enum STATUS
    {
        NONE,
        OPEN,
        CLOSE,
    }

    public class Edge
    {
        public GameObject m_from;
        public GameObject m_to;
        public bool m_canGoalong;

        public Edge( GameObject from,  GameObject to)
        {
            m_from = from;
            m_to = to;
            m_canGoalong = true;
        }
    }

    [SerializeField]
    public GameObject[] m_connect;

    public Transform m_my_TransForm { get; private set; }

    [HideInInspector]
    public STATUS m_status;
     [HideInInspector]
    public GameObject m_parent;
     [HideInInspector]
    public float m_score;

    List<Edge> m_edge_list; //このエッジはWaypoint間が接続されてるかどうかの判定に使う

    private void Create_EdgeList()
    {
        m_edge_list.Clear();
        foreach(GameObject it in m_connect)
        {
            m_edge_list.Add(new Edge( this.gameObject,  it));
        }
    }

    public void Reset()
    {
        m_status = STATUS.NONE;
        m_score = 0;
        m_parent = null;
    }

	// Use this for initialization
	void Start () 
    {
        m_my_TransForm = this.transform;
        Reset();
        m_edge_list = new List<Edge>();
        Create_EdgeList();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool Is_CutOff(GameObject to)
    {
        foreach(Edge it in m_edge_list)
        {
            if(it.m_to == to)
            {
                return (!it.m_canGoalong)?true : false;
            }
        }
        return false;
    }

    public bool CutOFF_Route(GameObject cutoff_Target )
    {
        foreach(Edge it in m_edge_list)
        {
            if(it.m_to == cutoff_Target)
            {
                it.m_canGoalong = false;
                return true;
            }
        }
        return false;
    }

    public bool OpenRoute(GameObject openRoute_Target)
    {
                foreach(Edge it in m_edge_list)
        {
            if(it.m_to == openRoute_Target)
            {
                it.m_canGoalong = true;
                return true;
            }
        }
        return false;
    }

}

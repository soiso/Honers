using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WayPointGraph : MonoBehaviour {

    public List<GameObject> m_waypoint_list { get; private set; }

    [SerializeField]
    private string m_waypoint_parentname;
	// Use this for initialization
	void Start () {
        var waypoint = GameObject.Find(m_waypoint_parentname);
        m_waypoint_list = new List<GameObject>();
        for (int i = 0; i < waypoint.transform.childCount; i++)
        {
            var tes = waypoint.transform.GetChild(i).gameObject;
            m_waypoint_list.Add(tes);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public GameObject Find(ref Transform find)
    {

        foreach (GameObject it in m_waypoint_list)
        {
            if (it.transform == find)
            {
                return it;
            }
        }
        return null;
    }

    public GameObject Find_Dist(ref Transform find_position)
    {
        float most_near = 100000.0f;
        GameObject ret = null;

        foreach (GameObject it in m_waypoint_list)
        {
            float d = (find_position.position - it.transform.position).sqrMagnitude;
            if (d <= most_near)
            {
                most_near = d;
                ret = it;
            }
        }
        return ret;
    }

    private bool SetRoute(ref GameObject start, ref GameObject goal,
                                            ref List<GameObject> ret_list)
    {
        ret_list.Clear();
        GameObject work = goal;
        while (work != start)
        {
            ret_list.Add(work);
            work = work.GetComponent<WayPoint>().m_parent;
        }
        return true;
    }

    public bool SearchPath(Transform start_position, ref GameObject goal,
        ref List<GameObject> ret_pathlist)
    {
        Func<GameObject, List<GameObject>, bool> Open = (GameObject open_obj, List<GameObject> open_list) =>
        {
            open_obj.GetComponent<WayPoint>().m_status = WayPoint.STATUS.OPEN;
            open_list.Add(open_obj);
            return true;
        };

        Func<GameObject, List<GameObject>, bool> Close = (GameObject open_obj, List<GameObject> open_list) =>
        {
            open_obj.GetComponent<WayPoint>().m_status = WayPoint.STATUS.CLOSE;
            open_list.Remove(open_obj);
            return true;
        };

        All_Reset_WayPoint();

        GameObject start = Find_Dist(ref start_position);

        List<GameObject> openlist = new List<GameObject>();
        Open(start, openlist);

        while (openlist.Count != 0)
        {
            float most_min_score = 100000.0f;
            GameObject N = null;
            foreach (GameObject it in openlist)
            {
                float s = it.GetComponent<WayPoint>().m_score;
                if (s < most_min_score)
                {
                    N = it;
                    most_min_score = s;
                }
            }
            if (N == goal)
            {
                SetRoute(ref start, ref goal, ref ret_pathlist);
                return true;
            }

            WayPoint N_waypoint = N.GetComponent<WayPoint>();
            float cost = N_waypoint.m_score;


            Close(N, openlist);
            //Nの接続先をチェック
            foreach (GameObject connect in N_waypoint.m_connect)
            {
                WayPoint connect_Waypint = connect.GetComponent<WayPoint>();
                if (N_waypoint.Is_CutOff(connect))
                    continue;

                float n_connect = (N.transform.position - connect.transform.position).sqrMagnitude;
                
                float score = n_connect + cost;
               
                if (connect_Waypint.m_status == WayPoint.STATUS.NONE)
                {
                    connect_Waypint.m_score = score;
                    connect_Waypint.m_parent = N;
                    Open(connect, openlist);
                }
                else if (connect_Waypint.m_status == WayPoint.STATUS.OPEN)
                {
                    if (score < connect_Waypint.m_score)
                    {
                        connect_Waypint.m_score = score;
                        connect_Waypint.m_parent = N;
                        Open(connect, openlist);
                    }
                }
                else if (connect_Waypint.m_status == WayPoint.STATUS.CLOSE)
                {
                    if (score < connect_Waypint.m_score)
                    {
                        connect_Waypint.m_score = score;
                        connect_Waypint.m_parent = N;
                        Open(connect, openlist);
                    }
                }

            }//connect_foreach
        }   //while


        return false;
    }

    private void All_Reset_WayPoint()
    {
        foreach (GameObject it in m_waypoint_list)
        {
            it.GetComponent<WayPoint>().Reset();
        }
    }
}

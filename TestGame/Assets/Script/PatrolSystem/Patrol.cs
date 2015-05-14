using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class Patrol : MonoBehaviour {

	// Use this for initialization

    [SerializeField]
    private GameObject[] m_goal_list;
    public GameObject[] Getgoallist { get { return m_goal_list; } }
    private List<GameObject> m_route_list;

    [SerializeField, HeaderAttribute("それぞれのゴール確率(トータルで100に)")]
    private int[] m_goal_Probability;

    private WayPointGraph m_waypoint_graph;

    private GameObject m_current_Goal;
    public GameObject GetcurrentGoal { get { return m_current_Goal; } }

    [SerializeField, HeaderAttribute("経路更新間隔")]
    private float m_updateInterval= 2;

    private float m_next_UpdateTime = 0;

    public AudioClip clip;
    //private AudioSource[] audio;

    //スーパーやっつけ
    int[] m_probability_Array;

    void Initialize_Probability()
    {
        m_probability_Array = new int[100];


        int count = 0;
        for(int i = 0 ; i < m_goal_Probability.Length; i++)
        {
            for(int t =0; t < m_goal_Probability[i];t++)
            {
                m_probability_Array[count] = i;
                count++;
            }
        }
    }

	void Start () 
    {
        Initialize_Probability();
        m_current_Goal = null;
        m_route_list = new List<GameObject>();
        m_waypoint_graph = GetComponent<WayPointGraph>();
        //audio[1] = GetComponent<AudioSource>();
        //audio[1].clip = clip;
	}


    bool Is_SameWayPoint(Transform from,Transform to)
    {
        GameObject from_point = m_waypoint_graph.Find_Dist(ref from);
        GameObject to_point = m_waypoint_graph.Find_Dist(ref to);

        return (from_point == to_point) ? true : false;
    }

    private void Direction_NextGoal()
    {
        bool loop = true;
        while(loop)
        {
            int candidacy = m_probability_Array[UnityEngine.Random.Range(0, 100)];

            if(m_current_Goal != m_goal_list[candidacy])
            {
                m_current_Goal = m_goal_list[candidacy];
                loop = false;
            }
        }
 

        foreach(GameObject it in m_waypoint_graph.m_waypoint_list )
        {
            var renderer = it.GetComponent<MeshRenderer>();
            if (!renderer)
                renderer = it.GetComponentInChildren<MeshRenderer>();
            renderer.material.color = Color.white;
        }

    }

    private bool    Move()
    {
        //謎のバグ防止
        if (m_route_list.Count == 0)
            return true;

        GameObject current_Target = m_route_list.Last();
        Vector3 move_direc = (current_Target.transform.position - this.transform.position);
        this.transform.position += move_direc.normalized * 2f * Time.deltaTime;

      

        GameObject last_Goal = m_route_list.First();
        var renderer = last_Goal.GetComponent<MeshRenderer>();
        if (!renderer)
            renderer = last_Goal.GetComponentInChildren<MeshRenderer>();
        renderer.material.color = Color.red;


        if(move_direc.magnitude<= 0.1f)
        {
            m_route_list.Remove(current_Target);
            if(m_route_list.Count ==0)
            {
                return true;
            }
        }
        return false;
    }

    bool Is_PathSearchTime()
    {
        if(m_next_UpdateTime  <= Time.time)
        {
            return true;
        }
        return false;
    }

    public  void Direction_MoveProgram()
    {
        if (m_current_Goal == null)
        {
            Direction_NextGoal();
            m_waypoint_graph.SearchPath(this.transform, ref m_current_Goal, ref m_route_list);
            return;
        }
        if(Is_SameWayPoint(this.transform,m_current_Goal.transform))
            goto Exit;
        
        while (!m_waypoint_graph.SearchPath(this.transform, ref m_current_Goal, ref m_route_list))
        {
            Direction_NextGoal();
        }

        Exit :
        m_next_UpdateTime = Time.time + m_updateInterval;
    }

    public void Exexute()
    {
        if(Is_PathSearchTime())
        {
            Direction_MoveProgram();
        }
        
        //やっつけ
        if(Move() && m_current_Goal)
        {
            //ゴール時のイベントをおこす
            var goal_event = m_current_Goal.GetComponent<InterFace_WayPointGoal>();
            if (goal_event)
            {
                //この関数の戻り値がtrueの場合ChangeStateが呼ばれたことになる
                if(!goal_event.Arrival_Goal(this.gameObject))
                {
                    Direction_NextGoal();
                    m_waypoint_graph.SearchPath(this.transform, ref m_current_Goal, ref m_route_list);
                }
            }

        }
    }

    public void Clear_CurrentGoal()
    {
        m_current_Goal = null;
    }

    public GameObject Get_MoveTarget()
    {
        if (m_route_list.Count == 0)
            return null;
        return m_route_list.Last();
    }
}

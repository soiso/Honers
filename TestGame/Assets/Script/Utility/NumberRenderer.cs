﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class NumberRenderer : MonoBehaviour {

    [SerializeField]
    private Material[] m_numberMaterial;

    [SerializeField]
    private GameObject[] m_numbers;
    private int m_value= 0;
    IList<int> m_worklist;
    private int max_num = 999999;

    [SerializeField, Range(0, 10), HeaderAttribute("ロールが開始するまでの待ち時間")]
    private float start_wait = 0;

    private float m_start_time;

    [SerializeField, HeaderAttribute("ドラムロールする時間")]
    private float m_rollTime = 0.5f;

    private float m_rollBeginTime;

    private bool m_is_rolling = false;

    private int m_current_surplus =0;

    private ScaleWeaver m_scale_Weaver = null;

    void Awake()
    {
        m_scale_Weaver = GetComponent<ScaleWeaver>();
        m_worklist = new List<int>();
    }

	void Start () 
    {
       
        Calculate(m_value);
      m_start_time = Time.time;
	}


    void Update()
    {
        if (Time.time > m_start_time + start_wait)
        {
            if (m_is_rolling)
            {
                if (Dram_Roll())
                {
                    Calculate(m_value);
                }
            }
        }
    }

    bool Dram_Roll()
    {
        for (int i = 0; i < m_current_surplus ; i ++ )
        {
            var render_num = m_numbers[i].GetComponent<Numbers>();
            render_num.Enable();
            render_num.SetMaterial(m_numberMaterial[Random.Range(0, 9)]);
        }
        for (int i = m_current_surplus; i < m_numbers.Length; i++)
        {
            var render_num = m_numbers[i].GetComponent<Numbers>();
            render_num.Disalble();
        }

        if(Time.time > m_rollBeginTime + m_rollTime)
        {
            m_is_rolling = false;
            return true;
        }
        return false;
    }


    void Calculate(int val)
    {
        m_worklist.Clear();

		if (val == 0) 
		{
			for(int i = 0 ; i < m_numbers.Length ; i++)
			{
				var rr = m_numbers[i].GetComponent<Numbers>();
				rr.Disalble();
			}

			var render_num = m_numbers[0].GetComponent<Numbers>();
			render_num.Enable();
			render_num.SetMaterial(m_numberMaterial[0]);
			return;
		}
       //桁数を出す
        while(val > 0)
        {
            int surplus = val % 10;
            m_worklist.Insert(0, surplus);
           // m_worklist.Add(surplus);
            val /= 10;
        }

        m_current_surplus = m_worklist.Count;
        int count =0;
        foreach (var it in m_worklist)
        {
            var render_num = m_numbers[count].GetComponent<Numbers>();
            render_num.Enable();
            render_num.SetMaterial(m_numberMaterial[it]);
            count++;
        }

        for (int i = m_current_surplus; i < m_numbers.Length; i++)
        {
			var render_num = m_numbers[i].GetComponent<Numbers>();
			render_num.Disalble();
        }



    }

    public void SetNumber(int value,bool rool = true)
    {
        if(value > max_num)
        {
            value = max_num;
        }
        m_value = value;
        Calculate(value);
        if(rool)
            m_is_rolling = true;
        m_rollBeginTime = Time.time;
        if (m_scale_Weaver)
           m_scale_Weaver.Begin_Wave();
    }
}

using UnityEngine;
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

	void Start () 
    {
        m_worklist = new List<int>();
	}
	

	void Update ()
    {
	
	}

    void Calculate(int val)
    {
        m_worklist.Clear();
       //桁数を出す
        while(val > 0)
        {
            int surplus = val % 10;
            m_worklist.Insert(0, surplus);
           // m_worklist.Add(surplus);
            val /= 10;
        }

        int active_keta = m_worklist.Count;
        int count =0;
        foreach (var it in m_worklist)
        {
            var render_num = m_numbers[count].GetComponent<Numbers>();
            render_num.Enable();
            render_num.SetMaterial(m_numberMaterial[it]);
            count++;
        }
         
        for(int i  = active_keta ; i < m_numbers.Length ; i++)
        {
            var render_num = m_numbers[i].GetComponent<Numbers>();
            render_num.Disalble();
        }

    }

    public void SetNumber(int value)
    {
        if(value > max_num)
        {
            value = max_num;
        }
        m_value = value;
        Calculate(value);
    }
}

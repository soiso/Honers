using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Ranking : MonoBehaviour
{
    List<int> m_Score;
    [SerializeField, HeaderAttribute("保存する個数")]
    private int m_Capacity;

    [SerializeField]
    GameObject m_numbers;

    NumberRenderer m_num;

    int m_current_Index = 0;


	// Use this for initialization
	void Start ()
    {
        m_Score = new List<int>();
        //m_Score.Capacity = m_Capacity;
        for (int i = 0; i < m_Capacity; i++)
        {
            m_Score.Add(PlayerPrefs.GetInt("Rank" + (i + 1).ToString(), 0));
            m_Score.Sort();
            m_numbers.transform.GetChild(i).GetComponent<NumberRenderer>().SetNumber(m_Score[i]);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < m_Capacity; i++)
        {
            Debug.Log(m_Score[i]);
            PlayerPrefs.SetInt("Rank" + (i + 1).ToString(), m_Score[i]);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetNewScore();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Reset();
        }
	}

    //スコアとランキングを比較してランクインした順位を返す
    //ランク外なら-1
    public int  Compare( int score )
    {

        for (int i = 0; i < m_Capacity; i++)
        {
            if( score > m_Score[i] )
            {
                return i;
            }
        }

        return -1;
    }


    void SetNewScore()
    {
        int inset = Random.Range(100, 1000);
        int rank = Compare(inset);
        if (rank == -1) return;

        m_Score.Remove(m_Score[m_Capacity - 1]);
        m_Score.Add(inset);
        m_Score.Sort();
        m_Score.Reverse();

        for (int i = 0; i < m_Capacity; i++ )
        {
            PlayerPrefs.SetInt("Rank" + i.ToString(), m_Score[i]);
            m_numbers.transform.GetChild(i).GetComponent<NumberRenderer>().SetNumber(m_Score[i]);
        }
  
    }

    void Reset()
    {
        for (int i = 0; i < m_Capacity; i++)
        {
            m_Score[i] = 0;
            m_numbers.transform.GetChild(i).GetComponent<NumberRenderer>().SetNumber(m_Score[i]);
        }

    }

}

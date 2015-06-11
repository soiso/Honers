using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Ranking : MonoBehaviour
{
    List<int> m_Score;
    [SerializeField, HeaderAttribute("保存する個数")]
    private int m_Capacity;

    [SerializeField, HeaderAttribute("スコア")]
    GameObject m_numbers;
    [SerializeField, HeaderAttribute("順位")]
    GameObject m_rank;

    private int m_CurrentScore;
    private int m_CurrentRnak;

    [SerializeField, HeaderAttribute("拡大倍率")]
    private float m_scale;

    [SerializeField, HeaderAttribute("点滅周期")]
    private float m_swith_Interval = 1.0f;
    private int m_switch_Timer = 0;
    private float m_nextSwitch;

    Vector3 m_StartPos;
    Vector3 m_EndPos;

    private int m_numloop;

    [SerializeField, HeaderAttribute("基本LocalScale")]
    private Vector3 m_default_Scale;
    [SerializeField, HeaderAttribute("拡大Scale")]
    private Vector3 m_expand_Scale;

    [SerializeField, HeaderAttribute("縮小Scale")]
    private Vector3 m_reduction_Scale;
    [SerializeField, HeaderAttribute("拡縮速度"), Range(0f, 1f)]
    private float m_speed = 0.05f;

	// Use this for initialization
	void Start ()
    {
        m_Score = new List<int>();
        //m_Score.Capacity = m_Capacity;
        for (int i = 0; i < m_Capacity; i++)
        {
            m_Score.Add(PlayerPrefs.GetInt("Rank" + (i + 1).ToString(), 0));
            m_Score.Sort();
            m_Score.Reverse();
            m_numbers.transform.GetChild(i).GetComponent<NumberRenderer>().SetNumber(m_Score[i]);
        }
        m_CurrentScore = 0;
        for( int i =0; i < 5; i++ )
        {
            m_CurrentScore += (int)Objectmanager.m_instance.m_score.GetScore(i);
        }
        m_CurrentRnak = Compare(m_CurrentScore);
        SetNewScore(m_CurrentScore);
        m_numloop = 0;
        while( m_CurrentScore > 0 )
        {
            m_numloop++;
            m_CurrentScore /= 10;
        }
         //m_current_Mode = ScalingMode.fix_default;
	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < m_Capacity; i++)
        {
            Debug.Log(m_Score[i]);
            PlayerPrefs.SetInt("Rank" + (i + 1).ToString(), m_Score[i]);
        }

        if (m_CurrentRnak != -1)
        {
            float interval = Time.time + m_swith_Interval;
            Transform t = m_numbers.transform.GetChild(m_CurrentRnak).transform;
            t.transform.localScale = Vector3.Lerp(t.transform.localScale, m_expand_Scale, m_speed);
            if (Time.time > m_nextSwitch)
            {
                for (int i = 0; i < m_numloop; i++  )
                {
                    MeshRenderer r = m_numbers.transform.GetChild(m_CurrentRnak).GetChild(i).GetComponent<MeshRenderer>();
                    r.enabled = !r.enabled;
                }
                m_nextSwitch = Time.time + m_swith_Interval;
            }
        }

        if( IsFlick() )
        {
            //データ保存
            for (int i = 0; i < m_Capacity; i++ )
            {
                PlayerPrefs.SetInt("Rank" + (i + 1).ToString(), m_Score[i]);
            }
            NextScene();
        }

        //if( Input.GetKeyDown( KeyCode.Mouse1 ) )
        //{
        //    Reset();
        //}

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


    void SetNewScore( int score )
    {
        //int inset = Random.Range(100, 1000);
        int rank = Compare(score);
        if (rank == -1) return;

        m_Score.Remove(m_Score[m_Capacity - 1]);
        m_Score.Add(score);
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

    public void NextScene()
    {
        Objectmanager.m_instance.m_fruit_Counter.Reset();
        Objectmanager.m_instance.m_score.Reset();
        Objectmanager.m_instance.m_scene_manager.NextSceneLoad("TitleTest");
    }

    private bool IsFlick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //現状タップらしいからここで帰る
            return true;
            m_StartPos = Input.mousePosition;
            m_StartPos.z = .0f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_EndPos = Input.mousePosition;
            m_EndPos.z = .0f;
            Vector3 FlickVec = m_EndPos - m_StartPos;
            FlickVec.Normalize();
            Vector3 Horlizon = new Vector3(1.0f, .0f, .0f);
            if (Vector3.Dot(FlickVec, Horlizon) < .0f)
            {
                return true;
            }
        }
        return false;
    }

}

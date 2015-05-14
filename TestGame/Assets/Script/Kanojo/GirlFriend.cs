using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GirlFriend : MonoBehaviour 
{
    private GameObject m_current_WantFruit = null;

    [SerializeField]
    private float m_min_shuffleInterval =10f;
    [SerializeField]
    private float m_max_shuffleInterval = 30f;

    private float m_lastShuffleTime = 0f;
    private float m_currentShuffleTime = 0f;

    [SerializeField]
    private GameObject[] m_fruit_Renderer;

    private FruitArrangeManager m_fruit_ArrangeManager;

    [SerializeField]
    private float m_maxAngry_Limit;
    [SerializeField]
    private float m_stress_Speed;
    private float m_current_stress = 0f;

    [SerializeField]
    private GameObject m_fukidshi;

    //Debug
    [SerializeField]
    Text m_Update_IntervalText;

    //private Score m_score;

    [SerializeField]
    private GameObject[] m_playerCollider;
   
    //delegate
    delegate void StateEnter();
    delegate bool StateExecute();
    delegate void StateExit();

	void Start () 
    {
       if(m_playerCollider.Length == 0)
       {
           Debug.Log("GirlFriend PlayerCollider is null !!");
       }
       if (m_fruit_Renderer.Length == 0)
       {
           Debug.Log("GirlFriend FruitRenderer is null !!");
       }
        for(int i = 0 ; i < m_fruit_Renderer.Length ; i++)
        {
            m_fruit_Renderer[i].GetComponent<MeshRenderer>().enabled = false;
        }

        m_current_WantFruit = m_fruit_Renderer[Random.Range(0, m_fruit_Renderer.Length)];
        m_current_WantFruit.GetComponent<MeshRenderer>().enabled = true;
        m_currentShuffleTime = Random.Range(m_min_shuffleInterval, m_max_shuffleInterval);


        m_fruit_ArrangeManager = GameObject.Find("FruitManager").GetComponent<FruitArrangeManager>();
	}

    void    Shaffle_WantFruit()
    {
        m_current_WantFruit.GetComponent<MeshRenderer>().enabled = false;
        bool loop = true;
        while(loop)
        {
            GameObject next = m_fruit_Renderer[Random.Range(0, m_fruit_Renderer.Length)];
            if(next != m_current_WantFruit)
            {
                m_current_WantFruit = next;
                m_current_WantFruit.GetComponent<MeshRenderer>().enabled = true;
                loop = false;
            }
        }
    }

    void   Shaffle_UpdateInterval()
    {
        float next_Interval = Random.Range(m_min_shuffleInterval, m_max_shuffleInterval);
        m_currentShuffleTime = Time.time + next_Interval;
    }
	
    void    Update_ShaffleInterval()
    {
        float current_Time = Time.time;
        if(current_Time >= m_currentShuffleTime)
        {
            Shaffle_WantFruit();
            Shaffle_UpdateInterval();
        }
        m_Update_IntervalText.text = "NextUpdate : " + m_currentShuffleTime.ToString();
    }

    void    Update_Stress()
    {
        m_current_stress += (m_stress_Speed / 60.0f);
    }

    void    Collider_Check()
    {
        //for(int i = 0 ; i < m_playerCollider.Length ; i++)
        //{
        //    if(m_playerCollider[i].GetComponent<Boolean_BoxCollider>().m_is_Active)
        //    {
        //        m_fruit_ArrangeManager.Arrange_Fruit();
        //    }
        //}
    }

	void Update () 
    {
        Update_Stress();
        Update_ShaffleInterval();
        Collider_Check();

	}

//    public float Get_Current_FruitRate(Fruit.FRUIT_TYPE type)
//    {
//        if(type == m_current_WantFruit.GetComponent<FruitInfomation>().fruit_type)
//        {
//            return 2.0f;
//        }
//        return 1.0f;
//    }


///**************************************************************************************/
///*                                                                             Stay                                                                                    */ 
///**************************************************************************************/
//    void    StayEnter()
//    {

//    }

//    bool    StayExecute()
//    {
//        return false;
//    }

//    /**************************************************************************************/
//    /*                                                                             GetFruit                                                                              */
//    /**************************************************************************************/

//    void GetFruitEnter()
//    {

//    }

//    bool GetFruitExecute()
//    {
//        return false;
//    }

}

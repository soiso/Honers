using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(TreeParametor))]
public class FruitArrangeManager : MonoBehaviour {

    public FruitFactory m_factory{get; private set;}
    private float  m_next_Enable_Arrangetime;
    [SerializeField]
    private float m_enable_ArrangeInterval = 3f;

    [SerializeField,HeaderAttribute("基準となるフルーツ出現間隔（秒）")]
    public float m_default_SpornInterval = 10f;
    [SerializeField,HeaderAttribute("基準値に対する最大上限値")]
    public  float m_adjust_Second = 2f;

    private float m_lastShuffleTime = 0f;
    private float m_currentShuffleTime = 0f;

    [SerializeField]
    private GameObject[] m_GameObjectOfTree;
    private Tree[] m_tree_Array;

    [SerializeField, HeaderAttribute("SpeedUp出現間隔")]
    private float m_speedUp_Interval = 7.0f;
    private float m_last_SpeedUpSpornTime = 0f;

   private FruitEventManager m_event_Manager;
    
    //やっつけ
    [SerializeField, HeaderAttribute("どんぐり出現までの数")]
    private int dongri_count;

    public int Get_DongriCount { get { return dongri_count; } }

    void Awake()
    {
        m_event_Manager = GetComponent<FruitEventManager>();
      foreach(var it in m_GameObjectOfTree)
      {
          m_event_Manager.Calculate_TreePoint(it.GetComponent<TreeParametor>());
      }
    }

	void Start () 
    {
        Objectmanager.m_instance.m_fruit_Counter.Set_FruitManager(this);
        m_factory = GetComponent<FruitFactory>();
        m_next_Enable_Arrangetime = m_enable_ArrangeInterval;

        m_tree_Array = new Tree[m_GameObjectOfTree.Length];
        for (int i = 0; i < m_GameObjectOfTree.Length; i ++)
        {
            m_tree_Array[i] = m_GameObjectOfTree[i].GetComponent<Tree>();
        }
	}

   private bool Book_SpecialFruit(FruitInterFace.FRUIT_TYPE type)
    {
       for(int i =0 ; i < 5 ; i++)
       {
           int index = Random.Range(0, m_tree_Array.Length);
           var param = m_tree_Array[index].GetComponent<TreeParametor>();
           if (param.m_book_fruit == FruitInterFace.FRUIT_TYPE.error)
           {
               m_tree_Array[index].GetComponent<Tree>().Set_BookFruit(type);
               return true;
           }
       }
       return false;

    }

    private void Update_SpecialFruit()
    {
        if (Time.time >= m_last_SpeedUpSpornTime + m_speedUp_Interval)
        {
            m_last_SpeedUpSpornTime = Time.time;
            Book_SpecialFruit(FruitInterFace.FRUIT_TYPE.speed_up);
        }

    }

    public void Create_Dongri()
    {
        bool loop = true;
        int count = 0;
        while(loop)
        {
            int index = Random.Range(0, m_tree_Array.Length);
            if(m_GameObjectOfTree[index].GetComponent<TreeParametor>().m_enable_Donguri)
            {
                m_tree_Array[index].GetComponent<Tree>().Set_BookFruit(FruitInterFace.FRUIT_TYPE.donguri);
                loop = false;
            }
            count++;
            if(count >= 100)
            {
                Debug.Log("LoopがFruitManagerでまわりすぎ");
                loop = false;
            }
        }
    }

	void Update ()
    {
        Update_SpecialFruit();
	}

    public bool Begin_FeaverTime()
    {
        foreach(var it in m_tree_Array)
        {
            it.Begin_Feaver();
        }
        return true;
    }

    public bool Event(int point_no)
    {
       return m_event_Manager.Event_Check(point_no);
    }
}

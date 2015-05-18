using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

    [SerializeField,HeaderAttribute("どんぐり出現間隔")]
    private float m_donguri_Interval = 8.0f;
    private float m_last_DongrispornTime = 0f;
    [SerializeField, HeaderAttribute("SpeedUp出現間隔")]
    private float m_speedUp_Interval = 7.0f;
    private float m_last_SpeedUpSpornTime = 0f;
    
    void Awake()
    {
      
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
        //Arrange_Fruit();
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
        if(Time.time >= m_last_DongrispornTime + m_donguri_Interval )
        {
            m_last_DongrispornTime = Time.time;
            Book_SpecialFruit(FruitInterFace.FRUIT_TYPE.donguri);
        }

        if (Time.time >= m_last_SpeedUpSpornTime + m_speedUp_Interval)
        {
            m_last_SpeedUpSpornTime = Time.time;
            Book_SpecialFruit(FruitInterFace.FRUIT_TYPE.speed_up);
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
}

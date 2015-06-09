using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(TreeParametor))]
public class FruitArrangeManager : MonoBehaviour {

    public enum DONGRI_SPORNTYPE
    {
        SCORE,
        NUM_FRUIT,
    }
    [SerializeField,HeaderAttribute("どんぐりの出現タイプ")]
    private DONGRI_SPORNTYPE m_spern_type;

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

    public FruitEventManager m_event_Manager { get; private set; }

   private float m_next_Dongrisocre = 0;

    //やっつけ
    [SerializeField, HeaderAttribute("どんぐり出現までの数")]
    private int dongri_count;

    [SerializeField, HeaderAttribute("どんぐりが出現するまでのスコア")]
    private float m_dongri_score;

    public int Get_DongriCount { get { return dongri_count; } }

    private GameObject feaver_sign;

    private int m_current_Fruit =0;

    private GameObject m_player;

    public GameObject m_fieldFruit_Root { get; private set; }

    void Awake()
    {
        Objectmanager.m_instance.m_fruit_Counter.Set_FruitManager(this);
        m_event_Manager = GetComponent<FruitEventManager>();
      foreach(var it in m_GameObjectOfTree)
      {
          m_event_Manager.Calculate_TreePoint(it.GetComponent<TreeParametor>());
      }
      m_next_Dongrisocre = m_dongri_score;
      m_fieldFruit_Root = new GameObject("Fruit_Root");
      m_fieldFruit_Root.transform.position = Vector3.zero;
      m_fieldFruit_Root.transform.rotation = Quaternion.identity;
     
    }

	void Start () 
    {
        
        m_factory = GetComponent<FruitFactory>();
        m_next_Enable_Arrangetime = m_enable_ArrangeInterval;

        m_tree_Array = new Tree[m_GameObjectOfTree.Length];
        for (int i = 0; i < m_GameObjectOfTree.Length; i ++)
        {
            m_tree_Array[i] = m_GameObjectOfTree[i].GetComponent<Tree>();
        }
        feaver_sign = GameObject.Find("FeaverSign");
        m_player = GameObject.Find("Player");
        m_fieldFruit_Root.transform.parent = this.gameObject.transform.parent;
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

    public  void Dongri_Check(float fruit_score,bool is_FeaverSporn)
    {
        if(m_spern_type == DONGRI_SPORNTYPE.NUM_FRUIT)
        {
            m_current_Fruit++;
            if(m_current_Fruit % dongri_count == 0)
            {
                Create_Dongri();
            }
            return;
        }
        else
        {
            if(feaver_sign.GetComponent<FeaverSign>().feaver_flag ||
                is_FeaverSporn)
            {
                m_next_Dongrisocre += fruit_score;
            }

            float score = Objectmanager.m_instance.m_score.GetScore(Objectmanager.m_instance.m_scene_manager.currentScene_num);
            if(score > m_next_Dongrisocre)
            {
                Create_Dongri();
                m_next_Dongrisocre += m_dongri_score;
            }
        }
        
    }

    public void Create_Dongri()
    {
        //bool loop = true;
        //int count = 0;
        //while(loop)
        //{
        //    int index = Random.Range(0, m_tree_Array.Length);
        //    if(m_GameObjectOfTree[index].GetComponent<TreeParametor>().m_enable_Donguri)
        //    {
        //        m_tree_Array[index].GetComponent<Tree>().Set_BookFruit(FruitInterFace.FRUIT_TYPE.donguri);
        //        loop = false;
        //    }
        //    count++;
        //    if(count >= 100)
        //    {
        //        Debug.Log("LoopがFruitManagerでまわりすぎ");
        //        loop = false;
        //    }
        //}

        float max_far_dist =0f;
        Tree create_Tree = null;
        foreach(var it in m_tree_Array)
        {
            Vector3 vec = it.transform.position - m_player.transform.position;

            float dist = vec.magnitude;
            if (dist > max_far_dist)
            {
                max_far_dist = dist;
                create_Tree = it;
            }
        }
        if(create_Tree == null)
        {
            Debug.Log("どんぐりできない");
        }
        create_Tree.Set_BookFruit(FruitInterFace.FRUIT_TYPE.donguri);

    }

	void Update ()
    {
        Update_SpecialFruit();
	}

    public bool Begin_FeaverTime()
    {
        feaver_sign.GetComponent<FeaverSign>().Feaver_Begin();
        foreach(var it in m_tree_Array)
        {
            it.Begin_Feaver();
        }
        return true;
    }

    public bool End_FeaverTime()
    {
        feaver_sign.GetComponent<FeaverSign>().Feaver_End();
        return true;
    }

    public bool Event(int point_no)
    {
       return m_event_Manager.Event_Check(point_no);
    }

    public void Regist_Fruit(GameObject regist_Fruit)
    {
        regist_Fruit.transform.parent = m_fieldFruit_Root.transform;
    }

}

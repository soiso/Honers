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

	// Use this for initialization

    void Awake()
    {
      
    }

	void Start () 
    {
        Objectmanager.m_instance.m_fruit_Counter.Set_FruitManager(this);
        //if(m_GameObjectOfTree.Length == 0)
        //{
        //    Debug.Log("FruitArrangeManager::arrangePoint is null !!");
        //}
        m_factory = GetComponent<FruitFactory>();
        m_next_Enable_Arrangetime = m_enable_ArrangeInterval;

        m_tree_Array = new Tree[m_GameObjectOfTree.Length];
        for (int i = 0; i < m_GameObjectOfTree.Length; i ++)
        {
            m_tree_Array[i] = m_GameObjectOfTree[i].GetComponent<Tree>();
        }
        //Arrange_Fruit();
	}
	
	void Update ()
    {

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

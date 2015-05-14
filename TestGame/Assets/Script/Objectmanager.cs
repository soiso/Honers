using UnityEngine;
using System.Collections;

public class Objectmanager : Singleton<Objectmanager>
{

    public FruitCounter m_fruit_Counter { get; private set; }
    public TimeScaler m_timescale_Adjust;

    void Awake()
    {
        m_fruit_Counter = GetComponentInChildren<FruitCounter>();
        m_timescale_Adjust = GetComponentInChildren<TimeScaler>();
    }

	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}

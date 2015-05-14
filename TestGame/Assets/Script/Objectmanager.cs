﻿using UnityEngine;
using System.Collections;

public class Objectmanager : Singleton<Objectmanager>
{
    public FruitCounter m_fruit_Counter { get; private set; }
    public TimeScaler m_timescale_Adjust;
    public SceneManager m_scene_manager { get; private set; }
    public Stage_Timer m_stage_timer { get; private set; }
    void Awake()
    {
        m_fruit_Counter = GetComponentInChildren<FruitCounter>();
        m_timescale_Adjust = GetComponentInChildren<TimeScaler>();
        m_scene_manager = GetComponentInChildren<SceneManager>();
        m_stage_timer = GetComponentInChildren<Stage_Timer>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}

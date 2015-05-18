using UnityEngine;
using System.Collections;

public class Objectmanager : Singleton<Objectmanager>
{
    public FruitCounter m_fruit_Counter { get; private set; }
    public TimeScaler m_timescale_Adjust;
    public SceneManager m_scene_manager { get; private set; }

    public Score m_score { get; private set; }
    public Stage_Timer m_stage_timer { get; private set; }
    public AudioSource m_BGM { get; private set; }
    public CameraMove m_camera_move { get; private set; }

    void Awake()
    {
        m_fruit_Counter = GetComponentInChildren<FruitCounter>();
        m_timescale_Adjust = GetComponentInChildren<TimeScaler>();
        m_scene_manager = GetComponentInChildren<SceneManager>();

        m_stage_timer = GetComponentInChildren<Stage_Timer>();
        m_score = GetComponentInChildren<Score>();

        m_BGM = GetComponentInChildren<AudioSource>();

        m_camera_move = GetComponentInChildren<CameraMove>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}

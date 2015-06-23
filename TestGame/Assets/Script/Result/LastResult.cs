using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LastResult : MonoBehaviour {

    [SerializeField, Range(0, 20.0f)]
    private float timer;

    [SerializeField]
    public GameObject score_rank;
    private float start_time;
    private float current_time;
    private bool s_flag;
    [SerializeField]
    private GameObject[] score_num;

    public float m_interval = 0.1f;

    private Text scoretext;

#if UNITY_STANDALONE

    // Use this for initialization
    void Start()
    {
        start_time = Time.time;
        s_flag = false;

        score_num = GetComponentInChildren<NumberRenderer>();
        score_num.SetNumber((int)Objectmanager.m_instance.m_score.GetScore());

        apple_num = Objectmanager.m_instance.m_fruit_Counter.apple_num;
        strawberry_num = Objectmanager.m_instance.m_fruit_Counter.strawberry_num;
        peach_num = Objectmanager.m_instance.m_fruit_Counter.peach_num;
        grape_num = Objectmanager.m_instance.m_fruit_Counter.grape_num;

        foreach(GameObject obj in fruit_obj)
        {
            scoretext = obj.GetComponentInChildren<Text>();
            if (obj.name == "apple")
                scoretext.text = apple_num.ToString();
            if (obj.name == "peach")
                scoretext.text = peach_num.ToString();
            if (obj.name == "orrange")
                scoretext.text = grape_num.ToString();
            if (obj.name == "strawberry")
                scoretext.text = strawberry_num.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() == "TitleTest") return;
        current_time = Time.time;
        if (current_time - start_time > timer && s_flag != true)
        {
            NextScene();
            s_flag = true;
        }

    }
    public void NextScene()
    {
        Objectmanager.m_instance.m_fruit_Counter.Reset();
        Objectmanager.m_instance.m_score.Reset();
        Objectmanager.m_instance.m_scene_manager.NextSceneLoad();
    }


#elif UNITY_ANDROID || UNITY_IOS
    // Use this for initialization
    void Awake()
    {

    }
    void Start()
    {
        start_time = Time.time;
        s_flag = true;
        Objectmanager.m_instance.m_scene_manager.NextSceneLoad("Ranking");
    }

    // Update is called once per frame
    void Update()
    {
        if (Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() == "TitleTest") return;

        if(s_flag)
        {
            StartCoroutine("SetScore");
            s_flag = false;
        }
        if (Touch())
        {
            NextScene();
        }
    }
    private IEnumerator SetScore()
    {
        for (int i = 0; i < 5; i++)
        {
            score_num[i].GetComponent<NumberRenderer>().SetNumber((int)Objectmanager.m_instance.m_score.GetScore(i));
            yield return new WaitForSeconds(m_interval);
        }

        score_num[5].GetComponent<NumberRenderer>().SetNumber((int)Objectmanager.m_instance.m_score.GetTotalScore());
        yield return new WaitForSeconds(m_interval);
        
        score_rank.GetComponent<Rank>().SetScore((int)Objectmanager.m_instance.m_score.GetTotalScore());
        score_rank.GetComponent<Rank>().Enable();
    }
    public void NextScene()
    {
        //Objectmanager.m_instance.m_fruit_Counter.Reset();
        //Objectmanager.m_instance.m_score.Reset();
        //Objectmanager.m_instance.m_scene_manager.NextSceneLoad();
        Objectmanager.m_instance.m_scene_manager.BeginLoad();
    }
    private Vector3 m_StartPos;
    private Vector3 m_EndPos;

    private bool Touch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
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

#endif
}

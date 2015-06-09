using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Result : MonoBehaviour
{
    [SerializeField, Range(0, 20.0f)]
    private float timer;
    [SerializeField]
    GameObject[] fruit_obj;
    [SerializeField]
    public GameObject create_fruit;
    [SerializeField]
    public GameObject score_rank;
    private float start_time;
    private float current_time;
    private bool s_flag;

    private NumberRenderer score_num;
    private int apple_num = 0;
    private int strawberry_num = 0;
    private int peach_num = 0;
    private int grape_num = 0;

    private int max_fruit_num;
    private int current_fruit_num;
    public Vector3 create_fruit_scale = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 create_fruit_pos_play = new Vector3(.0f, .0f, .0f);
    private Vector3 create_fruit_pos = new Vector3(0, 0, 0);
    public float create_fruit_wait = .0f;
    private float create_fruit_time = .0f;
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
        //Objectmanager.m_instance.m_score.Reset();
        Objectmanager.m_instance.m_scene_manager.NextSceneLoad();
    }


#elif UNITY_ANDROID || UNITY_IOS
    // Use this for initialization
    void Start()
    {
        start_time = Time.time;
        s_flag = false;

        score_num = GetComponentInChildren<NumberRenderer>();
        score_num.SetNumber((int)Objectmanager.m_instance.m_score.GetScore(Objectmanager.m_instance.m_scene_manager.currentScene_num));

        score_rank.GetComponent<Rank>().SetScore((int)Objectmanager.m_instance.m_score.GetScore(Objectmanager.m_instance.m_scene_manager.currentScene_num));
        score_rank.GetComponent<Rank>().Enable();

        apple_num = Objectmanager.m_instance.m_fruit_Counter.apple_num;
        strawberry_num = Objectmanager.m_instance.m_fruit_Counter.strawberry_num;
        peach_num = Objectmanager.m_instance.m_fruit_Counter.peach_num;
        grape_num = Objectmanager.m_instance.m_fruit_Counter.grape_num;

        foreach (GameObject obj in fruit_obj)
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
        current_fruit_num = 0;
        max_fruit_num = apple_num;
        if (max_fruit_num < grape_num) max_fruit_num = grape_num;
        if (max_fruit_num < peach_num) max_fruit_num = peach_num;
        if (max_fruit_num < strawberry_num) max_fruit_num = strawberry_num;

        CreatePosSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() == "TitleTest") return;
        current_time = Time.time;
        //if (current_time - start_time > timer && s_flag != true)
        //{
        //    NextScene();
        //    s_flag = true;
        //}
        if (Time.time > create_fruit_time + create_fruit_wait)
            Fruit_Fall();
        if (Touch())
        {
            NextScene();
            s_flag = true;
        }
    }
    public void NextScene()
    {
        Objectmanager.m_instance.m_fruit_Counter.Reset();
        //Objectmanager.m_instance.m_score.Reset();
        Objectmanager.m_instance.m_scene_manager.NextSceneLoad();
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

    private void Fruit_Fall()
    {
        GameObject insert = null;


        if (current_fruit_num < apple_num)
        {
            CreatePosSet();

            insert = create_fruit.GetComponent<FruitFactory>().Create_Object(FruitInterFace.FRUIT_TYPE.apple);
            insert.transform.position = create_fruit.transform.position + create_fruit_pos;
            insert.transform.localScale = create_fruit_scale;
            insert.GetComponent<Fruit>().m_swith_Time = 0;
            insert.GetComponent<Fruit>().m_EraseTime += 100;
        }
        if (current_fruit_num < grape_num)
        {
            CreatePosSet();

            insert = create_fruit.GetComponent<FruitFactory>().Create_Object(FruitInterFace.FRUIT_TYPE.grape);
            insert.transform.position = create_fruit.transform.position + create_fruit_pos;
            insert.transform.localScale = create_fruit_scale;
            insert.GetComponent<Fruit>().m_swith_Time = 0;
            insert.GetComponent<Fruit>().m_EraseTime += 100;
        }
        if (current_fruit_num < peach_num)
        {
            CreatePosSet();

            insert = create_fruit.GetComponent<FruitFactory>().Create_Object(FruitInterFace.FRUIT_TYPE.peach);
            insert.transform.position = create_fruit.transform.position + create_fruit_pos;
            insert.transform.localScale = create_fruit_scale;
            insert.GetComponent<Fruit>().m_swith_Time = 0;
            insert.GetComponent<Fruit>().m_EraseTime += 100;
        }
        if (current_fruit_num < strawberry_num)
        {
            CreatePosSet();

            insert = create_fruit.GetComponent<FruitFactory>().Create_Object(FruitInterFace.FRUIT_TYPE.strawberry);
            insert.transform.position = create_fruit.transform.position + create_fruit_pos;
            insert.transform.localScale = create_fruit_scale;
            insert.GetComponent<Fruit>().m_swith_Time = 0;
            insert.GetComponent<Fruit>().m_EraseTime += 100;
        }
        current_fruit_num += 1;
        create_fruit_time = Time.time;
    }
    private void CreatePosSet()
    {
        create_fruit_pos.x = Random.Range(-create_fruit_pos_play.x, create_fruit_pos_play.x);
        create_fruit_pos.y = Random.Range(-create_fruit_pos_play.y, create_fruit_pos_play.y);
        create_fruit_pos.z = Random.Range(-create_fruit_pos_play.z, create_fruit_pos_play.z);
    }
#endif

}

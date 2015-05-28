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
        Objectmanager.m_instance.m_score.Reset();
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

        if (current_fruit_num < apple_num) insert = create_fruit.GetComponent<FruitFactory>().Create_Object(FruitInterFace.FRUIT_TYPE.apple);
        Vector3 rand = new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(-5.0f, 5.0f), 0);
        Vector3 fruit_scale = new Vector3(1.5f, 1.5f, 1.0f);
        insert.transform.position = create_fruit.transform.position + rand;
        insert.transform.localScale = fruit_scale;
        if (current_fruit_num < grape_num) insert = create_fruit.GetComponent<FruitFactory>().Create_Object(FruitInterFace.FRUIT_TYPE.grape);
        rand = new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(-5.0f, 5.0f), 0);
        fruit_scale = new Vector3(1.5f, 1.5f, 1.0f);
        insert.transform.position = create_fruit.transform.position + rand;
        insert.transform.localScale = fruit_scale;
        if (current_fruit_num < peach_num) insert = create_fruit.GetComponent<FruitFactory>().Create_Object(FruitInterFace.FRUIT_TYPE.peach);
        rand = new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(-5.0f, 5.0f), 0);
        fruit_scale = new Vector3(1.5f, 1.5f, 1.0f);
        insert.transform.position = create_fruit.transform.position + rand;
        insert.transform.localScale = fruit_scale;
        if (current_fruit_num < strawberry_num) insert = create_fruit.GetComponent<FruitFactory>().Create_Object(FruitInterFace.FRUIT_TYPE.strawberry);
        rand = new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(-5.0f,5.0f), 0);
        fruit_scale = new Vector3(1.5f, 1.5f, 1.0f);
        insert.transform.position = create_fruit.transform.position + rand;
        insert.transform.localScale = fruit_scale;
        current_fruit_num += 1;
    }

#endif

}

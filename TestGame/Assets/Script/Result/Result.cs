using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Result : MonoBehaviour
{
    private float start_time;
    private float current_time;
    private bool s_flag;

    private Text score_num;
    private int apple_num = 0;
    private int strawberry_num = 0;
    private int peach_num = 0;
    private int grape_num = 0;

    private Transform[] transformList;
    private Text scoretext;

#if UNITY_STANDALONE

    // Use this for initialization
    void Start()
    {
        start_time = Time.time;
        s_flag = false;
        transformList = this.transform.GetComponentsInChildren<Transform>();
        score_num = transform.FindChild("Score_num").GetComponent<Text>();
        score_num.text = Objectmanager.m_instance.m_score.GetScore().ToString();

        apple_num = Objectmanager.m_instance.m_fruit_Counter.apple_num;
        strawberry_num = Objectmanager.m_instance.m_fruit_Counter.strawberry_num;
        peach_num = Objectmanager.m_instance.m_fruit_Counter.peach_num;
        grape_num = Objectmanager.m_instance.m_fruit_Counter.grape_num;

        foreach(Transform trans in transformList)
        {
            if (trans.name == "Canvas") continue;
            if (trans.name == "Background") continue;
            if (trans.name == "Mozi") continue;
            if (trans.name == "Score_num") continue;
            if (trans.name == "Text") continue;

            GameObject fruit_num = trans.FindChild("Text").gameObject;
            scoretext = fruit_num.GetComponent<Text>();
            if (trans.name == "apple")
                scoretext.text = apple_num.ToString();
            if (trans.name == "peach")
                scoretext.text = peach_num.ToString();
            if (trans.name == "orrange")
                scoretext.text = grape_num.ToString();
            if (trans.name == "strawberry")
                scoretext.text = strawberry_num.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() == "TitleTest") return;
        current_time = Time.time;
        if (current_time - start_time > 10.0f && s_flag != true)
        {
            NextScene();
            s_flag = true;
        }
    }
    public void NextScene()
    {
        Objectmanager.m_instance.m_scene_manager.NextSceneLoad();
    }
#elif UNITY_ANDROID || UNITY_IOS
    //void Update()
    //{
    //    //IsTouch();
    //}

    //public void IsTouch()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        Objectmanager.m_instance.m_scene_manager.NextSceneLoad();
    //    }
    //}
    // Use this for initialization
    void Start()
    {
        start_time = Time.time;
        s_flag = false;
        transformList = this.transform.GetComponentsInChildren<Transform>();
        score_num = transform.FindChild("Score_num").GetComponent<Text>();
        score_num.text = Objectmanager.m_instance.m_score.GetScore().ToString();

        apple_num = Objectmanager.m_instance.m_fruit_Counter.apple_num;
        strawberry_num = Objectmanager.m_instance.m_fruit_Counter.strawberry_num;
        peach_num = Objectmanager.m_instance.m_fruit_Counter.peach_num;
        grape_num = Objectmanager.m_instance.m_fruit_Counter.grape_num;

        foreach (Transform trans in transformList)
        {
            if (trans.name == "Canvas") continue;
            if (trans.name == "Background") continue;
            if (trans.name == "Mozi") continue;
            if (trans.name == "Score_num") continue;
            if (trans.name == "Text") continue;

            GameObject fruit_num = trans.FindChild("Text").gameObject;
            scoretext = fruit_num.GetComponent<Text>();
            if (trans.name == "apple")
                scoretext.text = apple_num.ToString();
            if (trans.name == "peach")
                scoretext.text = peach_num.ToString();
            if (trans.name == "orrange")
                scoretext.text = grape_num.ToString();
            if (trans.name == "strawberry")
                scoretext.text = strawberry_num.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() == "TitleTest") return;
        current_time = Time.time;
        if (current_time - start_time > 10.0f && s_flag != true)
        {
            NextScene();
            s_flag = true;
        }
    }
    public void NextScene()
    {
        Objectmanager.m_instance.m_scene_manager.NextSceneLoad();
    }
#endif

}

using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour
{
    private float start_time;
    private float current_time;
    private bool s_flag;

#if UNITY_STANDALONE

    // Use this for initialization
    void Start()
    {
        start_time = Time.time;
        s_flag = false;
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
    void Update()
    {
        //IsTouch();
    }

    //public void IsTouch()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        Objectmanager.m_instance.m_scene_manager.NextSceneLoad();
    //    }
    //}
#endif

}

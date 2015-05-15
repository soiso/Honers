
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class SceneManager : MonoBehaviour
{
    [SerializeField, SceneName]
    public string[] sceneName;

    private GameObject picpaper;
    private GameObject startsign;
    private AsyncOperation loadInfo;
    protected bool next_scene_load = false;
    public int currentScene_num = 0;
    public string currentSceneName;

    // Use this for initialization
    void Start()
    {
        currentSceneName = "TitleTest";
        startsign = GameObject.Find("StartSign");
    }

    // Update is called once per frame
    void Update()
    {

        if (next_scene_load == true)
        {
            if (loadInfo.allowSceneActivation == false)
            {
                if (loadInfo.progress >= 0.9f)
                {
                    //シーン切替 .
                    picpaper = GameObject.Find("PicturePaper_old");
                    picpaper.GetComponent<PicturePaper>().Move_Begin();
                    loadInfo.allowSceneActivation = true;
                }
            }
        }
        GameObject oldPicPaper = GameObject.Find("PicturePaper_old");
        if (oldPicPaper != null)
        {
            if (oldPicPaper.GetComponent<PicturePaper>().GetTargetRange() < 30.0f)
            {
                if (currentSceneName != "Result") startsign.GetComponent<StartSign>().AlphaIncrease_Begin();
                else Time.timeScale = 1;
                GameObject.Destroy(oldPicPaper);
                //BGM変えたい
                Objectmanager.m_instance.m_BGM.GetComponent<BGM>().ChangeBGM(currentSceneName);
            }

        }

        if (startsign.GetComponent<StartSign>().AlphaIncrease())
        {
            Time.timeScale = 1;

            Objectmanager.m_instance.m_stage_timer.Time_Start();

        }
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
    }
    public void CloseMenu()
    {
        Time.timeScale = 1;
    }

    public void ChangeScene(string sceneName)
    {
        currentSceneName = sceneName;
        Application.LoadLevel(sceneName);
        currentScene_num++;
        //BGM変えたい
        Objectmanager.m_instance.m_BGM.GetComponent<BGM>().ChangeBGM(sceneName);
        if (sceneName == "TitleTest")
            currentScene_num = 0;

    }
    public void ChangeScene_Add(string sceneName)
    {
        Time.timeScale = 0;
        currentSceneName = sceneName;
        picpaper = GameObject.Find("PicturePaper");

        loadInfo = Application.LoadLevelAdditiveAsync(sceneName);
        loadInfo.allowSceneActivation = false;
        next_scene_load = true;
        picpaper.GetComponent<PicturePaper>().SoundPlay();
        picpaper.name += "_old";

       
    }

    public void NextSceneLoad()
    {

        Objectmanager.m_instance.m_stage_timer.Reset();
        if (sceneName[currentScene_num] == "TitleTest")
        {
            ChangeScene(sceneName[currentScene_num]);
            currentScene_num = 0;
        }
        else
        {
            if (currentSceneName == sceneName[currentScene_num])
            {
                currentScene_num += 1;
                NextSceneLoad();
            }
            else
            {
                ChangeScene_Add(sceneName[currentScene_num]);
            }
        }
    }

    public void EndStage()
    {
        ChangeScene_Add("Result");
        currentScene_num++;
    }
    public string GetCurrentStageName()
    {
        return currentSceneName;
    }
}


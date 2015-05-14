
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class SceneManager : Singleton<SceneManager>
{
    [SerializeField, SceneName]
    public string[] sceneName;

    private GameObject picpaper;
    private GameObject startsign;
    private AsyncOperation loadInfo;
    protected bool next_scene_load = false;
    private int currentScene_num = 0;
    private string currentSceneName;



    // Use this for initialization
    void Start()
    {
        currentSceneName = "TitleTest";
        startsign = GameObject.Find("StartSign");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject oldPicPaper = GameObject.Find("PicturePaper_old");
        if (oldPicPaper != null)
        {
            if (oldPicPaper.GetComponent<PicturePaper>().GetTargetRange() < 30.0f)
            {
                startsign.GetComponent<StartSign>().AlphaIncrease_Begin();
                GameObject.Destroy(oldPicPaper);
            }

        }
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
        if (startsign.GetComponent<StartSign>().AlphaIncrease())
        {
            Time.timeScale = 1;
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

        currentScene_num++;

    }

    public void NextSceneLoad()
    {

        //next_scene_load = true;
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
}


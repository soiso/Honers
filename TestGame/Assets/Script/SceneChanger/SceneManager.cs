
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
    [SerializeField]
    private float[] rimit_time;
    [SerializeField]
    private GameObject light;
    [SerializeField]
    private GameObject fever_sign;
    [SerializeField]
    public int[] stage_pic_num;

    private GameObject picpaper;
    private GameObject startsign;
    private AsyncOperation loadInfo;
    public int currentScene_num = 0;
    public int LoadStage_num = 0;
    public string currentSceneName;
    private string previousSceneName;
    public string nextSceneName;

    private GameObject oldPicPaper;
    private bool LoadFlg = false;   //ロード中かどうか
    private bool ChangeFlg = false;

    private bool ResultFlg = false;

    // Use this for initialization
    void Start()
    {
        currentSceneName = "TitleTest";
        startsign = GameObject.Find("StartSign");
        oldPicPaper = null;
        Objectmanager.m_instance.m_camera_move.Init();
        Application.targetFrameRate = 60;
    }

    public void Init()
    {
        LoadFlg = false;   //ロード中かどうか
        loadInfo = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (ResultFlg)
        {
            light.GetComponent<Light>().intensity = Mathf.Lerp(light.GetComponent<Light>().intensity, 0.0f, 0.03f);
            float inset = light.GetComponent<Light>().intensity;
        }

        if (oldPicPaper != null)
        {
            if (oldPicPaper.GetComponent<PicturePaper>().GetTargetRange() < 20.0f)
            {
                if (currentSceneName == "New_Stage1" ||
                    currentSceneName == "New_Stage2" ||
                    currentSceneName == "New_Stage3" ||
                    currentSceneName == "New_Stage4" ||
                    currentSceneName == "New_Stage5")
                {
                    Objectmanager.m_instance.m_camera_move.cMove_Begin();
                    startsign.GetComponent<StartSign>().AlphaIncrease_Begin();
                    Objectmanager.m_instance.m_stage_timer.StartSEPlay();
                    Time.timeScale = 1;
                    oldPicPaper.GetComponent<PicturePaper>().PicDelete();
                    oldPicPaper = null;
                    
                        Objectmanager.m_instance.m_score.Init();
                }
                else
                {
                    Time.timeScale = 1;
                    oldPicPaper.GetComponent<PicturePaper>().PicDelete();
                    oldPicPaper = null;
                    //LoadFlg = false;
                    ChangeFlg = false;
                }
            }
        }

        if (startsign.GetComponent<StartSign>().AlphaIncrease())
        {
            Time.timeScale = 1;
            Objectmanager.m_instance.m_camera_move.cMove_Begin();
            Objectmanager.m_instance.m_stage_timer.Time_Start();
        }
    }
    public void BeginLoad()
    {
        if (!LoadFlg) return;
        if (loadInfo.allowSceneActivation) return;
        if (nextSceneName != "Result")
        {
            picpaper = GameObject.Find("PicturePaper");
            picpaper.GetComponent<PicturePaper>().SoundPlay();
            if (oldPicPaper != null)
            {
                oldPicPaper.GetComponent<PicturePaper>().PicDelete();
                oldPicPaper = null;
            }
            picpaper.name += "_old";
            oldPicPaper = picpaper;
        }
        previousSceneName = currentSceneName;
        currentSceneName = nextSceneName;
        //シーン切替 .
        if (currentSceneName != "Result")
            oldPicPaper.GetComponent<PicturePaper>().Move_Begin();
        //else
        LoadFlg = false;
        ResultFlg = false;
        loadInfo.allowSceneActivation = true;
        Time.timeScale = 0;


        if (currentSceneName == "New_Stage1" ||
    currentSceneName == "New_Stage2" ||
    currentSceneName == "New_Stage3" ||
    currentSceneName == "New_Stage4" ||
    currentSceneName == "New_Stage5")
        {
            Objectmanager.m_instance.m_camera_move.Init();
            fever_sign.GetComponent<FeaverSign>().FeaverSet();
        }
    }
    public bool LoadProgress()
    {
        if (!LoadFlg) return false;
        if (loadInfo.allowSceneActivation) return false;
        if (previousSceneName != "TitleTest")
            if (loadInfo.progress < 0.9f) return false;

        return true;
    }
    public void ChangeScene(string sceneName)
    {
        currentSceneName = sceneName;
        loadInfo = Application.LoadLevelAsync(sceneName);
        LoadFlg = true;
        loadInfo.allowSceneActivation = false;
        if (sceneName == "TitleTest")
            currentScene_num = 0;
    }
    public void ChangeScene_Add(string sceneName)
    {
        fever_sign.GetComponent<FeaverSign>().Reset();
        //previousSceneName = currentSceneName;
        nextSceneName = sceneName;
        if (currentSceneName != "Result")
            Objectmanager.m_instance.m_stage_timer.SetStageRimit(rimit_time[currentScene_num]);

        loadInfo = Application.LoadLevelAdditiveAsync(sceneName);
        LoadFlg = true;
        loadInfo.allowSceneActivation = false;


    }
    public void AddSceneResult()
    {
        loadInfo = Application.LoadLevelAdditiveAsync("Result");
        LoadFlg = true;
        loadInfo.allowSceneActivation = false;
        //ResultFlg = false;
        nextSceneName = "Result";
        currentSceneName = nextSceneName;
    }
    public void NextSceneLoad()
    {
        if (LoadFlg) return;
        Objectmanager.m_instance.m_stage_timer.Reset();

        if (currentSceneName != "TitleTest")
        {
            //currentSceneName = sceneName[currentScene_num];
            currentScene_num += 1;
            ChangeScene_Add(sceneName[currentScene_num]);
        }
        else
        {
            ChangeScene_Add(sceneName[currentScene_num]);
            return;
        }
    }

    public void NextSceneLoad(string newstageName)
    {
        if (LoadFlg) return;
        Objectmanager.m_instance.m_stage_timer.Reset();
        if (newstageName != "Result")
            currentScene_num = 0;
        foreach (string stageName in sceneName)
        {
            if (stageName == newstageName)
            {
                if (newstageName == "TitleTest")
                    ChangeScene(newstageName);
                else
                {
                    ChangeScene_Add(newstageName);
                }
                break;
            }
            if (newstageName != "Result")
                currentScene_num++;
        }
    }

    public void EndStage()
    {
        if (LoadFlg) return;
        ResultFlg = true;
        if (currentSceneName == "New_Stage5")
        {
            LoadStage_num = 5;
            //return;
        }
        AddSceneResult();

    }
    public string GetCurrentStageName()
    {
        return currentSceneName;
    }
}

